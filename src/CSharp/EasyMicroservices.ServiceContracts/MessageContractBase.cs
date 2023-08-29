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
        /// When the service result has success you can use the success stuff here
        /// </summary>
        public SuccessContract Success { get; set; }

        /// <summary>
        /// Get result when you are in generic MessageContract
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual object GetResult()
        {
            throw new NotImplementedException("In the simple MesageContract there is no result to get! you must get result form MessageContract<T>!");
        }

        /// <summary>
        /// Convert bool to messagecontract
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator MessageContract(bool result)
        {
            if (!result)
                throw new Exception("Do not send false to MessageContract directly, please use FailedReasonType enum!");
            return new MessageContract()
            {
                IsSuccess = result
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
                Error = failedReasonType
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
                Error = result
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
                Error = Error.ToChildren(),
                Success = Success,
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
                Error = Error.ToChildren(),
                Success = Success,
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
                Error = Error.ToChildren(),
                Success = Success,
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
                Error = Error.ToChildren(),
                Success = Success,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public static implicit operator MessageContract(Exception exception)
        {
            return new MessageContract()
            {
                IsSuccess = false,
                Error = exception
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
