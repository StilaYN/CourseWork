using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace CourseWork.MainLogic;

public class Employee:AbstractEmployee, IEmployee
{
    public Employee():base()
    {

    }

    public Employee(string login, string password, byte[] salt, IFullname name, string phoneNumber, string email, string position, List<EmployeeAccess> access) : base(login, password, salt, name, phoneNumber, email, position, access)
    {
    }
}
