using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public interface IStorageItem
{
    IComponent Component { get;}
    int ComponentNumber { get; set; }
}