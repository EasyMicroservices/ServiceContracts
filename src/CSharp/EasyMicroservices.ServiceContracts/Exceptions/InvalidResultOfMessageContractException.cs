using System;

namespace EasyMicroservices.ServiceContracts.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class InvalidResultOfMessageContractException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageContract"></param>
        public InvalidResultOfMessageContractException(MessageContract messageContract)
            : base($"The MessageContract is not success, Summary: {messageContract.Error.Message}")
        {
            MessageContract = messageContract;
        }

        /// <summary>
        /// 
        /// </summary>
        public MessageContract MessageContract { get; }
    }
}
