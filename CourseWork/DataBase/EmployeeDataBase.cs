using CourseWork.MainLogic;
using CourseWork.ViewModel;
using CourseWork.WorkWithFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CourseWork.DataBase;

public class EmployeeDataBase:INotifyPropertyChanged,IEmployeeDataBase
{
    private List<IEmployee> _employeeList;
    private IEmployeeBinder _employeeBinder;
    private string _employeePath;
    private IEmployeeFileManager _employeeFileManager;
    private IWorkProfileBinder _workProfileBinder;

    public EmployeeDataBase()
    {
        _employeeBinder = new EmployeeBinder();
        _employeePath = "Employee.json";
        _employeeFileManager = new EmployeeFileManager();
        _employeeList = _employeeFileManager.Load(_employeePath);
        _workProfileBinder = new WorkProfileBinder();
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public void ChangeAccess(IEmployee employee, List<EmployeeAccess> newAccess)
    {
        foreach (var i in _employeeList)
        {
            if (i.Equals(employee))
            {
                i.Access = newAccess;
                _employeeFileManager.Save(_employeeList, _employeePath);
                OnPropertyChanged("EmployeeList");
                break;
            }
        }
    }
    public void ChangeLogin(IEmployee employee, string newLogin)
    {
        foreach (var i in _employeeList)
        {
            if (i.Equals(employee))
            {
                i.Login = newLogin;
                _employeeFileManager.Save(_employeeList, _employeePath);
                OnPropertyChanged("EmployeeList");
                break;
            }
        }
    }
    public void ChangePassword(IEmployee employee, string newPassword)
    {
        foreach (var i in _employeeList)
        {
            if (i.Equals(employee))
            {
                i.Salt = AccessControl.GenerateSalt();
                i.Password = AccessControl.GenerateSaltPassword(newPassword, i.Salt);
                _employeeFileManager.Save(_employeeList, _employeePath);
                OnPropertyChanged("EmployeeList");
                break;
            }
        }
    }
    public List<IEmployee> EmployeeList => _employeeList;
    public List<IEmployee> FindEmployeeBySurname(string personalDate)
    {
        List<IEmployee> foundUser = new List<IEmployee>();
        foreach (var i in _employeeList)
        {
            if (i.Name.GetFullName.Contains(personalDate))
            {
                foundUser.Add(i);
            }
        }

        if (foundUser.Count > 0)
        {
            return foundUser;
        }
        throw new ArgumentException("Users with this name was not found");
    }

    public void AddUser(string login, string password, string name, string middleName, string lastName,
        string phoneNumber,
        string email, string position, List<EmployeeAccess> accesses)
    {
        _employeeList.Add(_employeeBinder.Create(login, password, name, middleName, lastName, phoneNumber, email,
            position, accesses));
        _employeeFileManager.Save(_employeeList, _employeePath);
        OnPropertyChanged("EmployeeList");
    }

    public void DeleteUser(IEmployee employee)
    {
        foreach (var i in _employeeList)
        {
            if (i.Equals(employee))
            {
                _employeeList.Remove(i);
                _employeeFileManager.Save(_employeeList, _employeePath);
                OnPropertyChanged("EmployeeList");
                break;
            }
        }
    }
    public List<ICanAuthorize> AuthorizeList => new List<ICanAuthorize>(_employeeList);

    public List<IWorkProfile> FindWorkProfile(ICanAuthorize element)
    {
        foreach (var i in _employeeList)
        {
            if (i.Equals(element)) return _workProfileBinder.Create(this, i.Access, i);
        }

        throw new ArgumentException("Element was not found");
    }

    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}