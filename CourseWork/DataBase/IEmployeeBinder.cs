using CourseWork.MainLogic;
using System.Collections.Generic;

namespace CourseWork.DataBase;

public interface IEmployeeBinder
{
    IEmployee Create(string login, string password, string name, string middleName, string lastName, string phoneNumber,
        string email, string position, List<EmployeeAccess> accesses);
}