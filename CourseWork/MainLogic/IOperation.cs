using System;

namespace CourseWork.MainLogic;

public interface IOperation:IEquatable<IOperation>
{
    string Name { get; set; }
    IDate Date { get; set; }
    IComponent? UsedComponent { get; set; }
    int Price { get; set; }
    int TotalPrice { get; }

}