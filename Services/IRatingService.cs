using Model;

namespace Services
{
    public interface IRatingService
    {
        Task<Rating> AddRating(Rating newRating);
    }
}