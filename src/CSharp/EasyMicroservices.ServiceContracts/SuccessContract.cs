namespace EasyMicroservices.ServiceContracts
{
    /// <summary>
    /// 
    /// </summary>
    public class SuccessContract
    {
        /// <summary>
        /// 
        /// </summary>
        public string EndUserMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static implicit operator SuccessContract(string message)
        {
            return new SuccessContract()
            {
                EndUserMessage = message
            };
        }
    }
}
