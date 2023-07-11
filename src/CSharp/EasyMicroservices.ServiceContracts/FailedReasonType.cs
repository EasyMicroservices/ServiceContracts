namespace EasyMicroservices.ServiceContracts
{
    /// <summary>
    /// Reason type when service has response erros
    /// </summary>
    public enum FailedReasonType : int
    {
        /// <summary>
        /// value is none, Never use the None to return values
        /// </summary>
        None = 0,
        /// <summary>
        /// error value is default
        /// </summary>
        Default = 1,
        /// <summary>
        /// for the filter values from web admin panel you can sent all for types
        /// </summary>
        All = 2,
        /// <summary>
        /// there is other error that is not in the types
        /// </summary>
        Other = 3,
        /// <summary>
        /// the error type is uknown to us
        /// </summary>
        Unknown = 4,
        /// <summary>
        /// there is nothing to show or validate error
        /// </summary>
        Nothing = 5,
        /// <summary>
        /// User is not init and has not sent or generated session
        /// </summary>
        SessionAccessDenied = 6,
        /// <summary>
        /// User has no permissions for the service
        /// </summary>
        AccessDenied = 7,
        /// <summary>
        /// Internal exception or erros happen
        /// </summary>
        InternalError = 8,
        /// <summary>
        /// Dupplicate request happen
        /// </summary>
        Dupplicate = 9,
        /// <summary>
        /// Data is empty or null
        /// </summary>
        Empty = 10,
        /// <summary>
        /// Logic or result not found for this request
        /// </summary>
        NotFound = 11,
        /// <summary>
        /// There is validation error for request
        /// </summary>
        ValidationsError = 12,
        /// <summary>
        /// Stream has error or corrupted
        /// </summary>
        StreamError = 13,
        /// <summary>
        /// When the external webservice has an error
        /// </summary>
        WebServiceNotWorking = 14,
    }
}
