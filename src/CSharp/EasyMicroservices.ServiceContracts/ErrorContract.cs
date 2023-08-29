using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyMicroservices.ServiceContracts
{
    /// <summary>
    /// The contract of Error when the service response has error
    /// </summary>
    public class ErrorContract
    {
        /// <summary>
        /// Validation errors result
        /// </summary>
        public List<ValidationContract> Validations { get; set; } = new List<ValidationContract>();
        /// <summary>
        /// Type of error
        /// </summary>
        public FailedReasonType FailedReasonType { get; set; }
        /// <summary>
        /// Simple message of error
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EndUserMessage { get; set; }
        /// <summary>
        /// Detailed message of error 
        /// </summary>
        public string Details { get; set; }
        /// <summary>
        /// Stack trance for debugging
        /// </summary>
        public List<string> StackTrace { get; set; }
        /// <summary>
        /// All of the inner errors from microservice tree
        /// </summary>
        public List<ErrorContract> Children { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ServiceDetailsContract ServiceDetails { get; set; }
        /// <summary>
        /// Get fast result for debugger
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Type: {FailedReasonType}\r\nMeesage: {Message}\r\n$Details: {Details}$StackTrace:\r\n{string.Join(Environment.NewLine, StackTrace)}$\r\n{GetChildrenTrace()}";
        }

        string GetChildrenTrace()
        {
            if (Children == null)
                return "";
            return $"Chilren:\r\n{string.Join("\r\n", Children.Select(x => x.ToString()))}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ErrorContract ToChildren()
        {
            if (this.Children == null)
                this.Children = new List<ErrorContract>();

            this.Children.Add(new ErrorContract()
            {
                FailedReasonType = FailedReasonType,
                StackTrace = Environment.StackTrace.ToListStackTrace()
            });
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="failedReasonType"></param>
        public static implicit operator ErrorContract(FailedReasonType failedReasonType)
        {
            return new ErrorContract()
            {
                FailedReasonType = failedReasonType,
                StackTrace = Environment.StackTrace.ToListStackTrace(),
                Message = failedReasonType.ToString()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public static implicit operator ErrorContract(Exception exception)
        {
            return new ErrorContract()
            {
                FailedReasonType = FailedReasonType.InternalError,
                StackTrace = Environment.StackTrace.ToListStackTrace(),
                Message = exception.Message,
                Details = exception.ToString()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="details"></param>
        public static implicit operator ErrorContract((FailedReasonType FailedReasonType, string Message) details)
        {
            return new ErrorContract()
            {
                FailedReasonType = details.FailedReasonType,
                StackTrace = Environment.StackTrace.ToListStackTrace(),
                Message = details.Message,
            };
        }
    }
}
