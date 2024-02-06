using System;
using CourseWork.MainLogic;
using System.Collections.Generic;
using System.ComponentModel;

namespace CourseWork.DataBase;

public class ManagerAccessProfile:IManagerAccessProfile
{
    private IEmployeeDataBase _employeeDataBase;
    private IApplicationDataBase _applicationDataBase;

    public ManagerAccessProfile()
    {
        _employeeDataBase = new EmployeeDataBase();
        _applicationDataBase = new ApplicationDataBase();
    }
    public List<IApplication>? Applications=> _applicationDataBase.Applications;

    public List<IApplication>? FindApplicationsByMasterName(string name)
    {
        List<IApplication> foundedApplication = new List<IApplication>();
        foreach (var application in _applicationDataBase.Applications)
        {
            if (application.Employee.Name.GetFullName.Contains(name))
            {
                foundedApplication.Add(application);
            }
        }

        if (foundedApplication.Count > 0) return foundedApplication;
        else throw new ArgumentException("Applications was not found");
    }

    public List<IEmployeePersonalData>? EmployeePersonalData =>
        new List<IEmployeePersonalData>(_employeeDataBase.EmployeeList);

    public List<IEmployeePersonalData>? FindEmployeeByName(string name)
    {
        List<IEmployeePersonalData> foundedEmployees = new List<IEmployeePersonalData>();
        foreach (var i in _employeeDataBase.EmployeeList)
        {
            if(i.Name.GetFullName.Contains(name)) foundedEmployees.Add(i);
        }

        if (foundedEmployees.Count > 0)
        {
            return foundedEmployees;
        }
        else throw new ArgumentException("Employee was not found");
    }
    public List<IClient> Clients
    {
         get
         {
             List<IClient> clients = new List<IClient>();
             foreach (var item in _applicationDataBase.Applications)
             {
                 clients.Add(item.Client);
             }

             return clients;
         }
    }

    public List<IClient> FindClientsByName(string name)
    {
        List<IClient> foundedClient = new List<IClient>();
        foreach (var item in _applicationDataBase.Applications)
        {
            if(item.Client.Name.GetFullName.Contains(name)) foundedClient.Add(item.Client);
        }

        if (foundedClient.Count > 0) return foundedClient;
        else throw new ArgumentException("Client was not found");
    }
    public List<IOperationItem>? Operations {
        get
         {
             List<IOperationItem> _operations = new List<IOperationItem>();
             foreach (var i in _applicationDataBase.Applications)
             {
                 foreach (var item in i.Operations)
                 {
                     _operations.Add(new OperationItem(i.Employee, item));
                 }
             }

             return _operations;
        }

    }

    public List<IOperationItem>? FindOperationsByMasterName(string name)
    {
        List<IOperationItem> _operations = new List<IOperationItem>();
        foreach (var i in _applicationDataBase.Applications)
        {
            if (i.Employee.Name.GetFullName.Contains(name))
            {
                foreach (var item in i.Operations)
                {
                    _operations.Add(new OperationItem(i.Employee, item));
                }
            }
        }

        if (_operations.Count > 0) return _operations;
        else throw new ArgumentException("Operations was not found");
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    //
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}