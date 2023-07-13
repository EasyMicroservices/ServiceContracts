namespace EasyMicroservices.ServiceContracts
{
    /// <summary>
    /// data of validation means
    /// </summary>
    public class ValidationContract
    {
        /// <summary>
        /// message of validation
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// details for debugger
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Message;
        }
    }
}
