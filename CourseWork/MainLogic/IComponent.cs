using System;
using Microsoft.VisualBasic.CompilerServices;

namespace CourseWork.MainLogic;

public interface IComponent:IEquatable<IComponent>
{
    string TechnicType { get; set; }
    string ComponentType { get; set; }
    string Name { get; set; }
    string Article { get; set; }
    int Price { get; set; }
    string ComponentToString();
}