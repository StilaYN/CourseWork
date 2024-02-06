using System;

namespace CourseWork.MainLogic;

public class Client:IClient
{
    public Client()
    {

    }
    public Client(Fullname name, string phoneNumber, string email, string passportData)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        PassportData = passportData;
    }

    public Client(string firstName, string middleName, string lastName, string phoneNumber, string email, string passportData)
    {
        Name = new Fullname(firstName, middleName, lastName);
        PhoneNumber = phoneNumber;
        Email = email;
        PassportData = passportData;
    }

    public bool Equals(IClient? right)
    {
        return _name.Equals(right.Name) && _phoneNumber == right.PhoneNumber && _email == right.Email && _passportData == right.PassportData;
    }

    public IFullname Name
    {
        get => _name;
        set => _name = value;
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (!string.IsNullOrEmpty(value) && IsDigitalOnly(value)) _phoneNumber = value;
            else if (string.IsNullOrEmpty(value)) throw new ArgumentException("Phone number will not be empty");
            else throw new ArgumentException("Phone number can contain only the numbers");
        }
    }

    private bool IsDigitalOnly(string str)
    {
        foreach (char i in str)
        {
            if(i<'0' || i>'9') return false;
        }
        return true;
    }

    public string Email
    {
        get => _email;
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Contains("@")) _email = value;
            else if (string.IsNullOrEmpty(value)) throw new ArgumentException("Email will not be empty");
            else throw new ArgumentException("Email should contain '@'");
        }
    }

    public string PassportData
    {
        get => _passportData;
        set
        {
            if (!string.IsNullOrEmpty(value) && IsDigitalOnly(value)) _passportData = value;
            else if (string.IsNullOrEmpty(value)) throw new ArgumentException("Passport data will not be empty");
            else throw new ArgumentException("Passport data can contain only number");
        }
    }

    private IFullname _name;
    private string _phoneNumber;
    private string _email;
    private string _passportData;
}