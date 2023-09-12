using EasyMicroservices.ServiceContracts.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EasyMicroservices.ServiceContracts
{
    /// <summary>
    /// 
    /// </summary>
    public static class MessageContractExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public static async Task<T> AsResult<T>(this Task<MessageContract<T>> task)
        {
            var result = await task;
            return result.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public static async Task<T> AsCheckedResult<T>(this Task<MessageContract<T>> task)
        {
            var result = await task;
            return result.GetCheckedResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="contract"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<MessageContract<TTo>> ToContract<TFrom, TTo>(this Task<MessageContract<TFrom>> contract, Func<TFrom, TTo> func)
        {
            var result = await contract;
            return result.ToContract(func);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="contract"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<ListMessageContract<TTo>> ToListContract<TFrom, TTo>(this Task<MessageContract<TFrom>> contract, Func<TFrom, List<TTo>> func)
        {
            var result = await contract;
            return result.ToListContract(func);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="contract"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<ListMessageContract<TTo>> ToListContract<TFrom, TTo>(this Task<ListMessageContract<TFrom>> contract, Func<List<TFrom>, List<TTo>> func)
        {
            var result = await contract;
            return result.ToAnotherListContract(func);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageContract"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryGetResult<T>(this MessageContract<T> messageContract, out T result)
        {
            if (messageContract)
            {
                result = messageContract.Result;
                return true;
            }
            result = default;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="messageContract"></param>
        /// <param name="newResult"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryGetResult<T, TContract>(this MessageContract<T> messageContract, out T result, out MessageContract<TContract> newResult)
        {
            if (messageContract)
            {
                newResult = default;
                result = messageContract.Result;
                return true;
            }
            newResult = messageContract.ToContract<TContract>();
            result = default;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageContract"></param>
        /// <param name="result"></param>
        /// <returns>true if success and has items</returns>
        public static bool TryGetResult<T>(this ListMessageContract<T> messageContract, out List<T> result)
        {
            if (messageContract.HasItems)
            {
                result = messageContract.Result;
                return true;
            }
            result = default;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="messageContract"></param>
        /// <param name="newResult"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryGetResult<T, TContract>(this ListMessageContract<T> messageContract, out List<T> result, out ListMessageContract<TContract> newResult)
        {
            if (messageContract)
            {
                newResult = default;
                result = messageContract.Result;
                return true;
            }
            newResult = messageContract.ToAnotherListContract<TContract>();
            result = default;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static MessageContract<T> ToContract<T>(this object result)
        {
            return Normalize((MessageContract<T>)Map(typeof(MessageContract<T>), result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static MessageContract ToContract(this object result)
        {
            return Normalize((MessageContract)Map(typeof(MessageContract), result));
        }

        static T Normalize<T>(T messageContract)
            where T : MessageContract
        {
            if (!messageContract)
                messageContract.Error = messageContract.Error.ToChildren();
            return messageContract;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ListMessageContract<T> ToListContract<T>(this object result)
        {
            return Normalize((ListMessageContract<T>)Map(typeof(ListMessageContract<T>), result));
        }

        static object Map(Type type, object obj)
        {
            if (obj is null)
                return null;
            if (!obj.GetType().GetTypeInfo().IsClass || obj is string)
            {
                return obj;
            }
            var objectType = obj.GetType();
            var instance = Activator.CreateInstance(type);
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var objectProperty = objectType.GetProperty(property.Name, BindingFlags.Public | BindingFlags.Instance);
                if (objectProperty != null && property.CanRead && property.CanWrite && objectProperty.CanRead && objectProperty.CanWrite)
                {
                    var value = objectProperty.GetValue(obj);
                    if (value != null)
                    {
                        if (value.GetType().GetTypeInfo().IsClass && value is not string)
                        {
                            if (value is IEnumerable objects)
                            {
                                var itemsInstance = Activator.CreateInstance(value.GetType());
                                foreach (var item in objects)
                                {
                                    if (itemsInstance is IList list)
                                    {
                                        list.Add(item == null ? null : Map(item.GetType(), item));
                                    }
                                }
                                property.SetValue(instance, itemsInstance);
                            }
                            else
                            {
                                value = Map(property.PropertyType, value);
                                if (value != null)
                                    property.SetValue(instance, value);
                            }
                        }
                        else if (property.PropertyType == objectProperty.PropertyType)
                        {
                            property.SetValue(instance, value);
                        }
                    }
                }
            }
            return instance;
        }

        internal static List<string> ToListStackTrace(this string stackTrace)
        {
            return stackTrace == null ? null : stackTrace.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /// <summary>
        /// Get a checked and valid result of messagecontract
        /// if IsSuccess is false, it will throw an exception
        /// </summary>
        /// <exception cref="InvalidResultOfMessageContractException"></exception>
        /// <returns></returns>
        public static T GetCheckedResult<T>(this object result)
        {
            var contrract = ToContract<T>(result);
            return contrract.GetCheckedResult();
        }

        /// <summary>
        /// throw an exception when message contract is not successed
        /// </summary>
        /// <exception cref="InvalidResultOfMessageContractException"></exception>
        /// <returns></returns>
        public static void ThrowsIfFails(this object result)
        {
            var contrract = ToContract(result);
            contrract.ThrowsIfFails();
        }
    }
}
