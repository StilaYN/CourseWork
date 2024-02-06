using System.Collections.Generic;
using System.Windows.Documents;
using CourseWork.MainLogic;

namespace CourseWork.WorkWithFile;

public interface IApplicationFileManager:ILoad<List<IApplication>> ,ISave<List<IApplication>>
{
    
}