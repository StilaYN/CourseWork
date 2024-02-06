using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CourseWork.DataBase;
using CourseWork.MainLogic;

namespace CourseWork.ViewModel;

public class Manager:IWorkProfile,INotifyPropertyChanged
{
    private string _name;
    private IManagerAccessProfile _source;
    private ObservableCollection<IApplication>? _applications;
    private ObservableCollection<IEmployeePersonalData>? _employeePersonalData;
    private ObservableCollection<IClient>? _clients;
    private ObservableCollection<IOperationItem>? _operationItems;

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public ObservableCollection<IApplication> Applications
    {
        get => _applications;
        set { _applications = value;OnPropertyChanged(""); }
    }

    public ObservableCollection<IEmployeePersonalData> EmployeePersonalData
    {
        get => _employeePersonalData;
        set { _employeePersonalData = value;OnPropertyChanged(""); }
    }

    public ObservableCollection<IClient> Clients
    {
        get => _clients;
        set { _clients = value;OnPropertyChanged(""); }
    }

    public ObservableCollection<IOperationItem> OperationItems
    {
        get => _operationItems;
        set { _operationItems = value;OnPropertyChanged(""); }
    }

    public Manager(string name, IManagerAccessProfile source)
    {
        _name = name;
        _source = source;
        Applications = new ObservableCollection<IApplication>(_source.Applications);
        EmployeePersonalData = new ObservableCollection<IEmployeePersonalData>(_source.EmployeePersonalData);
        Clients = new ObservableCollection<IClient>(_source.Clients);
        OperationItems = new ObservableCollection<IOperationItem>(_source.Operations);
    }

    public void StartWork()
    {
        Applications = new ObservableCollection<IApplication>(_source.Applications);
        EmployeePersonalData = new ObservableCollection<IEmployeePersonalData>(_source.EmployeePersonalData);
        Clients = new ObservableCollection<IClient>(_source.Clients);
        OperationItems = new ObservableCollection<IOperationItem>(_source.Operations);
        var window = new ManagerWindow();
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

    private List<IApplication>? GetApplication()
    {
        return _source.Applications;
    }

    private List<IEmployeePersonalData>? GetEmployeePersonalData()
    {
        return _source.EmployeePersonalData;
    }

    private List<IClient>? GetClient()
    {
        return _source.Clients;
    }

    private List<IOperationItem>? GetOperation()
    {
        return _source.Operations;
    }

    public ICommand ShowApplication => new Command(obj =>
    {
        var window = new ShowApplicationWindow();
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.DataContext = new ShowApplicationVM(window, GetApplication, _source.FindApplicationsByMasterName);
        window.ShowDialog();
    });
    public ICommand ShowEmployee => new Command(obj =>
    {
        var window = new ShowEmployeeWindow();
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.DataContext = new ShowEmployeeVM(window, GetEmployeePersonalData, _source.FindEmployeeByName);
        window.ShowDialog();
    });
    public ICommand ShowClient => new Command(obj =>
    {
        var window = new ShowClientWindow();
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.DataContext = new ShowClientVM(window, GetClient, _source.FindClientsByName);
        window.ShowDialog();
    });
    public ICommand ShowOperation => new Command(obj =>
    {
        var window = new ShowOpeationWindow();
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.DataContext = new ShowOperationVM(window, GetOperation, _source.FindOperationsByMasterName);
        window.ShowDialog();
    });
}