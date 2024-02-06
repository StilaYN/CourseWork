using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public class ComponentBinder:IComponentBinder
{
    public IComponent Create(string technnicType, string componentType, string name, string article, int price)
    {
        return new Component(technnicType, componentType, name, article, price);
    }
}