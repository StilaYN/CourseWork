using CourseWork.MainLogic;

namespace CourseWork.DataBase;

public interface IDateBinder
{
    IDate Create(int day,int month, int year);
}