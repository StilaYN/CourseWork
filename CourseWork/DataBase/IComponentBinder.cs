using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public interface IComponentBinder
{
    IComponent Create(string technnicType, string componentType, string name, string article, int price);
}