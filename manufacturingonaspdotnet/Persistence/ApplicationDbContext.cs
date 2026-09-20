using Microsoft.EntityFrameworkCore;

using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<Enterprise> Enterprises => Set<Enterprise>();
public DbSet<BusinessUnit> BusinessUnits => Set<BusinessUnit>();
public DbSet<Plant> Plants => Set<Plant>();
public DbSet<ProductionLine> ProductionLines => Set<ProductionLine>();
public DbSet<WorkCenter> WorkCenters => Set<WorkCenter>();
public DbSet<Item> Items => Set<Item>();
public DbSet<BOM> BOMs => Set<BOM>();
public DbSet<BOMItem> BOMItems => Set<BOMItem>();
public DbSet<Routing> Routings => Set<Routing>();
public DbSet<Operation> Operations => Set<Operation>();
public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
public DbSet<ProductionSchedule> ProductionSchedules => Set<ProductionSchedule>();
public DbSet<Supplier> Suppliers => Set<Supplier>();
public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
public DbSet<PurchaseOrderLine> PurchaseOrderLines => Set<PurchaseOrderLine>();
public DbSet<GoodsReceipt> GoodsReceipts => Set<GoodsReceipt>();
public DbSet<GoodsReceiptLine> GoodsReceiptLines => Set<GoodsReceiptLine>();
public DbSet<Warehouse> Warehouses => Set<Warehouse>();
public DbSet<Location> Locations => Set<Location>();
public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
public DbSet<InventoryTransaction> InventoryTransactions => Set<InventoryTransaction>();
public DbSet<Customer> Customers => Set<Customer>();
public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
public DbSet<SalesOrderLine> SalesOrderLines => Set<SalesOrderLine>();
public DbSet<QualitySpecification> QualitySpecifications => Set<QualitySpecification>();
public DbSet<InspectionPlan> InspectionPlans => Set<InspectionPlan>();
public DbSet<InspectionCharacteristic> InspectionCharacteristics => Set<InspectionCharacteristic>();
public DbSet<InspectionLot> InspectionLots => Set<InspectionLot>();
public DbSet<InspectionResult> InspectionResults => Set<InspectionResult>();
public DbSet<Nonconformance> Nonconformances => Set<Nonconformance>();
public DbSet<CorrectiveAction> CorrectiveActions => Set<CorrectiveAction>();
public DbSet<Asset> Assets => Set<Asset>();
public DbSet<MaintenancePlan> MaintenancePlans => Set<MaintenancePlan>();
public DbSet<MaintenanceOrder> MaintenanceOrders => Set<MaintenanceOrder>();
public DbSet<Employee> Employees => Set<Employee>();
public DbSet<Shift> Shifts => Set<Shift>();
public DbSet<ShiftAssignment> ShiftAssignments => Set<ShiftAssignment>();
public DbSet<Forecast> Forecasts => Set<Forecast>();
public DbSet<ForecastLine> ForecastLines => Set<ForecastLine>();
public DbSet<MRPRun> MRPRuns => Set<MRPRun>();
public DbSet<PlannedOrder> PlannedOrders => Set<PlannedOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Enterprise has one or more BusinessUnits of type BusinessUnit
        modelBuilder.Entity<BusinessUnit>()
            .HasOne<Enterprise>()
            .WithMany(parent => parent.BusinessUnits)
            .HasForeignKey("BusinessUnitsId");

        // Enterprise has one or more Plants of type Plant
        modelBuilder.Entity<Plant>()
            .HasOne<Enterprise>()
            .WithMany(parent => parent.Plants)
            .HasForeignKey("PlantsId");

        // Enterprise has one or more Suppliers of type Supplier
        modelBuilder.Entity<Supplier>()
            .HasOne<Enterprise>()
            .WithMany(parent => parent.Suppliers)
            .HasForeignKey("SuppliersId");

        // Enterprise has one or more Customers of type Customer
        modelBuilder.Entity<Customer>()
            .HasOne<Enterprise>()
            .WithMany(parent => parent.Customers)
            .HasForeignKey("CustomersId");

        // BusinessUnit has one Enterprise of type Enterprise
        modelBuilder.Entity<BusinessUnit>()
            .HasOne(x => x.Enterprise)
            .WithMany()
            .HasForeignKey("EnterpriseId");


        // BusinessUnit has one or more Items of type Item
        modelBuilder.Entity<Item>()
            .HasOne<BusinessUnit>()
            .WithMany(parent => parent.Items)
            .HasForeignKey("ItemsId");

        // BusinessUnit has one or more Plants of type Plant
        modelBuilder.Entity<Plant>()
            .HasOne<BusinessUnit>()
            .WithMany(parent => parent.Plants)
            .HasForeignKey("PlantsId");

        // Plant has one Enterprise of type Enterprise
        modelBuilder.Entity<Plant>()
            .HasOne(x => x.Enterprise)
            .WithMany()
            .HasForeignKey("EnterpriseId");


        // Plant has one or more ProductionLines of type ProductionLine
        modelBuilder.Entity<ProductionLine>()
            .HasOne<Plant>()
            .WithMany(parent => parent.ProductionLines)
            .HasForeignKey("ProductionLinesId");

        // Plant has one or more WorkCenters of type WorkCenter
        modelBuilder.Entity<WorkCenter>()
            .HasOne<Plant>()
            .WithMany(parent => parent.WorkCenters)
            .HasForeignKey("WorkCentersId");

        // Plant has one or more Warehouses of type Warehouse
        modelBuilder.Entity<Warehouse>()
            .HasOne<Plant>()
            .WithMany(parent => parent.Warehouses)
            .HasForeignKey("WarehousesId");

        // Plant has one or more Assets of type Asset
        modelBuilder.Entity<Asset>()
            .HasOne<Plant>()
            .WithMany(parent => parent.Assets)
            .HasForeignKey("AssetsId");

        // Plant has one or more ProductionSchedules of type ProductionSchedule
        modelBuilder.Entity<ProductionSchedule>()
            .HasOne<Plant>()
            .WithMany(parent => parent.ProductionSchedules)
            .HasForeignKey("ProductionSchedulesId");

        // ProductionLine has one Plant of type Plant
        modelBuilder.Entity<ProductionLine>()
            .HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey("PlantId");


        // ProductionLine has one or more WorkCenters of type WorkCenter
        modelBuilder.Entity<WorkCenter>()
            .HasOne<ProductionLine>()
            .WithMany(parent => parent.WorkCenters)
            .HasForeignKey("WorkCentersId");

        // WorkCenter has one ProductionLine of type ProductionLine
        modelBuilder.Entity<WorkCenter>()
            .HasOne(x => x.ProductionLine)
            .WithMany()
            .HasForeignKey("ProductionLineId");


        // WorkCenter has one or more Assets of type Asset
        modelBuilder.Entity<Asset>()
            .HasOne<WorkCenter>()
            .WithMany(parent => parent.Assets)
            .HasForeignKey("AssetsId");

        // WorkCenter has one or more MaintenanceOrders of type MaintenanceOrder
        modelBuilder.Entity<MaintenanceOrder>()
            .HasOne<WorkCenter>()
            .WithMany(parent => parent.MaintenanceOrders)
            .HasForeignKey("MaintenanceOrdersId");

        // Item has one BusinessUnit of type BusinessUnit
        modelBuilder.Entity<Item>()
            .HasOne(x => x.BusinessUnit)
            .WithMany()
            .HasForeignKey("BusinessUnitId");


        // Item has one or more Boms of type BOM
        modelBuilder.Entity<BOM>()
            .HasOne<Item>()
            .WithMany(parent => parent.Boms)
            .HasForeignKey("BomsId");

        // Item has one or more Routings of type Routing
        modelBuilder.Entity<Routing>()
            .HasOne<Item>()
            .WithMany(parent => parent.Routings)
            .HasForeignKey("RoutingsId");

        // Item has one or more Suppliers of type Supplier
        modelBuilder.Entity<Supplier>()
            .HasOne<Item>()
            .WithMany(parent => parent.Suppliers)
            .HasForeignKey("SuppliersId");

        // Item has one or more QualitySpecifications of type QualitySpecification
        modelBuilder.Entity<QualitySpecification>()
            .HasOne<Item>()
            .WithMany(parent => parent.QualitySpecifications)
            .HasForeignKey("QualitySpecificationsId");

        // Item has one or more InventoryItems of type InventoryItem
        modelBuilder.Entity<InventoryItem>()
            .HasOne<Item>()
            .WithMany(parent => parent.InventoryItems)
            .HasForeignKey("InventoryItemsId");

        // BOM has one ParentItem of type Item
        modelBuilder.Entity<BOM>()
            .HasOne(x => x.ParentItem)
            .WithMany()
            .HasForeignKey("ParentItemId");


        // BOM has one or more BomItems of type BOMItem
        modelBuilder.Entity<BOMItem>()
            .HasOne<BOM>()
            .WithMany(parent => parent.BomItems)
            .HasForeignKey("BomItemsId");

        // BOMItem has one Bom of type BOM
        modelBuilder.Entity<BOMItem>()
            .HasOne(x => x.Bom)
            .WithMany()
            .HasForeignKey("BomId");

        // BOMItem has one Component of type Item
        modelBuilder.Entity<BOMItem>()
            .HasOne(x => x.Component)
            .WithMany()
            .HasForeignKey("ComponentId");


        // Routing has one Item of type Item
        modelBuilder.Entity<Routing>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");


        // Routing has one or more Operations of type Operation
        modelBuilder.Entity<Operation>()
            .HasOne<Routing>()
            .WithMany(parent => parent.Operations)
            .HasForeignKey("OperationsId");

        // Operation has one Routing of type Routing
        modelBuilder.Entity<Operation>()
            .HasOne(x => x.Routing)
            .WithMany()
            .HasForeignKey("RoutingId");

        // Operation has one WorkCenter of type WorkCenter
        modelBuilder.Entity<Operation>()
            .HasOne(x => x.WorkCenter)
            .WithMany()
            .HasForeignKey("WorkCenterId");

        // Operation has one InspectionPlan of type InspectionPlan
        modelBuilder.Entity<Operation>()
            .HasOne(x => x.InspectionPlan)
            .WithMany()
            .HasForeignKey("InspectionPlanId");


        // WorkOrder has one Item of type Item
        modelBuilder.Entity<WorkOrder>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");

        // WorkOrder has one Plant of type Plant
        modelBuilder.Entity<WorkOrder>()
            .HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey("PlantId");

        // WorkOrder has one Routing of type Routing
        modelBuilder.Entity<WorkOrder>()
            .HasOne(x => x.Routing)
            .WithMany()
            .HasForeignKey("RoutingId");

        // WorkOrder has one Bom of type BOM
        modelBuilder.Entity<WorkOrder>()
            .HasOne(x => x.Bom)
            .WithMany()
            .HasForeignKey("BomId");

        // WorkOrder has one ProductionSchedule of type ProductionSchedule
        modelBuilder.Entity<WorkOrder>()
            .HasOne(x => x.ProductionSchedule)
            .WithMany()
            .HasForeignKey("ProductionScheduleId");

        // WorkOrder has one SalesOrder of type SalesOrder
        modelBuilder.Entity<WorkOrder>()
            .HasOne(x => x.SalesOrder)
            .WithMany()
            .HasForeignKey("SalesOrderId");


        // ProductionSchedule has one Plant of type Plant
        modelBuilder.Entity<ProductionSchedule>()
            .HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey("PlantId");


        // ProductionSchedule has one or more WorkOrders of type WorkOrder
        modelBuilder.Entity<WorkOrder>()
            .HasOne<ProductionSchedule>()
            .WithMany(parent => parent.WorkOrders)
            .HasForeignKey("WorkOrdersId");


        // Supplier has one or more Enterprises of type Enterprise
        modelBuilder.Entity<Enterprise>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.Enterprises)
            .HasForeignKey("EnterprisesId");

        // Supplier has one or more Items of type Item
        modelBuilder.Entity<Item>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.Items)
            .HasForeignKey("ItemsId");

        // Supplier has one or more PurchaseOrders of type PurchaseOrder
        modelBuilder.Entity<PurchaseOrder>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.PurchaseOrders)
            .HasForeignKey("PurchaseOrdersId");

        // PurchaseOrder has one Supplier of type Supplier
        modelBuilder.Entity<PurchaseOrder>()
            .HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey("SupplierId");

        // PurchaseOrder has one Plant of type Plant
        modelBuilder.Entity<PurchaseOrder>()
            .HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey("PlantId");


        // PurchaseOrder has one or more Lines of type PurchaseOrderLine
        modelBuilder.Entity<PurchaseOrderLine>()
            .HasOne<PurchaseOrder>()
            .WithMany(parent => parent.Lines)
            .HasForeignKey("LinesId");

        // PurchaseOrder has one or more GoodsReceipts of type GoodsReceipt
        modelBuilder.Entity<GoodsReceipt>()
            .HasOne<PurchaseOrder>()
            .WithMany(parent => parent.GoodsReceipts)
            .HasForeignKey("GoodsReceiptsId");

        // PurchaseOrderLine has one PurchaseOrder of type PurchaseOrder
        modelBuilder.Entity<PurchaseOrderLine>()
            .HasOne(x => x.PurchaseOrder)
            .WithMany()
            .HasForeignKey("PurchaseOrderId");

        // PurchaseOrderLine has one Item of type Item
        modelBuilder.Entity<PurchaseOrderLine>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");


        // GoodsReceipt has one PurchaseOrder of type PurchaseOrder
        modelBuilder.Entity<GoodsReceipt>()
            .HasOne(x => x.PurchaseOrder)
            .WithMany()
            .HasForeignKey("PurchaseOrderId");

        // GoodsReceipt has one Warehouse of type Warehouse
        modelBuilder.Entity<GoodsReceipt>()
            .HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey("WarehouseId");


        // GoodsReceipt has one or more Lines of type GoodsReceiptLine
        modelBuilder.Entity<GoodsReceiptLine>()
            .HasOne<GoodsReceipt>()
            .WithMany(parent => parent.Lines)
            .HasForeignKey("LinesId");

        // GoodsReceiptLine has one GoodsReceipt of type GoodsReceipt
        modelBuilder.Entity<GoodsReceiptLine>()
            .HasOne(x => x.GoodsReceipt)
            .WithMany()
            .HasForeignKey("GoodsReceiptId");

        // GoodsReceiptLine has one Item of type Item
        modelBuilder.Entity<GoodsReceiptLine>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");

        // GoodsReceiptLine has one InventoryTransaction of type InventoryTransaction
        modelBuilder.Entity<GoodsReceiptLine>()
            .HasOne(x => x.InventoryTransaction)
            .WithMany()
            .HasForeignKey("InventoryTransactionId");


        // Warehouse has one Plant of type Plant
        modelBuilder.Entity<Warehouse>()
            .HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey("PlantId");


        // Warehouse has one or more Locations of type Location
        modelBuilder.Entity<Location>()
            .HasOne<Warehouse>()
            .WithMany(parent => parent.Locations)
            .HasForeignKey("LocationsId");

        // Warehouse has one or more InventoryItems of type InventoryItem
        modelBuilder.Entity<InventoryItem>()
            .HasOne<Warehouse>()
            .WithMany(parent => parent.InventoryItems)
            .HasForeignKey("InventoryItemsId");

        // Location has one Warehouse of type Warehouse
        modelBuilder.Entity<Location>()
            .HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey("WarehouseId");


        // Location has one or more InventoryItems of type InventoryItem
        modelBuilder.Entity<InventoryItem>()
            .HasOne<Location>()
            .WithMany(parent => parent.InventoryItems)
            .HasForeignKey("InventoryItemsId");

        // InventoryItem has one Item of type Item
        modelBuilder.Entity<InventoryItem>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");

        // InventoryItem has one Location of type Location
        modelBuilder.Entity<InventoryItem>()
            .HasOne(x => x.Location)
            .WithMany()
            .HasForeignKey("LocationId");


        // InventoryTransaction has one Item of type Item
        modelBuilder.Entity<InventoryTransaction>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");

        // InventoryTransaction has one Location of type Location
        modelBuilder.Entity<InventoryTransaction>()
            .HasOne(x => x.Location)
            .WithMany()
            .HasForeignKey("LocationId");

        // InventoryTransaction has one WorkOrder of type WorkOrder
        modelBuilder.Entity<InventoryTransaction>()
            .HasOne(x => x.WorkOrder)
            .WithMany()
            .HasForeignKey("WorkOrderId");

        // InventoryTransaction has one PurchaseOrder of type PurchaseOrder
        modelBuilder.Entity<InventoryTransaction>()
            .HasOne(x => x.PurchaseOrder)
            .WithMany()
            .HasForeignKey("PurchaseOrderId");

        // InventoryTransaction has one SalesOrder of type SalesOrder
        modelBuilder.Entity<InventoryTransaction>()
            .HasOne(x => x.SalesOrder)
            .WithMany()
            .HasForeignKey("SalesOrderId");



        // Customer has one or more Enterprises of type Enterprise
        modelBuilder.Entity<Enterprise>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Enterprises)
            .HasForeignKey("EnterprisesId");

        // Customer has one or more SalesOrders of type SalesOrder
        modelBuilder.Entity<SalesOrder>()
            .HasOne<Customer>()
            .WithMany(parent => parent.SalesOrders)
            .HasForeignKey("SalesOrdersId");

        // SalesOrder has one Customer of type Customer
        modelBuilder.Entity<SalesOrder>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");

        // SalesOrder has one Plant of type Plant
        modelBuilder.Entity<SalesOrder>()
            .HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey("PlantId");


        // SalesOrder has one or more Lines of type SalesOrderLine
        modelBuilder.Entity<SalesOrderLine>()
            .HasOne<SalesOrder>()
            .WithMany(parent => parent.Lines)
            .HasForeignKey("LinesId");

        // SalesOrder has one or more WorkOrders of type WorkOrder
        modelBuilder.Entity<WorkOrder>()
            .HasOne<SalesOrder>()
            .WithMany(parent => parent.WorkOrders)
            .HasForeignKey("WorkOrdersId");

        // SalesOrderLine has one SalesOrder of type SalesOrder
        modelBuilder.Entity<SalesOrderLine>()
            .HasOne(x => x.SalesOrder)
            .WithMany()
            .HasForeignKey("SalesOrderId");

        // SalesOrderLine has one Item of type Item
        modelBuilder.Entity<SalesOrderLine>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");


        // QualitySpecification has one Item of type Item
        modelBuilder.Entity<QualitySpecification>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");


        // InspectionPlan has one Item of type Item
        modelBuilder.Entity<InspectionPlan>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");


        // InspectionPlan has one or more Characteristics of type InspectionCharacteristic
        modelBuilder.Entity<InspectionCharacteristic>()
            .HasOne<InspectionPlan>()
            .WithMany(parent => parent.Characteristics)
            .HasForeignKey("CharacteristicsId");

        // InspectionCharacteristic has one InspectionPlan of type InspectionPlan
        modelBuilder.Entity<InspectionCharacteristic>()
            .HasOne(x => x.InspectionPlan)
            .WithMany()
            .HasForeignKey("InspectionPlanId");


        // InspectionLot has one Item of type Item
        modelBuilder.Entity<InspectionLot>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");

        // InspectionLot has one WorkOrder of type WorkOrder
        modelBuilder.Entity<InspectionLot>()
            .HasOne(x => x.WorkOrder)
            .WithMany()
            .HasForeignKey("WorkOrderId");

        // InspectionLot has one GoodsReceipt of type GoodsReceipt
        modelBuilder.Entity<InspectionLot>()
            .HasOne(x => x.GoodsReceipt)
            .WithMany()
            .HasForeignKey("GoodsReceiptId");


        // InspectionLot has one or more Results of type InspectionResult
        modelBuilder.Entity<InspectionResult>()
            .HasOne<InspectionLot>()
            .WithMany(parent => parent.Results)
            .HasForeignKey("ResultsId");

        // InspectionResult has one InspectionLot of type InspectionLot
        modelBuilder.Entity<InspectionResult>()
            .HasOne(x => x.InspectionLot)
            .WithMany()
            .HasForeignKey("InspectionLotId");

        // InspectionResult has one Characteristic of type InspectionCharacteristic
        modelBuilder.Entity<InspectionResult>()
            .HasOne(x => x.Characteristic)
            .WithMany()
            .HasForeignKey("CharacteristicId");


        // Nonconformance has one Item of type Item
        modelBuilder.Entity<Nonconformance>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");

        // Nonconformance has one WorkOrder of type WorkOrder
        modelBuilder.Entity<Nonconformance>()
            .HasOne(x => x.WorkOrder)
            .WithMany()
            .HasForeignKey("WorkOrderId");

        // Nonconformance has one InspectionLot of type InspectionLot
        modelBuilder.Entity<Nonconformance>()
            .HasOne(x => x.InspectionLot)
            .WithMany()
            .HasForeignKey("InspectionLotId");

        // Nonconformance has one CorrectiveAction of type CorrectiveAction
        modelBuilder.Entity<Nonconformance>()
            .HasOne(x => x.CorrectiveAction)
            .WithMany()
            .HasForeignKey("CorrectiveActionId");


        // CorrectiveAction has one Nonconformance of type Nonconformance
        modelBuilder.Entity<CorrectiveAction>()
            .HasOne(x => x.Nonconformance)
            .WithMany()
            .HasForeignKey("NonconformanceId");

        // CorrectiveAction has one Owner of type Employee
        modelBuilder.Entity<CorrectiveAction>()
            .HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey("OwnerId");


        // Asset has one Plant of type Plant
        modelBuilder.Entity<Asset>()
            .HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey("PlantId");

        // Asset has one WorkCenter of type WorkCenter
        modelBuilder.Entity<Asset>()
            .HasOne(x => x.WorkCenter)
            .WithMany()
            .HasForeignKey("WorkCenterId");


        // Asset has one or more MaintenanceOrders of type MaintenanceOrder
        modelBuilder.Entity<MaintenanceOrder>()
            .HasOne<Asset>()
            .WithMany(parent => parent.MaintenanceOrders)
            .HasForeignKey("MaintenanceOrdersId");

        // Asset has one or more MaintenancePlans of type MaintenancePlan
        modelBuilder.Entity<MaintenancePlan>()
            .HasOne<Asset>()
            .WithMany(parent => parent.MaintenancePlans)
            .HasForeignKey("MaintenancePlansId");

        // MaintenancePlan has one Asset of type Asset
        modelBuilder.Entity<MaintenancePlan>()
            .HasOne(x => x.Asset)
            .WithMany()
            .HasForeignKey("AssetId");


        // MaintenancePlan has one or more MaintenanceOrders of type MaintenanceOrder
        modelBuilder.Entity<MaintenanceOrder>()
            .HasOne<MaintenancePlan>()
            .WithMany(parent => parent.MaintenanceOrders)
            .HasForeignKey("MaintenanceOrdersId");

        // MaintenanceOrder has one Asset of type Asset
        modelBuilder.Entity<MaintenanceOrder>()
            .HasOne(x => x.Asset)
            .WithMany()
            .HasForeignKey("AssetId");

        // MaintenanceOrder has one Plan of type MaintenancePlan
        modelBuilder.Entity<MaintenanceOrder>()
            .HasOne(x => x.Plan)
            .WithMany()
            .HasForeignKey("PlanId");

        // MaintenanceOrder has one WorkCenter of type WorkCenter
        modelBuilder.Entity<MaintenanceOrder>()
            .HasOne(x => x.WorkCenter)
            .WithMany()
            .HasForeignKey("WorkCenterId");


        // Employee has one WorkCenter of type WorkCenter
        modelBuilder.Entity<Employee>()
            .HasOne(x => x.WorkCenter)
            .WithMany()
            .HasForeignKey("WorkCenterId");


        // Employee has one or more ShiftAssignments of type ShiftAssignment
        modelBuilder.Entity<ShiftAssignment>()
            .HasOne<Employee>()
            .WithMany(parent => parent.ShiftAssignments)
            .HasForeignKey("ShiftAssignmentsId");

        // Employee has one or more CorrectiveActions of type CorrectiveAction
        modelBuilder.Entity<CorrectiveAction>()
            .HasOne<Employee>()
            .WithMany(parent => parent.CorrectiveActions)
            .HasForeignKey("CorrectiveActionsId");

        // Shift has one Plant of type Plant
        modelBuilder.Entity<Shift>()
            .HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey("PlantId");


        // Shift has one or more Assignments of type ShiftAssignment
        modelBuilder.Entity<ShiftAssignment>()
            .HasOne<Shift>()
            .WithMany(parent => parent.Assignments)
            .HasForeignKey("AssignmentsId");

        // ShiftAssignment has one Shift of type Shift
        modelBuilder.Entity<ShiftAssignment>()
            .HasOne(x => x.Shift)
            .WithMany()
            .HasForeignKey("ShiftId");

        // ShiftAssignment has one Employee of type Employee
        modelBuilder.Entity<ShiftAssignment>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");

        // ShiftAssignment has one WorkCenter of type WorkCenter
        modelBuilder.Entity<ShiftAssignment>()
            .HasOne(x => x.WorkCenter)
            .WithMany()
            .HasForeignKey("WorkCenterId");



        // Forecast has one or more Lines of type ForecastLine
        modelBuilder.Entity<ForecastLine>()
            .HasOne<Forecast>()
            .WithMany(parent => parent.Lines)
            .HasForeignKey("LinesId");

        // ForecastLine has one Forecast of type Forecast
        modelBuilder.Entity<ForecastLine>()
            .HasOne(x => x.Forecast)
            .WithMany()
            .HasForeignKey("ForecastId");

        // ForecastLine has one Item of type Item
        modelBuilder.Entity<ForecastLine>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");


        // MRPRun has one Plant of type Plant
        modelBuilder.Entity<MRPRun>()
            .HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey("PlantId");


        // MRPRun has one or more PlannedOrders of type PlannedOrder
        modelBuilder.Entity<PlannedOrder>()
            .HasOne<MRPRun>()
            .WithMany(parent => parent.PlannedOrders)
            .HasForeignKey("PlannedOrdersId");

        // PlannedOrder has one MrpRun of type MRPRun
        modelBuilder.Entity<PlannedOrder>()
            .HasOne(x => x.MrpRun)
            .WithMany()
            .HasForeignKey("MrpRunId");

        // PlannedOrder has one Item of type Item
        modelBuilder.Entity<PlannedOrder>()
            .HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey("ItemId");

        // PlannedOrder has one Plant of type Plant
        modelBuilder.Entity<PlannedOrder>()
            .HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey("PlantId");


    }
}
