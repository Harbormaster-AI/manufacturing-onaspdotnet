using System.ComponentModel.DataAnnotations.Schema;

namespace manufacturingonaspdotnet.Domain;


    [ComplexType]
    public record Money(
    decimal Amount,
    string Currency
    );

    [ComplexType]
    public record Address(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
    );

    [ComplexType]
    public record Quantity(
    decimal Amount,
    UnitOfMeasure Unit
    );

    [ComplexType]
    public record Measurement(
    decimal Value,
    UnitOfMeasure Unit
    );

    [ComplexType]
    public record Percentage(
    decimal Value
    );

    [ComplexType]
    public record TimeDuration(
    decimal Value,
    TimeUnit Unit
    );

    [ComplexType]
    public record LotId(
    string Value
    );

    [ComplexType]
    public record SerialId(
    string Value
    );

    [ComplexType]
    public record DateRange(
    DateOnly Start,
    DateOnly End
    );

