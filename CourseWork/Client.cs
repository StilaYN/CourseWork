namespace CourseWork;

public class Client
{
    public Client(Fullname name, int phoneNumber, string email, string passportData)
    {
        _name = name;
        _phoneNumber = phoneNumber;
        _email = email;
        _passportData = passportData;
    }

    public Client(string firstName, string middleName, string lastName,, int phoneNumber, string email, string passportData)
    {
        _name = new Fullname(firstName,middleName,lastName);
        _phoneNumber = phoneNumber;
        _email = email;
        _passportData = passportData;
    }

    public Fullname Name => _name;

    public int PhoneNumber => _phoneNumber;

    public string Email => _email;

    public string PassportData => _passportData;

    private readonly Fullname _name;
    private readonly int _phoneNumber;
    private readonly string _email;
    private readonly string _passportData;
}