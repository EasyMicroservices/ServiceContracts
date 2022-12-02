using System;
using System.Collections.Generic;

namespace ServiceContracts
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
        /// convert text to messagecontarct
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator MessageContract(string result)
        {
            return new MessageContract()
            {
                IsSuccess = false,
                Error = new ErrorContract()
                {
                    Message = result
                }
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
        /// Convert Message and Details to MessageContract<typeparamref name="T"/>
        /// </summary>
        /// <param name="data"></param>
        public static implicit operator MessageContract<T>((string Message, string Details) data)
        {
            return new MessageContract<T>()
            {
                IsSuccess = false,
                Error = new ErrorContract()
                {
                    Message = data.Message,
                    Details = data.Details
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
    }
}