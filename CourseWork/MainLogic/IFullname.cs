using System;

namespace CourseWork.MainLogic;

public interface IFullname:IEquatable<IFullname>
{
    public string FirstName { get; }
    public string? MiddleName { get; }
    public string LastName { get; }
    public string GetFullName { get; }
}