﻿using System;

namespace MassTransit.Account.AccountService.Models;

public class Account
{
    public Guid AccountId { get; init; }
    public string Firstname { get; init; }
    public string Lastname { get; init; }
    public string Email { get; init; }
    public int PhoneNumber { get; init; }
    public int Gender { get; init; }
    public string AddressLine1 { get; init; }
    public string AddressLine2 { get; init; }
    public string AddressLine3 { get; init; }
    public string City { get; init; }
    public string Country { get; init; }
    public int PostalCode { get; init; }
}