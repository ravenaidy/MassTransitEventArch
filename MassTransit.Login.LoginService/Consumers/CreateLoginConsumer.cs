using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit.LoginService.Events;
using MassTransit.LoginService.Models;
using MassTransit.LoginService.Repositories.Contracts;

namespace MassTransit.LoginService.Consumers
{
    public class CreateLoginConsumer: IConsumer<CreateLogin>
    {
        private readonly IMapper _mapper;
        private readonly ILoginRepository _loginRepository;

        public CreateLoginConsumer(IMapper mapper, ILoginRepository loginRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
        }

        public async Task Consume(ConsumeContext<CreateLogin> context)
        {
            var userId = await _loginRepository.RegisterLogin(_mapper.Map<Login>(context.Message));
            await context.Publish(new LoginCreated(userId, InVar.Timestamp));
        }
    }
}