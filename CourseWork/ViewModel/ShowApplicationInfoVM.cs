using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Documents;
using CourseWork.DataBase;
using CourseWork.MainLogic;
using IComponent = CourseWork.MainLogic.IComponent;

namespace CourseWork.ViewModel;

public class ShowApplicationInfoVM:INotifyPropertyChanged
{
    private string _technic;
    private string _problem;
    private IMasterAccessProfile _source;
    private Master _master;
    private ShowApplicationInfo _window;
    private ObservableCollection<IOperation>? _operations;
    private ApplicationStatus _status;

    public ShowApplicationInfoVM(IMasterAccessProfile source,Master master, ShowApplicationInfo window)
    {
        _source = source;
        _window = window;
        _master = master;
        Technic = _master.SelectedApplication.TechnicName;
        Problem = _master.SelectedApplication.Problem;
        Status = _master.SelectedApplication.Status;
        Operation = new ObservableCollection<IOperation>(_master.SelectedApplication.Operations);
        _master.PropertyChanged += _master_PropertyChanged;

    }

    private void _master_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == "Application") Operation = (_master.SelectedApplication!=null)? new ObservableCollection<IOperation>(_master.SelectedApplication.Operations):null;
    }

    public ApplicationStatus Status
    {
        get
        {
            return _status;
        }
        set
        {
            if (_status == value) return;
            _status = value;
            OnPropertyChanged("Status");
            OnPropertyChanged("IsAccepted");
            OnPropertyChanged("IsAtWork");
            OnPropertyChanged("IsCompleted");
            OnPropertyChanged("IsCollected");
            OnPropertyChanged("IsRejection");
        }
    }

    private void AddOperationMethod(string name, int day, int month, int year, IComponent? component, int price)
    {
        _source.AddOperation(_master.SelectedApplication, name, day, month, year, component, price);
    }
    public ICommand AddOperation => new Command(obj =>
    {
        var add = new AddOperationWindow();
        add.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        var info = new AddOperationVM(add,_source,AddOperationMethod);
        add.DataContext = info;
        add.ShowDialog();
    });
    public ICommand RemoveOperation => new Command(obj =>
    {
        _source.RemoveOperation(_master.SelectedApplication,SelectedOperation);
    }, obj =>
    {
        return SelectedOperation != null;
    });
    public ICommand Close => new Command(obj =>
    {
        _window.Close();
    });
    public string Technic
    {
        get => _technic;
        set
        {
            _technic = value;
            OnPropertyChanged("Technic");
        }
    }

    public string Problem
    {
        get => _problem;
        set
        {
            _problem = value;
            OnPropertyChanged("Problem");
        }
    }
    public ObservableCollection<IOperation>? Operation
    {
        get => _operations;
        set
        {
            _operations = value;
            OnPropertyChanged("Operation");
        }
    }
    public IOperation? SelectedOperation { get; set; }

    public bool IsAccepted
    {
        get
        {
            return Status==ApplicationStatus.Accepted;
        }
        set
        {
            Status = value ? ApplicationStatus.Accepted : Status;
        }
    }

    public bool IsAtWork {
        get
        {
            return Status == ApplicationStatus.AtWork;
        }
        set
        {
            Status = value ? ApplicationStatus.AtWork : Status;
        }
    }
    public bool IsCompleted {
        get
        {
            return Status == ApplicationStatus.Completed;
        }
        set
        {
            Status = value ? ApplicationStatus.Completed : Status;
        }
    }
    public bool IsCollected {
        get
        {
            return Status == ApplicationStatus.Collected;
        }
        set
        {
            Status = value ? ApplicationStatus.Collected : Status;
        }
    }
    public bool IsRejection {
        get
        {
            return Status == ApplicationStatus.Rejection;
        }
        set
        {
            Status = value ? ApplicationStatus.Rejection : Status;
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }

}