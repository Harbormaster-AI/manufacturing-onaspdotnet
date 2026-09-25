using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace manufacturingonaspdotnet.Telemetry;

public class ApplicationTelemetry
{
    private static readonly Meter Meter =
        new("Harbormaster.Application");

    private static readonly Counter<long> Operations =
        Meter.CreateCounter<long>(
            "harbormaster.application.operations");

    public async Task<T> Execute<T>(
        string entity,
        string operation,
        Func<Task<T>> action)
    {
        try
        {
            var result = await action();

            Operations.Add(
                1,
                new KeyValuePair<string, object?>(
                    "entity",
                    entity),
                new KeyValuePair<string, object?>(
                    "operation",
                    operation),
                new KeyValuePair<string, object?>(
                    "status",
                    "success"));

            return result;
        }
        catch
        {
            Operations.Add(
                1,
                new KeyValuePair<string, object?>(
                    "entity",
                    entity),
                new KeyValuePair<string, object?>(
                    "operation",
                    operation),
                new KeyValuePair<string, object?>(
                    "status",
                    "failure"));

            throw;
        }
    }

    public async Task Execute(
        string entity,
        string operation,
        Func<Task> action)
    {
        try
        {
            await action();

            Operations.Add(
                1,
                new KeyValuePair<string, object?>(
                    "entity",
                    entity),
                new KeyValuePair<string, object?>(
                    "operation",
                    operation),
                new KeyValuePair<string, object?>(
                    "status",
                    "success"));
        }
        catch
        {
            Operations.Add(
                1,
                new KeyValuePair<string, object?>(
                    "entity",
                    entity),
                new KeyValuePair<string, object?>(
                    "operation",
                    operation),
                new KeyValuePair<string, object?>(
                    "status",
                    "failure"));

            throw;
        }
    }
}