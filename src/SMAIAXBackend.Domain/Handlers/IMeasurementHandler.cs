using SMAIAXBackend.Domain.Model.Entities;

namespace SMAIAXBackend.Domain.Handlers;

public interface IMeasurementHandler
{
    Task<IList<Measurement>> GetMeasurementsByPolicyAsync(Policy policy);
}