using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public interface IApplicationBinder
{
    IApplication Create(string technicName, string problem, IEmployeePersonalData employee, IClient client, IDate date); 
}