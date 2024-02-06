using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public interface IOperationItem
{
    IEmployeePersonalData PersonalData { get; set; }
    IOperation CompletedOperation { get; set; } 
}