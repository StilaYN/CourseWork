using System.Text.Json.Serialization;

namespace Test;

public class Person
{
    public string Name { get; set; }
    public int PhoneNumber { get; set; }
    [JsonConstructor]
    public Person(string name, int phoneNumber)
    {
        Name=name;
        PhoneNumber=phoneNumber;
    }

    public override string ToString()
    {
        return Name+" "+PhoneNumber.ToString();
    }
}