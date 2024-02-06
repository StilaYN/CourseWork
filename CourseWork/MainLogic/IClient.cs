using System;

namespace CourseWork.MainLogic;

public interface IClient:IEquatable<IClient>,IHavePersonalData
{
    public string PassportData { get; set; }
}