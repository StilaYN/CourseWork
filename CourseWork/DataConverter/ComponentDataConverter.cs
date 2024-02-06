using System;
using CourseWork.MainLogic;
using Newtonsoft.Json.Converters;

namespace CourseWork.DataConverter;

public class ComponentDataConverter : CustomCreationConverter<IComponent>
{
    public Type ComponentDataType { get; private set; }

    public ComponentDataConverter(Type componentDataType)
    {
        ComponentDataType = componentDataType;
    }

    public override IComponent Create(Type objecType)
    {
        return (IComponent)Activator.CreateInstance(ComponentDataType);
    }
}