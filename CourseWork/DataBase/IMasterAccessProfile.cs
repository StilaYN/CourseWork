using System.Collections.Generic;
using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public interface IMasterAccessProfile:IAccessProfile,IHaveAccessToStore
{
    List<IApplication> ThisMasterApplication(IEmployeePersonalData master);
    void AddApplication(string technicName, string problem, IEmployeePersonalData employee, string firstName, 
        string middleName, string lastName, string phoneNumber, string email, string passportData, int day,int month,int year);
    void RemoveApplication(IApplication  application);
    void AddOperation(IApplication application, string name, int day, int month, int year, IComponent? component, int price);
    void RemoveOperation(IApplication application,IOperation  operation);
    void ChangeStatus(IApplication application,ApplicationStatus status);
    
}