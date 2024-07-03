namespace E_commerce.Domain.Entities;
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Supplier { get; set; } = string.Empty;
    public string Category {  get; set; } = string.Empty;
    public double GeneralRating 
    {
        get
        {
            return GeneralRating;
        }
        set 
        {
            ChangeGeneralRating();
        }
    }
    public List<Rating> RatingsList { get; set; } = default!;
    public List<Parameter> ParametersList { get; set; } = default!;
    public int Quantity {  get; set; }
    public double Price { get; set; }


    public void ChangeGeneralRating()
    {
        if (RatingsList == null || RatingsList.Count == 0)
        {
            GeneralRating = 0;
        }
        else
        {
            int sum = 0;
            double i = 0;
            foreach (Rating rating in RatingsList)
            {
                sum += ((int)rating.ratings);
                i++;
            }
            GeneralRating = Math.Round(sum / i,2);

        }
    }

}

