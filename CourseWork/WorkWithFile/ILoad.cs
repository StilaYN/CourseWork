namespace CourseWork.WorkWithFile;

public interface ILoad<Type>
{
    Type? Load(string path);
}