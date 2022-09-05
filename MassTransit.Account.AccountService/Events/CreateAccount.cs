using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.AutoMapperExtensions.Contracts;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.Account.AccountService.Events
{
    public class CreateAccount : IEvent, IMapTo<Models.Account>
    {
        public Guid AccountId { get; init; }
        
        [NotLogged]
        public string Email { get; init; }
        
        [NotLogged]
        public string Firstname { get; init; }
        
        [NotLogged]
        public string Lastname { get; init; }
        
        [NotLogged]
        public int PhoneNumber { get; init; }

        public int Gender { get; init; }
        
        [NotLogged]
        public string AddressLine1 { get; init; }
        
        [NotLogged]
        public string AddressLine2 { get; init; }
        
        [NotLogged]
        public string AddressLine3 { get; init; }
        
        [NotLogged]
        public string City { get; init; }
        
        [NotLogged]
        public string Country { get; init; }
        
        [NotLogged]
        public int PostalCode { get; init; }
        
        [NotLogged]
        public Guid CorrelationId { get; set; }
    }
}