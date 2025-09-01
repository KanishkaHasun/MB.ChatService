namespace ChatService.Application.Services
{
    public interface ITeamManagementService
    {
        Task AddTeamCapacity(int maxTeamCapacity);
        Task<int> GetMaxTeamCapacity();
    }
}
