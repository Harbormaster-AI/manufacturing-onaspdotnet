using manufacturingonaspdotnet.Api;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Telemetry;
using OpenTelemetry.Metrics;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

var dbEngineEnvironment = builder.Configuration["DB_TYPE"];

string dbEngine;
string dbUserName;
string dbPassword;
string dbName;
string dbHost;
string dbPort;

if (string.IsNullOrWhiteSpace(dbEngineEnvironment))
{
    // Generation-time configuration
    dbEngine = "mysql";
    dbUserName = "postgres";
    dbPassword = "no_password";
    dbName = "testDb";
    dbHost = "localhost";
    dbPort = "5432";
}
else
{
    // Runtime environment configuration
    dbEngine = dbEngineEnvironment;
    dbUserName = builder.Configuration["DB_USER_NAME"]
        ?? throw new InvalidOperationException("DB_USER_NAME is required when DB_ENGINE is provided.");

    dbPassword = builder.Configuration["DB_PASSWORD"]
        ?? throw new InvalidOperationException("DB_PASSWORD is required when DB_ENGINE is provided.");

    dbName = builder.Configuration["DB_NAME"]
        ?? throw new InvalidOperationException("DB_NAME is required when DB_ENGINE is provided.");

    dbHost = builder.Configuration["DB_HOST"]
        ?? throw new InvalidOperationException("DB_HOST is required when DB_ENGINE is provided.");

    dbPort = builder.Configuration["DB_PORT"]
        ?? throw new InvalidOperationException("DB_PORT is required when DB_ENGINE is provided.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString =
        $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUserName};Password={dbPassword}";

    if (dbEngine.Equals("mysql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("mariadb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlite(connectionString);
    }
    else if (dbEngine.Equals("sqlserver", StringComparison.OrdinalIgnoreCase) ||
             dbEngine.Equals("azuresql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlServer(connectionString);
    }
    else if (dbEngine.Equals("oracle", StringComparison.OrdinalIgnoreCase))
    {
        options.UseOracle(connectionString);
    }
    else if (dbEngine.Equals("mongodb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMongoDB(connectionString);
    }
    else if (dbEngine.Equals("inmemory", StringComparison.OrdinalIgnoreCase))
    {
        options.UseInMemoryDatabase("manufacturingonaspdotnet");
    }
    else if (dbEngine.Equals("cosmosdb", StringComparison.OrdinalIgnoreCase))
    {
        var cosmosEndpoint = builder.Configuration["DB_ENDPOINT"]
            ?? throw new InvalidOperationException(
                "DB_ENDPOINT is required for Cosmos DB.");

        var cosmosKey = builder.Configuration["DB_KEY"]
            ?? throw new InvalidOperationException(
                "DB_KEY is required for Cosmos DB.");

        options.UseCosmos(
            cosmosEndpoint,
            cosmosKey,
            dbName);
    }
    else if (dbEngine.Equals("postgres", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        throw new InvalidOperationException(
            "Unsupported or missing database engine configuration.");
    }
    // etc.
});

    builder.Services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
    builder.Services.AddScoped<IBusinessUnitRepository, BusinessUnitRepository>();
    builder.Services.AddScoped<IPlantRepository, PlantRepository>();
    builder.Services.AddScoped<IProductionLineRepository, ProductionLineRepository>();
    builder.Services.AddScoped<IWorkCenterRepository, WorkCenterRepository>();
    builder.Services.AddScoped<IItemRepository, ItemRepository>();
    builder.Services.AddScoped<IBOMRepository, BOMRepository>();
    builder.Services.AddScoped<IBOMItemRepository, BOMItemRepository>();
    builder.Services.AddScoped<IRoutingRepository, RoutingRepository>();
    builder.Services.AddScoped<IOperationRepository, OperationRepository>();
    builder.Services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
    builder.Services.AddScoped<IProductionScheduleRepository, ProductionScheduleRepository>();
    builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
    builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
    builder.Services.AddScoped<IPurchaseOrderLineRepository, PurchaseOrderLineRepository>();
    builder.Services.AddScoped<IGoodsReceiptRepository, GoodsReceiptRepository>();
    builder.Services.AddScoped<IGoodsReceiptLineRepository, GoodsReceiptLineRepository>();
    builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
    builder.Services.AddScoped<ILocationRepository, LocationRepository>();
    builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
    builder.Services.AddScoped<IInventoryTransactionRepository, InventoryTransactionRepository>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();
    builder.Services.AddScoped<ISalesOrderLineRepository, SalesOrderLineRepository>();
    builder.Services.AddScoped<IQualitySpecificationRepository, QualitySpecificationRepository>();
    builder.Services.AddScoped<IInspectionPlanRepository, InspectionPlanRepository>();
    builder.Services.AddScoped<IInspectionCharacteristicRepository, InspectionCharacteristicRepository>();
    builder.Services.AddScoped<IInspectionLotRepository, InspectionLotRepository>();
    builder.Services.AddScoped<IInspectionResultRepository, InspectionResultRepository>();
    builder.Services.AddScoped<INonconformanceRepository, NonconformanceRepository>();
    builder.Services.AddScoped<ICorrectiveActionRepository, CorrectiveActionRepository>();
    builder.Services.AddScoped<IAssetRepository, AssetRepository>();
    builder.Services.AddScoped<IMaintenancePlanRepository, MaintenancePlanRepository>();
    builder.Services.AddScoped<IMaintenanceOrderRepository, MaintenanceOrderRepository>();
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
    builder.Services.AddScoped<IShiftAssignmentRepository, ShiftAssignmentRepository>();
    builder.Services.AddScoped<IForecastRepository, ForecastRepository>();
    builder.Services.AddScoped<IForecastLineRepository, ForecastLineRepository>();
    builder.Services.AddScoped<IMRPRunRepository, MRPRunRepository>();
    builder.Services.AddScoped<IPlannedOrderRepository, PlannedOrderRepository>();

    builder.Services.AddScoped<IEnterpriseService, EnterpriseService>();
    builder.Services.AddScoped<IBusinessUnitService, BusinessUnitService>();
    builder.Services.AddScoped<IPlantService, PlantService>();
    builder.Services.AddScoped<IProductionLineService, ProductionLineService>();
    builder.Services.AddScoped<IWorkCenterService, WorkCenterService>();
    builder.Services.AddScoped<IItemService, ItemService>();
    builder.Services.AddScoped<IBOMService, BOMService>();
    builder.Services.AddScoped<IBOMItemService, BOMItemService>();
    builder.Services.AddScoped<IRoutingService, RoutingService>();
    builder.Services.AddScoped<IOperationService, OperationService>();
    builder.Services.AddScoped<IWorkOrderService, WorkOrderService>();
    builder.Services.AddScoped<IProductionScheduleService, ProductionScheduleService>();
    builder.Services.AddScoped<ISupplierService, SupplierService>();
    builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
    builder.Services.AddScoped<IPurchaseOrderLineService, PurchaseOrderLineService>();
    builder.Services.AddScoped<IGoodsReceiptService, GoodsReceiptService>();
    builder.Services.AddScoped<IGoodsReceiptLineService, GoodsReceiptLineService>();
    builder.Services.AddScoped<IWarehouseService, WarehouseService>();
    builder.Services.AddScoped<ILocationService, LocationService>();
    builder.Services.AddScoped<IInventoryItemService, InventoryItemService>();
    builder.Services.AddScoped<IInventoryTransactionService, InventoryTransactionService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<ISalesOrderService, SalesOrderService>();
    builder.Services.AddScoped<ISalesOrderLineService, SalesOrderLineService>();
    builder.Services.AddScoped<IQualitySpecificationService, QualitySpecificationService>();
    builder.Services.AddScoped<IInspectionPlanService, InspectionPlanService>();
    builder.Services.AddScoped<IInspectionCharacteristicService, InspectionCharacteristicService>();
    builder.Services.AddScoped<IInspectionLotService, InspectionLotService>();
    builder.Services.AddScoped<IInspectionResultService, InspectionResultService>();
    builder.Services.AddScoped<INonconformanceService, NonconformanceService>();
    builder.Services.AddScoped<ICorrectiveActionService, CorrectiveActionService>();
    builder.Services.AddScoped<IAssetService, AssetService>();
    builder.Services.AddScoped<IMaintenancePlanService, MaintenancePlanService>();
    builder.Services.AddScoped<IMaintenanceOrderService, MaintenanceOrderService>();
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    builder.Services.AddScoped<IShiftService, ShiftService>();
    builder.Services.AddScoped<IShiftAssignmentService, ShiftAssignmentService>();
    builder.Services.AddScoped<IForecastService, ForecastService>();
    builder.Services.AddScoped<IForecastLineService, ForecastLineService>();
    builder.Services.AddScoped<IMRPRunService, MRPRunService>();
    builder.Services.AddScoped<IPlannedOrderService, PlannedOrderService>();

// apply the service resolver
builder.Services.AddScoped<IServiceResolver, ServiceResolver>();

builder.Services.AddSingleton<ApplicationTelemetry>();

builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddMeter("Harbormaster.Application");

        metrics.AddPrometheusExporter();
    });

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();


// Health endpoint
app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Testing uses a shared SQLite file; wipe it so unique indexes (e.g. email) don't fail on re-runs.
    if (app.Environment.IsEnvironment("Testing"))
    {
        db.Database.EnsureDeleted();
    }

    // EnsureCreated does not alter an existing schema. If you changed relationships locally,
    // recreate the MySQL database (docker compose down -v && docker compose up -d).
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


    app.MapEnterpriseEndpoints();
    app.MapBusinessUnitEndpoints();
    app.MapPlantEndpoints();
    app.MapProductionLineEndpoints();
    app.MapWorkCenterEndpoints();
    app.MapItemEndpoints();
    app.MapBOMEndpoints();
    app.MapBOMItemEndpoints();
    app.MapRoutingEndpoints();
    app.MapOperationEndpoints();
    app.MapWorkOrderEndpoints();
    app.MapProductionScheduleEndpoints();
    app.MapSupplierEndpoints();
    app.MapPurchaseOrderEndpoints();
    app.MapPurchaseOrderLineEndpoints();
    app.MapGoodsReceiptEndpoints();
    app.MapGoodsReceiptLineEndpoints();
    app.MapWarehouseEndpoints();
    app.MapLocationEndpoints();
    app.MapInventoryItemEndpoints();
    app.MapInventoryTransactionEndpoints();
    app.MapCustomerEndpoints();
    app.MapSalesOrderEndpoints();
    app.MapSalesOrderLineEndpoints();
    app.MapQualitySpecificationEndpoints();
    app.MapInspectionPlanEndpoints();
    app.MapInspectionCharacteristicEndpoints();
    app.MapInspectionLotEndpoints();
    app.MapInspectionResultEndpoints();
    app.MapNonconformanceEndpoints();
    app.MapCorrectiveActionEndpoints();
    app.MapAssetEndpoints();
    app.MapMaintenancePlanEndpoints();
    app.MapMaintenanceOrderEndpoints();
    app.MapEmployeeEndpoints();
    app.MapShiftEndpoints();
    app.MapShiftAssignmentEndpoints();
    app.MapForecastEndpoints();
    app.MapForecastLineEndpoints();
    app.MapMRPRunEndpoints();
    app.MapPlannedOrderEndpoints();

app.Run();

public partial class Program { }

