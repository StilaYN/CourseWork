using System;
using System.Text.Json.Serialization;

namespace CourseWork.MainLogic;

public class Fullname:IFullname
{
    private string _firstName;//Имя
    private string? _middleName;//Отчество
    private string _lastName;//Фамилия

    public Fullname()
    {

    }
    public Fullname(string firstName, string? middleName, string lastName)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }

    public bool Equals(IFullname? right)
    {
        if (right != null)
        {
            if (FirstName == right.FirstName && MiddleName == right.MiddleName &&
                LastName == right.LastName) return true;
            return false;
        }

        return false;
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (!string.IsNullOrEmpty(value)) _firstName = value;
            else throw new ArgumentException("First name will not be empty");
        }
    }

    public string? MiddleName
    {
        get => _middleName;
        set => _middleName = value;
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (!string.IsNullOrEmpty(value)) _lastName = value;
            else throw new ArgumentException("Last name will not be empty");
        }
    }

    public string GetFullName
    {
        get
        {
            return _lastName + " " + _firstName + " " + _middleName;
        }
    }
}