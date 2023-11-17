using System;

namespace CourseWork;

public class Employee
{
    public Employee(Fullname name, string phoneNumber, string email, string position, string login, string password)
    {
        _name = name;
        _phoneNumber = phoneNumber;
        _email = email;
        _position = position;
        _login = login;
        _password = password;
    }

    public Employee(string firstName,string middleName, string lastName, string phoneNumber, string email, string position, string login, string password)
    {
        _name = new Fullname(firstName,middleName,lastName);
        _phoneNumber = phoneNumber;
        _email = email;
        _position = position;
        _login = login;
        _password = password;
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set => _phoneNumber = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Email
    {
        get => _email;
        set => _email = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Position
    {
        get => _position;
        set => _position = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Password
    {
        get => _password;
        set => _password = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Fullname Name => _name;

    public string Login => _login;

    private readonly Fullname _name;
    private string _phoneNumber;
    private string _email;
    private string _position;
    private readonly string _login;
    private string _password;

}