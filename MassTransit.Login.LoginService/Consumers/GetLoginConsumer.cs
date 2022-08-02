using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit.LoginService.Events;
using MassTransit.LoginService.Repositories.Contracts;

namespace MassTransit.LoginService.Consumers
{
    public class GetLoginConsumer : IConsumer<GetLogin>
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;
        private readonly ITopicProducer<LoginResponse> _producer;
        
        public GetLoginConsumer(ILoginRepository loginRepository, IMapper mapper, ITopicProducer<LoginResponse> producer)
        {
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }
        public async Task Consume(ConsumeContext<GetLogin> context)
        {
            var login = _mapper.Map<LoginResponse>(await _loginRepository.GetLogin(context.Message.Username,
                context.Message.Password)) ?? new LoginResponse {LoginId = -1};
            
            await _producer.Produce(login);
        }
    }
}