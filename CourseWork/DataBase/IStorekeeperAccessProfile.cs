using System.Collections.Generic;
using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public interface IStorekeeperAccessProfile:IAccessProfile, IHaveAccessToStore
{
    void AddComponent(IComponent  component,int number=1);
    void AddComponent(string technnicType, string componentType, string name, string article, int price, int number = 1);
    void RemoveComponent(IComponent  component,int number=1);
}