using CourseWork.MainLogic;
using CourseWork.WorkWithFile;
using System.Collections.Generic;
using System.ComponentModel;
using IComponent = CourseWork.MainLogic.IComponent;

namespace CourseWork.DataBase;

public class ApplicationDataBase:IApplicationDataBase,INotifyPropertyChanged
{
    private IApplicationBinder _applicationBinder;
    private List<IApplication> _applicationList;
    private IDateBinder _dateBinder;
    private IClientBinder _clientBinder;
    private IOperationBinder _operationBinder;
    private string _applicationPath;
    private IApplicationFileManager _applicationFileManager;

    public ApplicationDataBase()
    {
        _clientBinder = new ClientBinder();
        _applicationBinder = new ApplicationBinder();
        _operationBinder = new OperationBinder();
        _dateBinder = new DateBinder();
        _operationBinder = new OperationBinder();
        _dateBinder = new DateBinder();
        _applicationPath = "Application.json";
        _applicationFileManager = new ApplicationFileManager();
        _applicationList = _applicationFileManager.Load(_applicationPath);
    }
    public List<IApplication> Applications => _applicationList;


    public void ChangeStatus(IApplication application, ApplicationStatus status)
    {
        application.Status = status;
        _applicationFileManager.Save(_applicationList, _applicationPath);
        OnPropertyChanged("ApplicationList");
    }

    public void RemoveOperation(IApplication application, IOperation operation)
    {
        application.RemoveOperation(operation);
        _applicationFileManager.Save(_applicationList, _applicationPath);
        OnPropertyChanged("ApplicationList");
    }

    public void AddOperation(IApplication application, string name, int day, int month, int year, IComponent? component,
        int price)
    {
        IOperation operation = _operationBinder.Create(name, day, month, year, component, price);
        application.AddOperation(operation);
        _applicationFileManager.Save(_applicationList, _applicationPath);
        OnPropertyChanged("ApplicationList");
    }

    public void RemoveApplication(IApplication application)
    {
        foreach (var i in _applicationList)
        {
            if (i.Equals(application))
            {
                _applicationList.Remove(i);
                break;
            }
        }

        _applicationFileManager.Save(_applicationList, _applicationPath);
        OnPropertyChanged("ApplicationList");
    }

    public void AddApplication(string technicName, string problem, IEmployeePersonalData employee, string firstName,
        string middleName, string lastName, string phoneNumber, string email, string passportData, int day, int month,
        int year)
    {
        IClient client = _clientBinder.Create(firstName, middleName, lastName, phoneNumber, email, passportData);
        IDate date = _dateBinder.Create(day, month, year);
        IApplication application = _applicationBinder.Create(technicName, problem, employee, client, date);
        _applicationList.Add(application);
        _applicationFileManager.Save(_applicationList, _applicationPath);
        OnPropertyChanged("ApplicationList");
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}