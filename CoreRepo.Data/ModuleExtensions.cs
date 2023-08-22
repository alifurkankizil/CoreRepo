using CoreRepo.Data.Infrastructure;
using CoreRepo.Data.Receipt;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreRepo.Data;

public static class ModuleExtensions
{
    public static IServiceCollection AddDbModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<CoreDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("CoreDB")));
        return services;
    }
    public static IApplicationBuilder UseDbModule(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<CoreDbContext>();
        context!.Database.Migrate();
        return app;
    }
    public static IApplicationBuilder UseFakeData(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<CoreDbContext>();
        context!.Database.Migrate();
        
        if (context.Receipts.Any()) return app;

        // var bulkConfig = new BulkConfig { IncludeGraph = true };
        // for (int i = 0; i < 25; i++)
        // {
        //     var receipts = FakeData.GetReceipts(2000);
        //     context.BulkInsert(receipts,bulkConfig);
        //     //context.Receipts.AddRange(receipts);
        // }
        //
        // //context.SaveChanges();
        // context.BulkSaveChanges();

        context.Receipts.AddRange(FakeData.GetReceipts(500));
        context.SaveChanges();
        
        return app;
    }

    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<ICoreDbContext, CoreDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IReceiptRepository, ReceiptRepository>();
        
        return services;
    }

}