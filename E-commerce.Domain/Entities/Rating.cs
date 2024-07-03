namespace E_commerce.Domain.Entities;
public class Rating
{
    public Guid Id { get; set; }
    public User User { get; set; } 
    public DateTime DateTime { get; set; }
    public Ratings ratings { get; set; }

    public enum Ratings
    {
        very_bad = 1,
        bad = 2,
        neutral = 3,
        good = 4,
        very_good = 5,
    }
}
