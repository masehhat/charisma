using Charisma.Application.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Charisma.Application.Common.Views;

public class OperationResult<TResult>
{
    [JsonInclude]
    [JsonProperty]
    public TResult Result { get; private set; }

    [JsonInclude]
    [JsonProperty]
    public bool Success { get; private set; }

    [JsonInclude]
    [JsonProperty]
    public ErrorCode? ErrorCode { get; private set; }

    public static OperationResult<TResult> BuildSuccess(TResult result)
    {
        return new OperationResult<TResult>
        {
            ErrorCode = default,
            Success = true,
            Result = result
        };
    }

    public static OperationResult<TResult> BuildFailure(ErrorCode errorCode, TResult result = default)
    {
        return new OperationResult<TResult>
        {
            Success = false,
            ErrorCode = errorCode,
            Result = result
        };
    }
}
