using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public class OperationBinder:IOperationBinder
{
    public IOperation Create(string name, int day, int month, int year, IComponent? component, int price)
    {
        Operation operation = new Operation(name,component,price,new Date(day,month,year));
        return operation;
    }
}