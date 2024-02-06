using CourseWork.MainLogic;
using System.Collections.Generic;

namespace CourseWork.DataBase;

public interface IApplicationDataBase
{
    public List<IApplication> Applications { get; }
    void AddApplication(string technicName, string problem, IEmployeePersonalData employee, string firstName,
        string middleName, string lastName, string phoneNumber, string email, string passportData, int day, int month, int year);
    void RemoveApplication(IApplication application);
    void AddOperation(IApplication application, string name, int day, int month, int year, IComponent? component, int price);
    void RemoveOperation(IApplication application, IOperation operation);
    void ChangeStatus(IApplication application, ApplicationStatus status);
}