using System;

namespace CourseWork.MainLogic;

public interface IEmployeePersonalData:IHavePersonalData,IEquatable<IEmployeePersonalData>
{
    public string Position { get; set; }
}