using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CourseWork.MainLogic;

namespace CourseWork.ViewModel;

public class AddUserVM:INotifyPropertyChanged
{
    private string _name;
    private string _surname;
    private string _middleName;
    private string _login;
    private string _password;
    private string _phoneNumber;
    private string _email;
    private string _position;
    private bool _isSysAdmin;
    private bool _isManager;
    private bool _isStorage;
    private bool _isMaster;
    private AddUserWindow _window;
    private AddFunction _addFunction;
    private Visibility _visibile;
    private string _errorText;

    public delegate void AddFunction(string login, string password, string name, string middleName, string lastName,
        string phoneNumber,
        string email, string position, List<EmployeeAccess> accesses);
    public AddUserVM(AddUserWindow window, AddFunction addFunction)
    {
        _window = window;
        _addFunction = addFunction;
    }

    public AddUserVM(AddUserWindow window, string name, string surname, string middleName, string login, string password, string phoneNumber, string email, string position, List<EmployeeAccess> Access)
    {
        _window = window;
        Name = name;
        Surname = surname;
        MiddleName = middleName;
        Login = login;
        Password = password;
        PhoneNumber = phoneNumber;
        Email = email;
        Position = position;
        foreach (var i in Access)
        {
            switch (i)
            {
                case EmployeeAccess.Sysadmin:
                    IsSysAdmin = true;
                    break;
                case EmployeeAccess.Manager:
                    IsManager = true;
                    break;
                case EmployeeAccess.Storekeeper:
                    IsStorage = true;
                    break;
                case EmployeeAccess.Master:
                    IsMaster = true;
                    break;
            }
        }
    }

    private List<EmployeeAccess> GetAccesses()
    {
        var list = new List<EmployeeAccess>();
        if(IsSysAdmin) list.Add(EmployeeAccess.Sysadmin);
        if(IsManager) list.Add(EmployeeAccess.Manager);
        if(IsStorage) list.Add(EmployeeAccess.Storekeeper);
        if(IsMaster) list.Add(EmployeeAccess.Master);
        return list;
    }
    public List<EmployeeAccess> Accesses => GetAccesses();

    public string Name
    {
        get => _name;
        set { _name = value;OnPropertyChanged("Name"); }
    }

    public string Surname
    {
        get => _surname;
        set { _surname = value;OnPropertyChanged("Surname"); }
    }

    public string MiddleName
    {
        get => _middleName;
        set { _middleName = value; OnPropertyChanged("MiddleName"); }
    }

    public string Login
    {
        get => _login;
        set { _login = value;OnPropertyChanged("Login"); }
    }

    public string Password
    {
        get => _password;
        set { _password = value;OnPropertyChanged("Password"); }
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set { _phoneNumber = value; OnPropertyChanged("PhoneNumber"); }
    }

    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged("Email"); }
    }

    public string Position
    {
        get => _position;
        set { _position = value;OnPropertyChanged("Position"); }
    }

    public bool IsSysAdmin
    {
        get => _isSysAdmin;
        set { _isSysAdmin = value; OnPropertyChanged("IsSysAdmin"); }
    }

    public bool IsManager
    {
        get => _isManager;
        set { _isManager = value;OnPropertyChanged("IsManager"); }
    }

    public bool IsStorage
    {
        get => _isStorage;
        set { _isStorage = value;OnPropertyChanged("IsStorage"); }
    }

    public bool IsMaster
    {
        get => _isMaster;
        set { _isMaster = value; OnPropertyChanged("IsMaster");}
    }
    public Visibility Visible
    {
        get => _visibile;
        set { _visibile = value; OnPropertyChanged("Visible"); }
    }
    public string ErrorText
    {
        get => _errorText;
        set { _errorText = value; OnPropertyChanged("ErrorText"); }
    }

    public ICommand Create => new Command(
        obj =>
        {
            try
            {
                _addFunction(Login, Password, Name, MiddleName, Surname, PhoneNumber, Email, Position, GetAccesses());
                _window.Close();
            }
            catch (ArgumentException e)
            {
                Visible = Visibility.Visible;
                ErrorText = e.Message;
            }
        },obj=> IsManager||IsMaster||IsStorage||IsSysAdmin);

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}