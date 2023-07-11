using System.Collections.Generic;
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
        public static async Task<T> AsResultThrowsIfFailed<T>(this Task<MessageContract<T>> task)
        {
            var result = await task;
            return result.ThorwsIfFailed();
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
        /// <param name="messageContract"></param>
        /// <param name="result"></param>
        /// <returns>true if success and has items</returns>
        public static bool TryGetResult<T>(this MessageContractList<T> messageContract, out List<T> result)
        {
            if (messageContract.HasItems)
            {
                result = messageContract.Result;
                return true;
            }
            result = default;
            return false;
        }
    }
}
