using System.Collections.Generic;

namespace CourseWork.DataBase;

public interface IHaveAccessToStore:IAccessProfile
{
    List<IStorageItem> Storage { get; }
    List<IStorageItem> FindItem(string item);
}