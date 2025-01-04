using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Entities.Measurements;

namespace SMAIAXBackend.Domain.Handlers;

public interface IMeasurementHandler
{
    Task<IList<MeasurementBase>> GetMeasurementsByPolicyAsync(Policy policy);
}