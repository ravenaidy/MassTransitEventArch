using MediatR;

namespace MassTransit.BFFServices.SignalRWorker.Account.Commands
{
    public class NewAccountRequest : IRequest
    {
        public string Username { get; set; }
     
        public string Password { get; set; }
        
        public string Email { get; set; }

        public string Firstname { get; set; }
     
        public string Lastname { get; set; }
     
        public int Gender { get; set; }

        public string AddressLine1 { get; set; }
        
        public string AddressLine2 { get; set; }
        
        public string AddressLine3 { get; set; }
        
        public string City { get; set; }
        
        public string Country { get; set; }
        
        public int PostalCode { get; set; }
    }
}