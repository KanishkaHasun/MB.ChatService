using ChatService.Application.Cqrs.Chats.Commands;
using ChatService.Application.Interfaces;
using ChatService.Application.Models.Dtos;
using ChatService.Application.Services;
using ChatService.Domain.Entities;
using ChatService.Domain.Enums;
using MediatR;

namespace ChatService.Application.Handlers
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, CreateChatResponseDto>
    {
        private readonly IUserRepository _userRepo;
        private readonly IChatRepository _chatRepo;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IChatSessionQueueService _chatSessionQueueService;
        private readonly IChatSessionPolllingService _chatSessionPolllingService;
        private readonly ITeamManagementService _teamManagementService;

        public CreateChatCommandHandler(IUserRepository userRepo,
            IChatRepository chatRepo,
            IRepositoryManager repositoryManager,
            IChatSessionQueueService chatSessionQueueService,
            IChatSessionPolllingService chatSessionPolllingService,
            ITeamManagementService teamManagementService)
        {
            _userRepo = userRepo;
            _chatRepo = chatRepo;
            _repositoryManager = repositoryManager;
            _chatSessionQueueService = chatSessionQueueService;
            _chatSessionPolllingService = chatSessionPolllingService;
            _teamManagementService = teamManagementService;
        }

        public async Task<CreateChatResponseDto> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            User? user = null;
            if (request.UserId != null) {
                 user = await _userRepo.GetByIdAsync(request.UserId.Value!);
            }

            if (user is null) {
                user = new User(request.Username);
                await _userRepo.AddAsync(user);
            }

            if (!await IsQueueUnderMaxCapacity()) 
            {
                throw new InvalidOperationException("The chat queue is at maximum capacity. Please try again later.");
            }
                ChatSession chatSession= user.CreateChat();
            await _chatRepo.AddAsync(chatSession);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            await _chatSessionQueueService.EnqueueAsync(chatSession.Id.ToString());
           
            chatSession.SetState(ChatStatus.QUEUED);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            CreateChatResponseDto chatResponseDto = MapToCreateChatResponseDto(user.Id, chatSession.Id, chatSession.Status);
            await _chatSessionPolllingService.AddAsync(chatResponseDto);

            return chatResponseDto;
        }

        private static CreateChatResponseDto MapToCreateChatResponseDto(Guid userId, Guid chatId, ChatStatus chatStatus) 
        {
            return new CreateChatResponseDto
            {
                UserId = userId,
                ChatId = chatId,
                ChatStatus = chatStatus.ToString()
            };
        }

        private async Task<bool> IsQueueUnderMaxCapacity() 
        {
            int queueSize = await _chatSessionQueueService.GetQueueLengthAsync();
            int maxCapacity = await _teamManagementService.GetMaxTeamCapacity();

            return (queueSize < maxCapacity);
        }
    }
}
