﻿using MassTransit.AccountOrchestrator.Models;

namespace MassTransit.AccountOrchestrator.Events
{
    public record CreateAccount(Guid CorrelationId) : CorrelatedBy<Guid>
    {
        public int AccountId { get; set; }
        
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        public Gender Gender { get; set; }

        public string AddressLine1 { get; set; }
        
        public string AddressLine2 { get; set; }
        
        public string AddressLine3 { get; set; }
        
        public string City { get; set; }
        public string Country { get; set; }
        
        public int PostalCode { get; set; }
    }
}