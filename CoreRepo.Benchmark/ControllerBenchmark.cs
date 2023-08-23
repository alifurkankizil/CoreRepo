using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CoreRepo.Api.Controllers;
using CoreRepo.Api.Profile;
using CoreRepo.Data;
using CoreRepo.Data.Infrastructure;
using CoreRepo.Data.Receipt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TanvirArjel.EFCore.GenericRepository;

namespace CoreRepo.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ControllerBenchmark
{
    private IServiceProvider _serviceProvider;

    private ReceiptController _receiptController;

    [GlobalSetup]
    public void Setup()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.Development.json", true, true)
            .Build();
        
        var services = new ServiceCollection();

        services.AddDbContextPool<CoreDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("CoreDB")));

        services.AddScoped<ICoreDbContext, CoreDbContext>();
        services.AddScoped<IReceiptRepository, ReceiptRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddGenericRepository<CoreDbContext>();
        services.AddQueryRepository<CoreDbContext>();

        services.AddAutoMapper(typeof(ReceiptProfile));
        
        _serviceProvider = services.BuildServiceProvider();

        IUnitOfWork unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
        IQueryRepository queryRepository = _serviceProvider.GetRequiredService<IQueryRepository>();
        IRepository repository = _serviceProvider.GetRequiredService<IRepository>();
        IMapper mapper = _serviceProvider.GetRequiredService<IMapper>();

        _receiptController = new ReceiptController(unitOfWork, queryRepository, repository, mapper);
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
    public async Task A()
    {
        var b=await _receiptController.GetByIdV1(Guid.Parse("d581c0bf-e4ca-4684-f8bc-08db9d084e10"));
    }
    [Benchmark]
    public async Task A2()
    {
        var b = await _receiptController.GetByIdV2(Guid.Parse("d581c0bf-e4ca-4684-f8bc-08db9d084e10"));
    }
}

