using Reviews.Models;

namespace Reviews.Services
{
    public interface IReviewService
    {
        public List<Review> GetAll();

        public List<Review> GetAll(string query);

        public Review Get(int id);

        public void Edit(int id, string username, int score, string feedback);

        public void Delete(int id);

        // returns average score
        public string GetAverageScore();
    }
}
