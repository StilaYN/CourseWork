using System;
using CourseWork.MainLogic;
using Newtonsoft.Json.Converters;

namespace CourseWork.DataConverter;

public class FullnameDataConverter : CustomCreationConverter<IFullname>
{
    public Type FullnameDataType { get; private set; }
    public FullnameDataConverter(Type fullnameDataType)
    {
        FullnameDataType = fullnameDataType;
    }

    public override IFullname Create(Type objectType)
    {
        return (IFullname)Activator.CreateInstance(FullnameDataType);
    }
}