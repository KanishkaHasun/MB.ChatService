using ChatService.Application.Interfaces;
using ChatService.Application.Models.Dtos;
using ChatService.Application.Services;
using ChatService.Domain.Entities;
using MediatR;

namespace ChatService.Application.Cqrs.Chats.Queries
{
    public sealed class GetChatStatusQueryHandler : IRequestHandler<GetChatStatusQuery, ChatStatusDto>
    {
        private readonly IChatRepository _chatRepo;
        private readonly IChatSessionPolllingService _chatSessionPolllingService;

        public GetChatStatusQueryHandler(IChatSessionPolllingService chatSessionPolllingService, IChatRepository chatRepo)
        {
            _chatSessionPolllingService = chatSessionPolllingService;
            _chatRepo = chatRepo;
        }

        public async Task<ChatStatusDto> Handle(GetChatStatusQuery request, CancellationToken cancellationToken)
        {
            var chatSession = await _chatSessionPolllingService.GetSessionStatusAsync(request.ChatId);

            if (chatSession is not null)
            {
                await _chatSessionPolllingService.UpdateHeartbeatAsync(request.ChatId);
            }
            else
            {
                ChatSession? chatSessionDetails = await _chatRepo.GetByIdAsync(request.ChatId);
                chatSession = MapToChatStatusDto(chatSessionDetails);
            }
            
            return chatSession;
        }

        private ChatStatusDto MapToChatStatusDto(ChatSession? chatSession) {

            if (chatSession is null)
            {
                throw new ArgumentNullException(nameof(chatSession));
            }

            return new ChatStatusDto { 
                UserId = chatSession!.UserId,
                AgentId = chatSession.AssignedAgentId,
                ChatStatus = chatSession!.Status.ToString(),
            };

        }
    }

}
