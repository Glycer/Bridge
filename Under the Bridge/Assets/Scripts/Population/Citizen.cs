public class Citizen
{
    public Personality personality;
    public string Name { get; set; }

    public Citizen()
    {
        Name = "Unnamed";
        personality = new Personality();
    }
}
