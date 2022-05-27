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
        private readonly ITopicProducer<LoginCreated> _producer;

        public CreateLoginConsumer(IMapper mapper, ILoginRepository loginRepository, ITopicProducer<LoginCreated> producer)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

        public async Task Consume(ConsumeContext<CreateLogin> context)
        {
            var userId = await _loginRepository.RegisterLogin(_mapper.Map<Login>(context.Message));
            await _producer.Produce(new LoginCreated { UserId = userId, LoginCreatedTimeStamp = InVar.Timestamp });
        }
    }
}