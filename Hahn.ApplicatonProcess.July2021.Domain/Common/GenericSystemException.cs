using System;

namespace Hahn.ApplicatonProcess.July2021.Domain.Common
{
    public class GenericSystemException : Exception
    {
        public ErrorCodes Error { get; }
        public override string Message { get; }
        public string ExciptionMessage { get; }
        public GenericSystemException(ErrorCodes error, string message, string exciptionMessage = "")
        {
            Error = error; Message = message; ExciptionMessage = exciptionMessage;
        }

    }
}
