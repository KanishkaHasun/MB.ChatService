using ChatService.Application.Models.Dtos;
using MediatR;

namespace ChatService.Application.Cqrs.Chats.Commands
{
    public record CreateChatCommand(Guid? UserId, string? Username) : IRequest<CreateChatResponseDto>;
}
