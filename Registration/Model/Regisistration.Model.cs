
namespace Registration.Model
{



public class Person
{
    public int ID { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string IDNumber { get; set; }
    public string Email { get; set; }
    public Address PhysicalAddress { get; set; }
    public Address PostalAddress { get; set; }
    public Contact ContactDetails { get; set; }
}

public class Address
{
    public int ID { get; set; }
    public string AddressLine { get; set; }
}

public class Contact
{
    public int ID { get; set; }
    public string Telephone { get; set; }
    public string Cell { get; set; }
    public string Work { get; set; }
}
}
