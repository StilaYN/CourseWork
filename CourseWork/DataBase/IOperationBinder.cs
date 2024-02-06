using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public interface IOperationBinder
{
    IOperation Create(string name, int day, int month, int year, IComponent? component, int price);
}