using System.Collections.Generic;
using CourseWork.MainLogic;
using CourseWork.ViewModel;

namespace CourseWork.DataBase;

public interface IHaveAuthorizeInfo
{
    List<ICanAuthorize> AuthorizeList { get; }
    List<IWorkProfile> FindWorkProfile(ICanAuthorize element);
}