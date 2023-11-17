namespace CourseWork;

public class Fullname
{
    private string _firstName;//Имя
    private string _middleName;//Отчество
    private string _lastName;//Фамилия

    public Fullname(string firstName, string middleName, string lastName)
    {
        _firstName = firstName;
        _middleName = middleName;
        _lastName = lastName;
    }

    public string FirstName => _firstName;

    public string MiddleName => _middleName;

    public string LastName => _lastName;

    public override string ToString()
    {
        if (string.IsNullOrEmpty(_firstName) || string.IsNullOrEmpty(_lastName))
        {
            return base.ToString();
        }
        return _firstName + " " + _lastName + " " + _middleName;
    }
}