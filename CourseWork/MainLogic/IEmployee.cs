using System;

namespace CourseWork.MainLogic;

public interface IEmployee : IEmployeePersonalData, ICanAuthorize,IHaveAccess, IEquatable<IEmployee>
{
    string AccessToString { get; }
}