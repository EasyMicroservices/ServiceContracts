using System.Collections.Generic;

namespace EasyMicroservices.ServiceContracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageContractList<T> : MessageContract<List<T>>
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
    }
}
