using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CoreRepo.Core.Enums;
using CoreRepo.Data;
using CoreRepo.Data.Infrastructure;
using CoreRepo.Data.Models;
using CoreRepo.Data.Receipt;
using CoreRepo.Data.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TanvirArjel.EFCore.GenericRepository;

namespace CoreRepo.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class DataBenchmark
{
    private IConfiguration _configuration;
    private IServiceProvider _serviceProvider;

    private IUnitOfWork _unitOfWork;
    private IQueryRepository _queryRepository;

    private PaginationSpecification<ReceiptEntity> _specification;
    private ReceiptSearchModel request;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        
        request = new ReceiptSearchModel
        {
            PageIndex = 1,
            PageSize = 100,
            PaymentType = PaymentType.Cash
        };

        _specification = ReceiptSpecification.SearchSpecification(request);
        _specification.Includes = o => o
            .Include(x => x.ReceiptLines)
            .ThenInclude(x => x.ReceiptLineTags);
        
        var services = new ServiceCollection();

        services.AddDbContextPool<CoreDbContext>(o => o.UseSqlServer());

        services.AddScoped<ICoreDbContext, CoreDbContext>();
        services.AddScoped<IReceiptRepository, ReceiptRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddGenericRepository<CoreDbContext>();
        services.AddQueryRepository<CoreDbContext>();

        _serviceProvider = services.BuildServiceProvider();

        _unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
        _queryRepository = _serviceProvider.GetRequiredService<IQueryRepository>();
    }
    
    [GlobalCleanup]
    public void GlobalCleanup()
    {
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    [Benchmark]
    public async Task CoreRepo()
    {
        var a = await _unitOfWork.Receipt.Search(request);
    }

    [Benchmark]
    public async Task ModernRepo()
    {
       var b = await _queryRepository.GetListAsync(_specification);
    }
}