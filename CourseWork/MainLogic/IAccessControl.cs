using System.Collections.Generic;
using System.Collections.ObjectModel;
using CourseWork.ViewModel;

namespace CourseWork.MainLogic;

public interface IAccessControl
{
    List<IWorkProfile> Authorize(string login, string password);
}