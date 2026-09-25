using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Contracts;

public class IdentifierRequest
{
    public Guid Id { get; set; }
}

public class AssociationRequest
{
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
}

public class MultipleAssociationRequest
{
    public Guid ParentId { get; set; }
    public List<Guid> ChildIds { get; set; } = new();
}

public class EnterpriseRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? RegistrationCountry { get; set; } 
 public virtual string? Website { get; set; } 
 public virtual string? TaxId { get; set; } 
}

public class EnterpriseResponse : EnterpriseRequest {
    public static EnterpriseResponse FromModel(Enterprise model) {
        return new EnterpriseResponse {
            Id = model.Id,
            Name = model.Name,
            LegalName = model.LegalName,
            RegistrationCountry = model.RegistrationCountry,
            Website = model.Website,
            TaxId = model.TaxId,
        };
    }
}

public class BusinessUnitRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual BusinessUnitCategory? Category { get; set; } 
}

public class BusinessUnitResponse : BusinessUnitRequest {
    public static BusinessUnitResponse FromModel(BusinessUnit model) {
        return new BusinessUnitResponse {
            Id = model.Id,
            Name = model.Name,
            Code = model.Code,
            Category = model.Category,
        };
    }
}

public class PlantRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? PlantCode { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual string? TimeZone { get; set; } 
}

public class PlantResponse : PlantRequest {
    public static PlantResponse FromModel(Plant model) {
        return new PlantResponse {
            Id = model.Id,
            Name = model.Name,
            PlantCode = model.PlantCode,
            Address = model.Address,
            TimeZone = model.TimeZone,
        };
    }
}

public class ProductionLineRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LineCode { get; set; } 
 public virtual ProductionLineType? LineType { get; set; } 
}

public class ProductionLineResponse : ProductionLineRequest {
    public static ProductionLineResponse FromModel(ProductionLine model) {
        return new ProductionLineResponse {
            Id = model.Id,
            Name = model.Name,
            LineCode = model.LineCode,
            LineType = model.LineType,
        };
    }
}

public class WorkCenterRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual int? CapacityPerHour { get; set; } 
 public virtual Percentage? OeeTarget { get; set; } 
 public virtual WorkCenterType? WorkCenterType { get; set; } 
}

public class WorkCenterResponse : WorkCenterRequest {
    public static WorkCenterResponse FromModel(WorkCenter model) {
        return new WorkCenterResponse {
            Id = model.Id,
            Name = model.Name,
            Code = model.Code,
            CapacityPerHour = model.CapacityPerHour,
            OeeTarget = model.OeeTarget,
            WorkCenterType = model.WorkCenterType,
        };
    }
}

public class ItemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ItemNumber { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Money? StandardCost { get; set; } 
 public virtual Measurement? Weight { get; set; } 
 public virtual bool? AsSerialControlled { get; set; } 
 public virtual ItemType? ItemType { get; set; } 
 public virtual ProcurementType? ProcurementType { get; set; } 
 public virtual UnitOfMeasure? UnitOfMeasure { get; set; } 
 public virtual ProductLifecycleStatus? LifecycleStatus { get; set; } 
}

public class ItemResponse : ItemRequest {
    public static ItemResponse FromModel(Item model) {
        return new ItemResponse {
            Id = model.Id,
            ItemNumber = model.ItemNumber,
            Name = model.Name,
            StandardCost = model.StandardCost,
            Weight = model.Weight,
            AsSerialControlled = model.AsSerialControlled,
            ItemType = model.ItemType,
            ProcurementType = model.ProcurementType,
            UnitOfMeasure = model.UnitOfMeasure,
            LifecycleStatus = model.LifecycleStatus,
        };
    }
}

public class BOMRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? BomNumber { get; set; } 
 public virtual string? Revision { get; set; } 
 public virtual DateOnly? EffectivityStart { get; set; } 
 public virtual DateOnly? EffectivityEnd { get; set; } 
 public virtual BOMStatus? Status { get; set; } 
}

public class BOMResponse : BOMRequest {
    public static BOMResponse FromModel(BOM model) {
        return new BOMResponse {
            Id = model.Id,
            BomNumber = model.BomNumber,
            Revision = model.Revision,
            EffectivityStart = model.EffectivityStart,
            EffectivityEnd = model.EffectivityEnd,
            Status = model.Status,
        };
    }
}

public class BOMItemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? LineNumber { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual Percentage? ScrapPercent { get; set; } 
}

public class BOMItemResponse : BOMItemRequest {
    public static BOMItemResponse FromModel(BOMItem model) {
        return new BOMItemResponse {
            Id = model.Id,
            LineNumber = model.LineNumber,
            Quantity = model.Quantity,
            ScrapPercent = model.ScrapPercent,
        };
    }
}

public class RoutingRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? RoutingNumber { get; set; } 
 public virtual string? Revision { get; set; } 
 public virtual DateOnly? EffectivityStart { get; set; } 
 public virtual DateOnly? EffectivityEnd { get; set; } 
 public virtual RoutingType? RoutingType { get; set; } 
 public virtual RoutingStatus? Status { get; set; } 
}

public class RoutingResponse : RoutingRequest {
    public static RoutingResponse FromModel(Routing model) {
        return new RoutingResponse {
            Id = model.Id,
            RoutingNumber = model.RoutingNumber,
            Revision = model.Revision,
            EffectivityStart = model.EffectivityStart,
            EffectivityEnd = model.EffectivityEnd,
            RoutingType = model.RoutingType,
            Status = model.Status,
        };
    }
}

public class OperationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? OperationNumber { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual TimeDuration? SetupTime { get; set; } 
 public virtual TimeDuration? StandardCycleTime { get; set; } 
 public virtual OperationType? OperationType { get; set; } 
}

public class OperationResponse : OperationRequest {
    public static OperationResponse FromModel(Operation model) {
        return new OperationResponse {
            Id = model.Id,
            OperationNumber = model.OperationNumber,
            Name = model.Name,
            SetupTime = model.SetupTime,
            StandardCycleTime = model.StandardCycleTime,
            OperationType = model.OperationType,
        };
    }
}

public class WorkOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? WorkOrderNumber { get; set; } 
 public virtual DateTime? PlannedStart { get; set; } 
 public virtual DateTime? PlannedEnd { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual int? Priority { get; set; } 
 public virtual WorkOrderStatus? Status { get; set; } 
}

public class WorkOrderResponse : WorkOrderRequest {
    public static WorkOrderResponse FromModel(WorkOrder model) {
        return new WorkOrderResponse {
            Id = model.Id,
            WorkOrderNumber = model.WorkOrderNumber,
            PlannedStart = model.PlannedStart,
            PlannedEnd = model.PlannedEnd,
            Quantity = model.Quantity,
            Priority = model.Priority,
            Status = model.Status,
        };
    }
}

public class ProductionScheduleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ScheduleNumber { get; set; } 
 public virtual DateOnly? HorizonStart { get; set; } 
 public virtual DateOnly? HorizonEnd { get; set; } 
 public virtual ScheduleStatus? Status { get; set; } 
}

public class ProductionScheduleResponse : ProductionScheduleRequest {
    public static ProductionScheduleResponse FromModel(ProductionSchedule model) {
        return new ProductionScheduleResponse {
            Id = model.Id,
            ScheduleNumber = model.ScheduleNumber,
            HorizonStart = model.HorizonStart,
            HorizonEnd = model.HorizonEnd,
            Status = model.Status,
        };
    }
}

public class SupplierRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? SupplierCode { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual SupplierTier? SupplierTier { get; set; } 
 public virtual PaymentTerms? PaymentTerms { get; set; } 
}

public class SupplierResponse : SupplierRequest {
    public static SupplierResponse FromModel(Supplier model) {
        return new SupplierResponse {
            Id = model.Id,
            Name = model.Name,
            SupplierCode = model.SupplierCode,
            Address = model.Address,
            SupplierTier = model.SupplierTier,
            PaymentTerms = model.PaymentTerms,
        };
    }
}

public class PurchaseOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PoNumber { get; set; } 
 public virtual DateOnly? OrderDate { get; set; } 
 public virtual Money? TotalAmount { get; set; } 
 public virtual PurchaseOrderStatus? Status { get; set; } 
}

public class PurchaseOrderResponse : PurchaseOrderRequest {
    public static PurchaseOrderResponse FromModel(PurchaseOrder model) {
        return new PurchaseOrderResponse {
            Id = model.Id,
            PoNumber = model.PoNumber,
            OrderDate = model.OrderDate,
            TotalAmount = model.TotalAmount,
            Status = model.Status,
        };
    }
}

public class PurchaseOrderLineRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? LineNumber { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual Money? UnitPrice { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
}

public class PurchaseOrderLineResponse : PurchaseOrderLineRequest {
    public static PurchaseOrderLineResponse FromModel(PurchaseOrderLine model) {
        return new PurchaseOrderLineResponse {
            Id = model.Id,
            LineNumber = model.LineNumber,
            Quantity = model.Quantity,
            UnitPrice = model.UnitPrice,
            DueDate = model.DueDate,
        };
    }
}

public class GoodsReceiptRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ReceiptNumber { get; set; } 
 public virtual DateOnly? ReceiptDate { get; set; } 
 public virtual ReceiptStatus? Status { get; set; } 
}

public class GoodsReceiptResponse : GoodsReceiptRequest {
    public static GoodsReceiptResponse FromModel(GoodsReceipt model) {
        return new GoodsReceiptResponse {
            Id = model.Id,
            ReceiptNumber = model.ReceiptNumber,
            ReceiptDate = model.ReceiptDate,
            Status = model.Status,
        };
    }
}

public class GoodsReceiptLineRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? LineNumber { get; set; } 
 public virtual Quantity? ReceivedQuantity { get; set; } 
 public virtual Quantity? AcceptedQuantity { get; set; } 
 public virtual Quantity? RejectedQuantity { get; set; } 
 public virtual LotId? Lot { get; set; } 
}

public class GoodsReceiptLineResponse : GoodsReceiptLineRequest {
    public static GoodsReceiptLineResponse FromModel(GoodsReceiptLine model) {
        return new GoodsReceiptLineResponse {
            Id = model.Id,
            LineNumber = model.LineNumber,
            ReceivedQuantity = model.ReceivedQuantity,
            AcceptedQuantity = model.AcceptedQuantity,
            RejectedQuantity = model.RejectedQuantity,
            Lot = model.Lot,
        };
    }
}

public class WarehouseRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? WarehouseCode { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual WarehouseType? WarehouseType { get; set; } 
}

public class WarehouseResponse : WarehouseRequest {
    public static WarehouseResponse FromModel(Warehouse model) {
        return new WarehouseResponse {
            Id = model.Id,
            Name = model.Name,
            WarehouseCode = model.WarehouseCode,
            Address = model.Address,
            WarehouseType = model.WarehouseType,
        };
    }
}

public class LocationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? LocationCode { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual LocationType? LocationType { get; set; } 
}

public class LocationResponse : LocationRequest {
    public static LocationResponse FromModel(Location model) {
        return new LocationResponse {
            Id = model.Id,
            LocationCode = model.LocationCode,
            Description = model.Description,
            LocationType = model.LocationType,
        };
    }
}

public class InventoryItemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Quantity? QuantityOnHand { get; set; } 
 public virtual Quantity? QuantityReserved { get; set; } 
 public virtual LotId? LotNumber { get; set; } 
 public virtual SerialId? SerialNumber { get; set; } 
}

public class InventoryItemResponse : InventoryItemRequest {
    public static InventoryItemResponse FromModel(InventoryItem model) {
        return new InventoryItemResponse {
            Id = model.Id,
            QuantityOnHand = model.QuantityOnHand,
            QuantityReserved = model.QuantityReserved,
            LotNumber = model.LotNumber,
            SerialNumber = model.SerialNumber,
        };
    }
}

public class InventoryTransactionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? TransactionNumber { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual DateTime? TransactionDateTime { get; set; } 
 public virtual string? ReferenceDocument { get; set; } 
 public virtual InventoryTransactionType? TransactionType { get; set; } 
}

public class InventoryTransactionResponse : InventoryTransactionRequest {
    public static InventoryTransactionResponse FromModel(InventoryTransaction model) {
        return new InventoryTransactionResponse {
            Id = model.Id,
            TransactionNumber = model.TransactionNumber,
            Quantity = model.Quantity,
            TransactionDateTime = model.TransactionDateTime,
            ReferenceDocument = model.ReferenceDocument,
            TransactionType = model.TransactionType,
        };
    }
}

public class CustomerRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? CustomerCode { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual CustomerType? CustomerType { get; set; } 
}

public class CustomerResponse : CustomerRequest {
    public static CustomerResponse FromModel(Customer model) {
        return new CustomerResponse {
            Id = model.Id,
            Name = model.Name,
            CustomerCode = model.CustomerCode,
            Address = model.Address,
            CustomerType = model.CustomerType,
        };
    }
}

public class SalesOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? OrderNumber { get; set; } 
 public virtual DateOnly? OrderDate { get; set; } 
 public virtual Money? TotalAmount { get; set; } 
 public virtual SalesOrderStatus? Status { get; set; } 
}

public class SalesOrderResponse : SalesOrderRequest {
    public static SalesOrderResponse FromModel(SalesOrder model) {
        return new SalesOrderResponse {
            Id = model.Id,
            OrderNumber = model.OrderNumber,
            OrderDate = model.OrderDate,
            TotalAmount = model.TotalAmount,
            Status = model.Status,
        };
    }
}

public class SalesOrderLineRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? LineNumber { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual Money? UnitPrice { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
}

public class SalesOrderLineResponse : SalesOrderLineRequest {
    public static SalesOrderLineResponse FromModel(SalesOrderLine model) {
        return new SalesOrderLineResponse {
            Id = model.Id,
            LineNumber = model.LineNumber,
            Quantity = model.Quantity,
            UnitPrice = model.UnitPrice,
            DueDate = model.DueDate,
        };
    }
}

public class QualitySpecificationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? SpecCode { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Version { get; set; } 
}

public class QualitySpecificationResponse : QualitySpecificationRequest {
    public static QualitySpecificationResponse FromModel(QualitySpecification model) {
        return new QualitySpecificationResponse {
            Id = model.Id,
            SpecCode = model.SpecCode,
            Name = model.Name,
            Version = model.Version,
        };
    }
}

public class InspectionPlanRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PlanNumber { get; set; } 
 public virtual string? Revision { get; set; } 
 public virtual SamplingPlanType? SamplingPlan { get; set; } 
 public virtual QualityPlanStatus? Status { get; set; } 
}

public class InspectionPlanResponse : InspectionPlanRequest {
    public static InspectionPlanResponse FromModel(InspectionPlan model) {
        return new InspectionPlanResponse {
            Id = model.Id,
            PlanNumber = model.PlanNumber,
            Revision = model.Revision,
            SamplingPlan = model.SamplingPlan,
            Status = model.Status,
        };
    }
}

public class InspectionCharacteristicRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? CharacteristicCode { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Measurement? LowerSpecLimit { get; set; } 
 public virtual Measurement? UpperSpecLimit { get; set; } 
 public virtual Measurement? Target { get; set; } 
 public virtual MeasurementType? MeasurementType { get; set; } 
}

public class InspectionCharacteristicResponse : InspectionCharacteristicRequest {
    public static InspectionCharacteristicResponse FromModel(InspectionCharacteristic model) {
        return new InspectionCharacteristicResponse {
            Id = model.Id,
            CharacteristicCode = model.CharacteristicCode,
            Name = model.Name,
            LowerSpecLimit = model.LowerSpecLimit,
            UpperSpecLimit = model.UpperSpecLimit,
            Target = model.Target,
            MeasurementType = model.MeasurementType,
        };
    }
}

public class InspectionLotRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? LotNumber { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual int? SampleSize { get; set; } 
 public virtual DateTime? CreatedOn { get; set; } 
 public virtual InspectionType? InspectionType { get; set; } 
 public virtual InspectionStatus? Status { get; set; } 
}

public class InspectionLotResponse : InspectionLotRequest {
    public static InspectionLotResponse FromModel(InspectionLot model) {
        return new InspectionLotResponse {
            Id = model.Id,
            LotNumber = model.LotNumber,
            Quantity = model.Quantity,
            SampleSize = model.SampleSize,
            CreatedOn = model.CreatedOn,
            InspectionType = model.InspectionType,
            Status = model.Status,
        };
    }
}

public class InspectionResultRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Measurement? ResultValue { get; set; } 
 public virtual DateTime? RecordedOn { get; set; } 
 public virtual string? Notes { get; set; } 
 public virtual InspectionResultStatus? ResultStatus { get; set; } 
}

public class InspectionResultResponse : InspectionResultRequest {
    public static InspectionResultResponse FromModel(InspectionResult model) {
        return new InspectionResultResponse {
            Id = model.Id,
            ResultValue = model.ResultValue,
            RecordedOn = model.RecordedOn,
            Notes = model.Notes,
            ResultStatus = model.ResultStatus,
        };
    }
}

public class NonconformanceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? NcNumber { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual string? ContainmentAction { get; set; } 
 public virtual NonconformanceType? NcType { get; set; } 
 public virtual QualitySeverity? Severity { get; set; } 
 public virtual NonconformanceStatus? Status { get; set; } 
}

public class NonconformanceResponse : NonconformanceRequest {
    public static NonconformanceResponse FromModel(Nonconformance model) {
        return new NonconformanceResponse {
            Id = model.Id,
            NcNumber = model.NcNumber,
            Description = model.Description,
            ContainmentAction = model.ContainmentAction,
            NcType = model.NcType,
            Severity = model.Severity,
            Status = model.Status,
        };
    }
}

public class CorrectiveActionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? CapaNumber { get; set; } 
 public virtual string? RootCause { get; set; } 
 public virtual string? CorrectiveAction_ { get; set; } 
 public virtual DateOnly? VerificationDate { get; set; } 
 public virtual CAPAStatus? Status { get; set; } 
}

public class CorrectiveActionResponse : CorrectiveActionRequest {
    public static CorrectiveActionResponse FromModel(CorrectiveAction model) {
        return new CorrectiveActionResponse {
            Id = model.Id,
            CapaNumber = model.CapaNumber,
            RootCause = model.RootCause,
            CorrectiveAction_ = model.CorrectiveAction_,
            VerificationDate = model.VerificationDate,
            Status = model.Status,
        };
    }
}

public class AssetRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? AssetTag { get; set; } 
 public virtual string? AssetName { get; set; } 
 public virtual DateOnly? CommissioningDate { get; set; } 
 public virtual AssetStatus? AssetStatus { get; set; } 
}

public class AssetResponse : AssetRequest {
    public static AssetResponse FromModel(Asset model) {
        return new AssetResponse {
            Id = model.Id,
            AssetTag = model.AssetTag,
            AssetName = model.AssetName,
            CommissioningDate = model.CommissioningDate,
            AssetStatus = model.AssetStatus,
        };
    }
}

public class MaintenancePlanRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PlanNumber { get; set; } 
 public virtual TimeDuration? Interval { get; set; } 
 public virtual DateOnly? LastServiceDate { get; set; } 
 public virtual MaintenanceStrategy? Strategy { get; set; } 
}

public class MaintenancePlanResponse : MaintenancePlanRequest {
    public static MaintenancePlanResponse FromModel(MaintenancePlan model) {
        return new MaintenancePlanResponse {
            Id = model.Id,
            PlanNumber = model.PlanNumber,
            Interval = model.Interval,
            LastServiceDate = model.LastServiceDate,
            Strategy = model.Strategy,
        };
    }
}

public class MaintenanceOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? OrderNumber { get; set; } 
 public virtual int? Priority { get; set; } 
 public virtual DateOnly? RequestedDate { get; set; } 
 public virtual DateOnly? CompletionDate { get; set; } 
 public virtual MaintenanceOrderStatus? Status { get; set; } 
}

public class MaintenanceOrderResponse : MaintenanceOrderRequest {
    public static MaintenanceOrderResponse FromModel(MaintenanceOrder model) {
        return new MaintenanceOrderResponse {
            Id = model.Id,
            OrderNumber = model.OrderNumber,
            Priority = model.Priority,
            RequestedDate = model.RequestedDate,
            CompletionDate = model.CompletionDate,
            Status = model.Status,
        };
    }
}

public class EmployeeRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual EmployeeRole? Role { get; set; } 
 public virtual SkillLevel? SkillLevel { get; set; } 
}

public class EmployeeResponse : EmployeeRequest {
    public static EmployeeResponse FromModel(Employee model) {
        return new EmployeeResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Role = model.Role,
            SkillLevel = model.SkillLevel,
        };
    }
}

public class ShiftRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ShiftName { get; set; } 
 public virtual string? StartTime { get; set; } 
 public virtual string? EndTime { get; set; } 
 public virtual ShiftType? ShiftType { get; set; } 
}

public class ShiftResponse : ShiftRequest {
    public static ShiftResponse FromModel(Shift model) {
        return new ShiftResponse {
            Id = model.Id,
            ShiftName = model.ShiftName,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            ShiftType = model.ShiftType,
        };
    }
}

public class ShiftAssignmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? AssignmentDate { get; set; } 
}

public class ShiftAssignmentResponse : ShiftAssignmentRequest {
    public static ShiftAssignmentResponse FromModel(ShiftAssignment model) {
        return new ShiftAssignmentResponse {
            Id = model.Id,
            AssignmentDate = model.AssignmentDate,
        };
    }
}

public class ForecastRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ForecastNumber { get; set; } 
 public virtual DateOnly? ForecastHorizonStart { get; set; } 
 public virtual DateOnly? ForecastHorizonEnd { get; set; } 
 public virtual ForecastMethod? Method { get; set; } 
}

public class ForecastResponse : ForecastRequest {
    public static ForecastResponse FromModel(Forecast model) {
        return new ForecastResponse {
            Id = model.Id,
            ForecastNumber = model.ForecastNumber,
            ForecastHorizonStart = model.ForecastHorizonStart,
            ForecastHorizonEnd = model.ForecastHorizonEnd,
            Method = model.Method,
        };
    }
}

public class ForecastLineRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? Period { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual Percentage? Confidence { get; set; } 
}

public class ForecastLineResponse : ForecastLineRequest {
    public static ForecastLineResponse FromModel(ForecastLine model) {
        return new ForecastLineResponse {
            Id = model.Id,
            Period = model.Period,
            Quantity = model.Quantity,
            Confidence = model.Confidence,
        };
    }
}

public class MRPRunRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? RunNumber { get; set; } 
 public virtual DateTime? RunDateTime { get; set; } 
 public virtual int? PlanningHorizonDays { get; set; } 
 public virtual MRPRunStatus? Status { get; set; } 
}

public class MRPRunResponse : MRPRunRequest {
    public static MRPRunResponse FromModel(MRPRun model) {
        return new MRPRunResponse {
            Id = model.Id,
            RunNumber = model.RunNumber,
            RunDateTime = model.RunDateTime,
            PlanningHorizonDays = model.PlanningHorizonDays,
            Status = model.Status,
        };
    }
}

public class PlannedOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PlannedOrderNumber { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual PlannedOrderType? OrderType { get; set; } 
 public virtual PlannedOrderStatus? Status { get; set; } 
}

public class PlannedOrderResponse : PlannedOrderRequest {
    public static PlannedOrderResponse FromModel(PlannedOrder model) {
        return new PlannedOrderResponse {
            Id = model.Id,
            PlannedOrderNumber = model.PlannedOrderNumber,
            Quantity = model.Quantity,
            DueDate = model.DueDate,
            OrderType = model.OrderType,
            Status = model.Status,
        };
    }
}

