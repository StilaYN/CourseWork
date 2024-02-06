using CourseWork.DataBase;
using CourseWork.MainLogic;

namespace CourseWork.ViewModel;

public interface IWorkProfile
{
    string Name { get; set; }
    void StartWork();

}