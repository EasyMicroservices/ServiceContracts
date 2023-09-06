using EasyMicroservices.ServiceContracts.Exceptions;
using System;
using System.Collections.Generic;

namespace EasyMicroservices.ServiceContracts
{
    /// <summary>
    /// MessageContract with result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageContract<T> : MessageContract
    {
        /// <summary>
        /// Result of service when it's successfuly
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Get result of messagecontract from none generic MessageContract
        /// </summary>
        /// <returns></returns>
        public override object GetResult()
        {
            return Result;
        }

        /// <summary>
        /// Get a checked and valid result of messagecontract
        /// if IsSuccess is false, it will throw an exception
        /// </summary>
        /// <exception cref="InvalidResultOfMessageContractException"></exception>
        /// <returns></returns>
        public T GetCheckedResult()
        {
            if (!IsSuccess)
                throw new InvalidResultOfMessageContractException(this);
            return Result;
        }

        /// <summary>
        /// When the messagecontract has result
        /// </summary>
        /// <returns></returns>
        public bool HasResult()
        {
            return IsSuccess && !EqualityComparer<T>.Default.Equals(Result, default);
        }

        /// <summary>
        /// Convert MessageContract<typeparamref name="T"/> to bool
        /// </summary>
        /// <param name="contract"></param>
        public static implicit operator bool(MessageContract<T> contract)
        {
            return contract.IsSuccess;
        }
        /// <summary>
        /// Convert T to MessageContract<typeparamref name="T"/>
        /// </summary>
        /// <param name="contract"></param>
        public static implicit operator MessageContract<T>(T contract)
        {
            if (contract == null)
            {
                return new MessageContract<T>()
                {
                    IsSuccess = false,
                    Error = (FailedReasonType.Empty, "You sent null value to MessageContract result!")
                };
            }
            return new MessageContract<T>()
            {
                IsSuccess = true,
                Result = contract
            };
        }

        /// <summary>
        /// Convert T to MessageContract<typeparamref name="T"/>
        /// </summary>
        /// <param name="values"></param>
        public static implicit operator MessageContract<T>((T Contract, string EndUserMessage) values)
        {
            var result = (MessageContract<T>)values.Contract;
            result.Success = values.EndUserMessage;
            return result;
        }

        /// <summary>
        /// Convert MessageContract type
        /// </summary>
        /// <returns></returns>
        public MessageContract<T> ToContract()
        {
            return new MessageContract<T>()
            {
                IsSuccess = IsSuccess,
                Error = Error.ToChildren(),
                Success = Success
            };
        }

        /// <summary>
        /// Convert failed reason and message to MessageContract
        /// </summary>
        /// <param name="details"></param>
        public static implicit operator MessageContract<T>((FailedReasonType FailedReasonType, string Message) details)
        {
            return new MessageContract<T>()
            {
                IsSuccess = false,
                Error = details
            };
        }

        /// <summary>
        /// Convert FailedReasonType to MessageContract<typeparamref name="T"/>
        /// </summary>
        /// <param name="failedReasonType"></param>
        public static implicit operator MessageContract<T>(FailedReasonType failedReasonType)
        {
            return new MessageContract<T>()
            {
                IsSuccess = false,
                Error = failedReasonType
            };
        }

        /// <summary>
        /// Convert Exception To MessageContract<typeparamref name="T"/>
        /// </summary>
        /// <param name="exception"></param>
        public static implicit operator MessageContract<T>(Exception exception)
        {
            return new MessageContract<T>()
            {
                IsSuccess = false,
                Error = exception
            };
        }

        /// <summary>
        /// Convert to string for debugging
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// Thorw an excpetion if the result is not success
        /// </summary>
        /// <exception cref="Exception"></exception>
        public virtual T ThorwsIfFailed()
        {
            if (!IsSuccess)
                throw new Exception(ToString());
            return Result;
        }
    }
}