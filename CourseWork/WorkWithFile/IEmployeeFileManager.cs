using System.Collections.Generic;
using System.Windows.Documents;
using CourseWork.MainLogic;

namespace CourseWork.WorkWithFile;

public interface IEmployeeFileManager:ILoad<List<IEmployee>>, ISave<List<IEmployee>>
{
    
}