using System.Collections.Generic;
using CourseWork.MainLogic;
using CourseWork.ViewModel;

namespace CourseWork.DataBase;

public interface IWorkProfileBinder
{
    List<IWorkProfile> Create(IEmployeeDataBase dataBase, List<EmployeeAccess> employeeAccesses, IEmployee employee);
}