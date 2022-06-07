using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransitApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ITopicProducer<Account> _producer;

        public AccountController(ITopicProducer<Account> producer)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

        [HttpPost(Name = "register")]
        public async Task<IActionResult> RegisterAccount(Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            await _producer.Produce(account);
            return Ok();
        }
    }
}