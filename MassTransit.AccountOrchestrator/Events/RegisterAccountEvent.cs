using MassTransit.AccountOrchestrator.Models;

namespace MassTransit.AccountOrchestrator.Events
{
    public interface RegisterAccount : CorrelatedBy<Guid>
    {
        string Username { get; set; }
     
        string Password { get; set; }

        string Firstname { get; set; }
        
        string Lastname { get; set; }
        
        Gender Gender { get; set; }

        string AddressLine1 { get; set; }
        
        string AddressLine2 { get; set; }
        
        string AddressLine3 { get; set; }
        
        string City { get; set; }
        
        int PostalCode { get; set; }
    }
}