using CourseWork.MainLogic;
using System.Collections.Generic;
using System.ComponentModel;
using IComponent = CourseWork.MainLogic.IComponent;

namespace CourseWork.DataBase;

public class MasterAccessProfile:IMasterAccessProfile
{
    private IApplicationDataBase _applicationDataBase;
    private IStorageDataBase _storageDataBase;

    public MasterAccessProfile()
    {
        _applicationDataBase = new ApplicationDataBase();
        _storageDataBase = new StorageDataBase();
    }
    public void ChangeStatus(IApplication application, ApplicationStatus status)
    {
        _applicationDataBase.ChangeStatus(application,status);
        OnPropertyChanged("ApplicationList");
    }
    public void RemoveOperation(IApplication application, IOperation operation)
    {
        _applicationDataBase.RemoveOperation(application,operation);
        if (operation.UsedComponent != null)
        {
            _storageDataBase.AddComponent(operation.UsedComponent);
        }
        OnPropertyChanged("ApplicationList");
    }
    public void AddOperation(IApplication application, string name, int day, int month, int year, IComponent? component,
        int price)
    {
        _applicationDataBase.AddOperation(application,name,day,month,year,component,price);
        if (component != null) _storageDataBase.RemoveComponent(component);
        OnPropertyChanged("ApplicationList");
    }

    public void RemoveApplication(IApplication application)
    {
        _applicationDataBase.RemoveApplication(application);
        OnPropertyChanged("ApplicationList");
    }

    public void AddApplication(string technicName, string problem, IEmployeePersonalData employee, string firstName,
        string middleName, string lastName, string phoneNumber, string email, string passportData, int day, int month,
        int year)
    {
        _applicationDataBase.AddApplication(technicName,problem,employee,firstName,middleName,lastName,phoneNumber,email,passportData,day,month,year);
        OnPropertyChanged("ApplicationList");
    }

    public List<IApplication> ThisMasterApplication(IEmployeePersonalData master)
    {
        List<IApplication> thisMasterApplications = new List<IApplication>();
        foreach (var i in _applicationDataBase.Applications)
        {
            if (i.Employee.Equals(master) && i.Status != ApplicationStatus.Collected &&
                i.Status != ApplicationStatus.Rejection) thisMasterApplications.Add(i);
        }

        return thisMasterApplications;
    }
    public List<IStorageItem> Storage => _storageDataBase.Storage;

    public List<IStorageItem> FindItem(string item)
    {
        return _storageDataBase.FindItem(item);
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}