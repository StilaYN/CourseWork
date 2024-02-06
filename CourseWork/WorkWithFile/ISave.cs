namespace CourseWork.WorkWithFile;

public interface ISave<Type>
{
    void Save(Type o,string path);
}