using System;

namespace CourseWork.MainLogic;

public interface IDate:IEquatable<IDate>
{
    int Day { get; set; }
    int Month { get; set; }
    int Year { get; set; }
    public string GetStringDate { get; }
}