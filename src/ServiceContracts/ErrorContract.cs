using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    /// <summary>
    /// The contract of Error when the service response has error
    /// </summary>
    public class ErrorContract
    {
        /// <summary>
        /// Validation errors result
        /// </summary>
        public List<ValidationContract> Validations { get; set; } = new List<ValidationContract>();
        /// <summary>
        /// Type of error
        /// </summary>
        public FailedReasonType FailedReasonType { get; set; }
        /// <summary>
        /// Simple message of error
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Detailed message of error 
        /// </summary>
        public string Details { get; set; }
        /// <summary>
        /// Stack trance for debugging
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Get fast result for debugger
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{FailedReasonType}\r\n{Message}\r\n${Details}${StackTrace}$";
        }
    }
}
