using OneReview.Domain;
using OneReview.Persistence.Repositories;


public class CourseService(CourseRepository courseRepository)
{
    private readonly CourseRepository _courseRepository = courseRepository;
    public async Task CreateAsync(Course course)
    {
        // store players in the database
        await _courseRepository.CreateAsync(course);
    }

    public async Task<Course?> GetAsync(Guid courseId)
    {
       return await _courseRepository.GetByIdAsync(courseId);
    }

    
   /* public async Task DeleteAsync(Guid courseId)
    {
        await _playerRepository.DeleteAsync(playerId);
    }
    */
}   