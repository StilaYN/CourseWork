using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;

namespace CourseWork.MainLogic;

public class AbstractEmployee:IEmployee
{
    public AbstractEmployee()
    {
    }

    public AbstractEmployee(string login, string password, byte[] salt, IFullname name, string phoneNumber, string email, string position, List<EmployeeAccess> access)
    {
        Login = login;
        Password = password;
        _salt = salt;
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Position = position;
        Access = access;
    }


    public virtual string PhoneNumber
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
            if (i < '0' || i > '9') return false;
        }
        return true;
    }
    public virtual string Email
    {
        get => _email;
        set {
            if (!string.IsNullOrEmpty(value) && value.Contains("@")) _email = value;
            else if (string.IsNullOrEmpty(value)) throw new ArgumentException("Email will not be empty");
            else throw new ArgumentException("Email should contain '@'");
        }
    }

    public virtual List<EmployeeAccess> Access
    {
        get => _access;
        set => _access = value;
    }

    public virtual IFullname Name
    {
        get => _name;
        set
        {
            _name = value;
        }
    }

    public string AccessToString => GetAccessString();

    private string GetAccessString()
    {
        string result  = string.Empty;
        foreach (var i in Access)
        {
            if(i==EmployeeAccess.Sysadmin) result += " Sys ";
            if(i==EmployeeAccess.Storekeeper) result += " Store ";
            if(i==EmployeeAccess.Manager) result += " Manag ";
            if(i==EmployeeAccess.Master) result += " Master ";
        }
        return result;
    }

    public virtual bool Equals(IEmployee right)
    {
        return Name.Equals(right.Name)&&PhoneNumber==right.PhoneNumber&&Email==right.Email&&Position==right.Position&&IsCollectionEqual(Access,right.Access)&&Equals((ICanAuthorize)right);
    }

    public virtual string Position
    {
        get { return _position; }
        set
        {
            if (!string.IsNullOrEmpty(value)) _position = value;
            else if (string.IsNullOrEmpty(value)) throw new ArgumentException("Position will not be empty");
        }
    }

    private bool IsCollectionEqual(List<EmployeeAccess> left, List<EmployeeAccess> right)
    {
        if(right.Count!=left.Count) return false;
        else
        {
            for (int i = 0; i < left.Count; i++)
            {
                if (right[i] != left[i]) return false;
            }
            return true;
        }
    }

    public bool Equals(IEmployeePersonalData right)
    {
        return Name.Equals(right.Name) && Position == right.Position;
    }

    public bool Equals(ICanAuthorize right)
    {
        return Login==right.Login&&Password==right.Password&&Salt==right.Salt;
    }

    private string _login;
    private string _password;
    private byte[] _salt;

    public string Login
    {
        get => _login;
        set => _login = value ?? throw new ArgumentException("Login will not be empty");
    }

    public string Password
    {
        get => _password;
        set => _password = value ?? throw new ArgumentException("Password will not be empty");
    }

    public byte[] Salt
    {
        get => _salt;
        set => _salt = value ?? throw new ArgumentNullException(nameof(value));
    }


    private IFullname _name;
    private string _phoneNumber;
    private string _email;
    private string _position;
    private List<EmployeeAccess> _access;
}