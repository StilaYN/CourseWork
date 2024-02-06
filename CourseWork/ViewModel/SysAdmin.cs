using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CourseWork.DataBase;
using CourseWork.MainLogic;

namespace CourseWork.ViewModel;

public class SysAdmin:IWorkProfile,INotifyPropertyChanged
{
    private string _name; 
    private ISysAdminAccessProfile _source;
    private ObservableCollection<IEmployee> _employees;
    private Visibility _visibile;
    private string _errorText;
    private string? _search;

    public string? Search
    {
        get => _search;
        set { _search = value;OnPropertyChanged("Search"); }
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

    public ObservableCollection<IEmployee> Employees
    {
        get => _employees;
        set { _employees = value;OnPropertyChanged("Employees"); }
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }
    public IEmployee ThisEmployee { get; set; }
    public SysAdmin(string name, ISysAdminAccessProfile source,IEmployee thisEmployee)
    {
        _name = name;
        _source = source;
        ThisEmployee = thisEmployee;
        _source.PropertyChanged += SourcePropertyChangedEventHandler;
    }
    public IEmployee? SelectedEmployee { get; set; }

    private void SourcePropertyChangedEventHandler(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Employees = new ObservableCollection<IEmployee>(_source.EmployeeList);
    }

    public ICommand DeleteUser => new Command(obj =>
        {
            _source.DeleteUser(SelectedEmployee);
            Search = null;
        }, obj =>
        {
            return SelectedEmployee!=null && !ThisEmployee.Equals((IEmployeePersonalData)SelectedEmployee);
        }

    );

    public ICommand ChangePassword => new Command(obj =>
    {
        var enterWindow = new EnterLoginWindow();
        enterWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var info = new EnterLoginVM(enterWindow,"New Password");
        enterWindow.DataContext = info;
        enterWindow.ShowDialog();
        if (info.CanCreate)
        {
            _source.ChangePassword(SelectedEmployee,info.Field);
            Search = null;
        }
    }, obj => SelectedEmployee != null);
    public ICommand ChangeLogin => new Command(obj =>
    {
        var enterWindow = new EnterLoginWindow();
        enterWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var info = new EnterLoginVM(enterWindow, "New Login");
        enterWindow.DataContext = info;
        enterWindow.ShowDialog();
        if (info.CanCreate)
        {
            _source.ChangeLogin(SelectedEmployee,info.Field);
            Search = null;
        }
    }, obj => SelectedEmployee != null);

    private void AddUserFunction(string login, string password, string name, string middleName, string lastName,
        string phoneNumber,
        string email, string position, List<EmployeeAccess> accesses)
    {
        _source.AddUser(login, password, name, middleName, lastName, phoneNumber, email, position, accesses);
    }
    public ICommand AddUser => new Command(obj =>
    {
        var addUserWindow = new AddUserWindow();
        addUserWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var user = new AddUserVM(addUserWindow,AddUserFunction);
        addUserWindow.DataContext = user;
        addUserWindow.ShowDialog();
        Search = null;
    });

    private void ChangeAccessMethod(List<EmployeeAccess> accesses)
    {
        _source.ChangeAccess(SelectedEmployee, accesses);
    }
    public ICommand ChangeAccess => new Command(obj =>
    {
        var enterWindow = new ChangeAccessWindow();
        enterWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var info = new ChangeAccessVM(enterWindow,ChangeAccessMethod);
        enterWindow.DataContext = info;
        enterWindow.ShowDialog();
        Search = null;

    }, obj => SelectedEmployee != null);


    public ICommand Find=> new Command(obj =>
    {
        try
        {
            Employees = new ObservableCollection<IEmployee>(_source.FindEmployeeBySurname(Search));
            Visible = Visibility.Hidden;
        }
        catch (ArgumentException e)
        {
            Visible = Visibility.Visible;
            ErrorText = e.Message;
        }
    },obj=> !string.IsNullOrEmpty(Search));
    public ICommand Clear=> new Command(obj =>
    {
        Employees = new ObservableCollection<IEmployee>(_source.EmployeeList);
        Visible = Visibility.Hidden;
        Search = null;

    }, obj => !string.IsNullOrEmpty(Search));
    public void StartWork()
    {
        Employees = new ObservableCollection<IEmployee>(_source.EmployeeList);
        SysAdminWindow window = new SysAdminWindow();
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.DataContext = this;
        window.Show();
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}