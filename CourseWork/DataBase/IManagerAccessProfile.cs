using CourseWork.MainLogic;
using System.Collections.Generic;

namespace CourseWork.DataBase;

public interface IManagerAccessProfile:IAccessProfile
{
    List<IApplication>? Applications { get; }
    List<IApplication>? FindApplicationsByMasterName(string name);
    List<IEmployeePersonalData>? EmployeePersonalData { get; }
    List<IEmployeePersonalData>? FindEmployeeByName(string name);
    List<IClient>? Clients { get; }
    List<IClient> FindClientsByName(string name);
    List<IOperationItem>? Operations { get; }
    List<IOperationItem>? FindOperationsByMasterName(string name);
}