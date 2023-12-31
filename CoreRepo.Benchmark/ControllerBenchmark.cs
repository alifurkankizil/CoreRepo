using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using CoreRepo.Api.Controllers;
using CoreRepo.Api.Models;
using CoreRepo.Api.Profile;
using CoreRepo.Core.Enums;
using CoreRepo.Data;
using CoreRepo.Data.Infrastructure;
using CoreRepo.Data.Models;
using CoreRepo.Data.Receipt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TanvirArjel.EFCore.GenericRepository;

namespace CoreRepo.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[SimpleJob(RunStrategy.ColdStart, launchCount:50)]
public class ControllerBenchmark
{
    private ReceiptController _receiptController;
    private IServiceProvider _serviceProvider;
    private readonly Guid _receiptId = Guid.Parse("f70cc102-c3fd-2767-92c3-039ca4cad6df");
    
    private readonly ReceiptSearchModel _searchModel = new ReceiptSearchModel
    {
        Date = new DateTimeOffset(new DateTime(2023, 1, 1)),
        PaymentType = PaymentType.Cash,
        CurrencyType = CurrencyType.Try,
        PageIndex = 1,
        PageSize = 100
    };
    
    
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
    public async Task GetByIdV1()
    {
        await _receiptController.GetByIdV1(_receiptId);
    }
    
    [Benchmark]
    public async Task GetByIdV2()
    {
        await _receiptController.GetByIdV2(_receiptId);
    }
    
    [Benchmark]
    public async Task GetByIdDetailV1()
    {
        await _receiptController.GetByIdDetailV1(_receiptId);
    }
    
    [Benchmark]
    public async Task GetByIdDetailV2()
    {
        await _receiptController.GetByIdDetailV2(_receiptId);
    }
    
    [Benchmark]
    public async Task SearchV1()
    {
        await _receiptController.SearchV1(_searchModel);
    }
    
    [Benchmark]
    public async Task SearchV2()
    {
        await _receiptController.SearchV2(_searchModel);
    }
    
    [Benchmark]
    public async Task DetailSearchV1()
    {
        await _receiptController.DetailSearchV1(_searchModel);
    }
    
    [Benchmark]
    public async Task DetailSearchV2()
    {
        await _receiptController.DetailSearchV2(_searchModel);
    }
}

