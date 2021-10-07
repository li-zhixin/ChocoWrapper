using System;

namespace ChocoWrapper
{
    public enum ChocoWrapperExceptionType
    {
        Process
    }

    public class ChocoWrapperException : Exception
    {
        public ChocoWrapperException(ChocoWrapperExceptionType type, string message, Exception? innerException = null, string errorOutput = "")
            : base(message, innerException)
        {
            ErrorOutput = errorOutput;
            Type = type;
        }
        public ChocoWrapperException(ChocoWrapperExceptionType type, string message, string errorOutput = "")
            : base(message)
        {
            ErrorOutput = errorOutput;
            Type = type;
        }
        public ChocoWrapperException(ChocoWrapperExceptionType type, string message)
            : base(message)
        {
            ErrorOutput = string.Empty;
            Type = type;
        }
        
        public ChocoWrapperExceptionType Type { get; }
        
        public string ErrorOutput { get; }
    }
}