namespace CourseWork;

public class Client
{
    public Client(Fullname name, string phoneNumber, string email, string passportData)
    {
        _name = name;
        _phoneNumber = phoneNumber;
        _email = email;
        _passportData = passportData;
    }

    public Client(string firstName, string middleName, string lastName, string phoneNumber, string email, string passportData)
    {
        _name = new Fullname(firstName,middleName,lastName);
        _phoneNumber = phoneNumber;
        _email = email;
        _passportData = passportData;
    }

    private bool IsEquel(Client right)
    {
        if(_name==right._name&&_phoneNumber==right._phoneNumber&&_email==right._email&&_passportData==right._passportData) return true;
        return false;
    }

    public static bool operator ==(Client left, Client right)
    {
        return left.IsEquel(right);
    }

    public static bool operator !=(Client left, Client right)
    {
        return !left.IsEquel(right);
    }

    public Fullname Name => _name;

    public string PhoneNumber => _phoneNumber;

    public string Email => _email;

    public string PassportData => _passportData;

    private readonly Fullname _name;
    private readonly string _phoneNumber;
    private readonly string _email;
    private readonly string _passportData;
}