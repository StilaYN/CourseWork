using System.Collections.Generic;
using System.Windows.Documents;
using CourseWork.DataBase;

namespace CourseWork.WorkWithFile;

public interface IStorageFileManager:ILoad<List<IStorageItem>>, ISave<List<IStorageItem>>
{
    
}