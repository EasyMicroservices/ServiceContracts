using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyMicroservices.ServiceContracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListMessageContract<T> : MessageContract<List<T>>
    {
        /// <summary>
        /// when IsSuccess = true and Result has any items
        /// </summary>
        public bool HasItems
        {
            get
            {
                return IsSuccess && Result?.Count > 0;
            }
        }

        /// <summary>
        /// Convert T to MessageContractList<typeparamref name="T"/>
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ListMessageContract<T>(List<T> result)
        {
            if (result == null)
            {
                return new ListMessageContract<T>()
                {
                    IsSuccess = false,
                    Error = (FailedReasonType.Empty, "You sent null value to MessageContract result!")
                };
            }
            return new ListMessageContract<T>()
            {
                IsSuccess = true,
                Result = result
            };
        }

        /// <summary>
        /// Convert T to MessageContract<typeparamref name="T"/>
        /// </summary>
        /// <param name="values"></param>
        public static implicit operator ListMessageContract<T>((List<T> Result, string EndUserMessage) values)
        {
            var result = (ListMessageContract<T>)values.Result;
            result.Success = values.EndUserMessage;
            return result;
        }

        /// <summary>
        /// Convert MessageContractList type
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        public ListMessageContract<TContract> ToAnotherListContract<TContract>()
        {
            return new ListMessageContract<TContract>()
            {
                IsSuccess = IsSuccess,
                Error = Error.ToChildren(),
                Success = Success
            };
        }

        /// <summary>
        /// Convert MessageContract type
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        public ListMessageContract<TContract> ToAnotherListContract<TContract>(string customMessage)
        {
            if (Error != null)
                Error.Message = customMessage;
            return new ListMessageContract<TContract>()
            {
                IsSuccess = IsSuccess,
                Error = Error.ToChildren(),
                Success = Success
            };
        }

        /// <summary>
        /// Convert failed reason and message to MessageContractList
        /// </summary>
        /// <param name="details"></param>
        public static implicit operator ListMessageContract<T>((FailedReasonType FailedReasonType, string Message) details)
        {
            return new ListMessageContract<T>()
            {
                IsSuccess = false,
                Error = details
            };
        }

        /// <summary>
        /// Convert FailedReasonType to MessageContractList<typeparamref name="T"/>
        /// </summary>
        /// <param name="failedReasonType"></param>
        public static implicit operator ListMessageContract<T>(FailedReasonType failedReasonType)
        {
            return new ListMessageContract<T>()
            {
                IsSuccess = false,
                Error = failedReasonType
            };
        }

        /// <summary>
        /// Convert Exception To MessageContractList<typeparamref name="T"/>
        /// </summary>
        /// <param name="exception"></param>
        public static implicit operator ListMessageContract<T>(Exception exception)
        {
            return new ListMessageContract<T>()
            {
                IsSuccess = false,
                Error = exception
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{IsSuccess} {Result?.Count}\r\n{Error}";
        }
    }
}
