namespace MassTransit.Account.AccountService.Models
{
    public class Account
    {
        public int AccountId { get; init; }
        public string Firstname { get; init; }
        public string Lastname { get; init; }
        public int Gender { get; init; }
        public string AddressLine1 { get; init; }
        public string AddressLine2 { get; init; }
        public string AddressLine3 { get; init; }
        public string City { get; init; }
        public string Country { get; init; }
        public int PostalCode { get; init; }
    }
}