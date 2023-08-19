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
                    Error = new ErrorContract()
                    {
                        FailedReasonType = FailedReasonType.NotFound,
                        StackTrace = Environment.StackTrace,
                        Message = "یافت نشد."
                    }
                };
            }
            return new MessageContract<T>()
            {
                IsSuccess = true,
                Result = contract
            };
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
                Error = Error
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
                Error = new ErrorContract()
                {
                    FailedReasonType = details.FailedReasonType,
                    StackTrace = Environment.StackTrace,
                    Message = details.Message
                }
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
                Error = new ErrorContract()
                {
                    FailedReasonType = failedReasonType,
                    StackTrace = Environment.StackTrace,
                    Message = failedReasonType.ToString()
                }
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
                Error = new ErrorContract()
                {
                    FailedReasonType = FailedReasonType.InternalError,
                    StackTrace = Environment.StackTrace,
                    Message = exception.Message,
                    Details = exception.ToString()
                }
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