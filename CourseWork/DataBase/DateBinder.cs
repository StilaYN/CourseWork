using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public class DateBinder:IDateBinder
{
    public IDate Create(int day, int month, int year)
    {
        return new Date(day, month, year);
    }
}