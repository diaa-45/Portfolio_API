namespace Portfolio_API.Interfaces
{
    public interface IProjectImageRepository
    {
        Task<IEnumerable<ProjectImages>> GetAllByProjectIdAsync(int projectId);
        Task AddImagesAsync(int projectId, IEnumerable<string> urls);
        
        Task<ProjectImages?> GetByIdAsync(int imageId);
        Task DeleteAsync(int imageId);
    }

}
