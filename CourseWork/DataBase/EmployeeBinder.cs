using System;
using CourseWork.MainLogic;
using System.Collections.Generic;
using Accessibility;

namespace CourseWork.DataBase;

public class EmployeeBinder:IEmployeeBinder
{
    public EmployeeBinder()
    {

    }
    public IEmployee Create(string login, string password, string name, string middleName, string lastName, string phoneNumber,
        string email, string position, List<EmployeeAccess> accesses)
    {
        if (login == null)
        {
            throw new ArgumentException("Login will not be empty");

        }
        if (password == null) {  throw new ArgumentException("Password will not be empty"); }
        var salt = AccessControl.GenerateSalt();
        var saltedPassword = AccessControl.GenerateSaltPassword(password, salt);
        IFullname fullname = new Fullname(name, middleName,lastName);
        IEmployee employee = new Employee(login, saltedPassword, salt, fullname, phoneNumber, email,
            position, accesses);
        return employee;
    }
}