namespace DbEntity;

public interface IHeadingFilter
{
    public int heading_id { get; set; }
    public int property_values_id { get; set; }
    public int Count { get; set; }
}