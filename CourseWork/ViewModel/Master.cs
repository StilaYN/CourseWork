using System;
using CourseWork.DataBase;
using CourseWork.MainLogic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.ViewModel;

public class Master:IWorkProfile,INotifyPropertyChanged
{
    private IEmployeePersonalData _employeePersonalData;
    private IMasterAccessProfile _source;
    private ObservableCollection<IApplication> _application;


    public IEmployeePersonalData EmployeePersonalData
    {
        get => _employeePersonalData;
        set => _employeePersonalData = value ?? throw new ArgumentNullException(nameof(value));
    }


    public ObservableCollection<IApplication> Application
    {
        get => _application;
        set { _application = value;OnPropertyChanged("Application"); }
    }
    public IApplication? SelectedApplication { get; set; }

    public string Name { get; set; }

    public Master(string name, IMasterAccessProfile source, IEmployeePersonalData employeePersonalData)
    {
        Name = name;
        _employeePersonalData = employeePersonalData;
        _source = source;
        _source.PropertyChanged += SourcePropertyChangedEventHandler;
    }
    private void SourcePropertyChangedEventHandler(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Application = new ObservableCollection<IApplication>(_source.ThisMasterApplication(_employeePersonalData));
    }

    public void StartWork()
    {
        Application = new ObservableCollection<IApplication>(_source.ThisMasterApplication(_employeePersonalData));
        MasterWindow window = new MasterWindow();
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

    private void CreateApplication(string technicName, string problem,string firstName,
        string middleName, string lastName, string phoneNumber, string email, string passportData, int day, int month,
        int year)
    {
        _source.AddApplication(technicName, problem, EmployeePersonalData, firstName, middleName, lastName, phoneNumber, email, passportData, day, month, year);
    }
    public ICommand AddApplication => new Command(obj =>
    {
        var addApplicationWindow = new AddApplication();
        addApplicationWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var info = new AddApplicationVM(addApplicationWindow,CreateApplication);
        addApplicationWindow.DataContext = info;
        addApplicationWindow.ShowDialog();
    });
    public ICommand RemoveApplication => new Command(obj =>
    {
        _source.RemoveApplication(SelectedApplication);
    }, obj =>
    {
        return SelectedApplication != null;
    });
    public ICommand ShowInfo=> new Command(obj =>
    {
        var show = new ShowApplicationInfo();
        show.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var info = new ShowApplicationInfoVM(_source,this,show);
        show.DataContext = info;
        show.ShowDialog();
        _source.ChangeStatus(SelectedApplication,info.Status);
    }, obj =>
    {
        return SelectedApplication != null;
    });
}