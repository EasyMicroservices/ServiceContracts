using System;

namespace EasyMicroservices.ServiceContracts
{
    /// <summary>
    /// Base contract of every services
    /// </summary>
    public class MessageContract
    {
        /// <summary>
        /// The result of service is success or failed
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// When the service result has failed you will see the error here
        /// </summary>
        public ErrorContract Error { get; set; }

        /// <summary>
        /// Get result when you are in generic MessageContract
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual object GetResult()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Convert bool to messagecontract
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator MessageContract(bool result)
        {
            return new MessageContract()
            {
                IsSuccess = result,
                Error = result ? null : new ErrorContract()
                {
                    Message = "No details!"
                }
            };
        }

        /// <summary>
        /// Convert messagecontract to bool
        /// </summary>
        /// <param name="contract"></param>
        public static implicit operator bool(MessageContract contract)
        {
            return contract.IsSuccess;
        }

        /// <summary>
        /// Convert failreason to messagecontract
        /// </summary>
        /// <param name="failedReasonType"></param>
        public static implicit operator MessageContract(FailedReasonType failedReasonType)
        {
            return new MessageContract()
            {
                IsSuccess = false,
                Error = new ErrorContract()
                {
                    Message = failedReasonType.ToString(),
                    FailedReasonType = failedReasonType
                }
            };
        }

        /// <summary>
        /// Convert failreason and message to messagecontract
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator MessageContract((FailedReasonType FailedReasonType, string Message) result)
        {
            return new MessageContract()
            {
                IsSuccess = false,
                Error = new ErrorContract()
                {
                    Message = result.Message,
                    FailedReasonType = result.FailedReasonType
                }
            };
        }

        /// <summary>
        /// Convert MessageContract type
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        public MessageContract<TContract> ToContract<TContract>()
        {
            return new MessageContract<TContract>()
            {
                IsSuccess = IsSuccess,
                Error = Error
            };
        }

        /// <summary>
        /// Convert ListMessageContract type
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        public ListMessageContract<TContract> ToListContract<TContract>()
        {
            return new ListMessageContract<TContract>()
            {
                IsSuccess = IsSuccess,
                Error = Error
            };
        }

        /// <summary>
        /// Convert MessageContract type
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        public MessageContract<TContract> ToContract<TContract>(string customMessage)
        {
            if (Error != null)
                Error.Message = customMessage;
            return new MessageContract<TContract>()
            {
                IsSuccess = IsSuccess,
                Error = Error
            };
        }

        /// <summary>
        /// Convert ListMessageContract type
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        public ListMessageContract<TContract> ToListContract<TContract>(string customMessage)
        {
            if (Error != null)
                Error.Message = customMessage;
            return new ListMessageContract<TContract>()
            {
                IsSuccess = IsSuccess,
                Error = Error
            };
        }

        /// <summary>
        /// Get string result for debugger
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{IsSuccess}\r\n{Error}";
        }
    }
}
