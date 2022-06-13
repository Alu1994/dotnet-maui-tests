namespace MonkeyFinder.Model;

public class Monkey
{
    public Monkey()
    {

    }

    public Monkey(Monkey monkey)
    {
        Id = monkey.Id;
        Name = monkey.Name;
        Location = monkey.Location;
        Details = monkey.Details;
        Image = monkey.Image;
        Population = monkey.Population;
        Latitude = monkey.Latitude;
        Longitude = monkey.Longitude;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Details { get; set; }
    public string Image { get; set; }
    public int Population { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
