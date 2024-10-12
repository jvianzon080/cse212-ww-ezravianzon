public class FeatureCollection
{
    public string Type { get; set; }
    public List<Feature> Features { get; set; }
}

public class Properties
{
    public string Place {get;set;}
    public double? Mag {get;set;} // Use double just in case there are null values for magnitude
}