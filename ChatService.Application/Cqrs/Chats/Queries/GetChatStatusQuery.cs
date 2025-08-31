using ChatService.Application.Models.Dtos;
using MediatR;

namespace ChatService.Application.Cqrs.Chats.Queries
{
    public sealed record GetChatStatusQuery(Guid ChatId) : IRequest<ChatStatusDto>;
}
