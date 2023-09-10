namespace Entity;

public class PropertyValue
{
    public PropertyValue(string value, int count)
    {
        Value = value;
        Count = count;
    }

    public string Value { get; set; }
    public int Count { get; set; }
}