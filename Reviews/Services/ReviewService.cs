using Reviews.Data;
using Reviews.Models;

namespace Reviews.Services
{
    public class ReviewService : IReviewService
    {
        private ReviewsContext reviewContext = new();
        public List<Review> GetAll()
        {
            return reviewContext.Review.ToList();
        }
        // gets all reviews
        public List<Review> GetAll(string s)
        {
            var reviews = GetAll();
            reviews = reviews.FindAll(r => r.Username == s || r.Feedback == s);
            return reviews;
        }
        // gets review
        public Review Get(int id)
        {
            return GetAll().FirstOrDefault(r => r.Id == id);
        }
        // edits review
        public void Edit(int id, string username, int score, string feedback)
        {
            var review = Get(id);
            review.Username = username;
            review.Score = score;
            review.Feedback = feedback;
        }
        // deletes review
        public void Delete(int id)
        {
            reviewContext.Review.Remove(Get(id));
        }

        // gets average score to two numbers after decimal
        public string GetAverageScore()
        {
            var reviews = GetAll();
            double sum = 0;
            foreach (var review in reviews)
            {
                sum += review.Score;
            }
            return (sum / reviews.Count).ToString("0.00");
        }
    }
}
