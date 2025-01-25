namespace SMAIAXBackend.Application.Exceptions;

public class TenantNotFoundException : Exception
{
    public TenantNotFoundException(Guid tenantId, Guid userId) : base(
        $"Tenant with id '{tenantId}' not found for user with id '{userId}'.")
    {
    }

    public TenantNotFoundException(Guid tenantId) : base(
        $"Tenant with id '{tenantId}' not found.")
    {
    }
}