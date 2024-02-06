using System;
using CourseWork.MainLogic;
using Newtonsoft.Json.Converters;

namespace CourseWork.DataConverter;

public class OperationDataConvert : CustomCreationConverter<IOperation>
{
    public Type OperationDataType { get; private set; }

    public OperationDataConvert(Type operationDataType)
    {
        OperationDataType = operationDataType;
    }

    public override IOperation Create(Type objectType)
    {
        return (IOperation)Activator.CreateInstance(OperationDataType);
    }
}