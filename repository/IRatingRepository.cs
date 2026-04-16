using Model;

namespace Repository
{
    public interface IRatingRepository
    {
        Task<Rating> AddRating(Rating newRating);
    }
}
