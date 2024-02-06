using System;
using CourseWork.MainLogic;
using Newtonsoft.Json.Converters;

namespace CourseWork.DataConverter;

public class DateDataConverter : CustomCreationConverter<IDate>
{
    public Type DateDataType { get; private set; }

    public DateDataConverter(Type dateDataType)
    {
        DateDataType = dateDataType;
    }

    public override IDate Create(Type objecType)
    {
        return (IDate)Activator.CreateInstance(DateDataType);
    }
}