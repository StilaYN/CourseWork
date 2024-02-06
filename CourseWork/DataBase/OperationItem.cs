using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public class OperationItem:IOperationItem
{
    public OperationItem(IEmployeePersonalData personalData, IOperation completedOperation)
    {
        PersonalData = personalData;
        CompletedOperation = completedOperation;
    }

    public IEmployeePersonalData PersonalData { get; set; }
    public IOperation CompletedOperation { get; set; }
}