using SignalGo.Shared.DataTypes;

namespace ServiceContracts
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

        /// <summary>
        /// cast base validation to validation contract
        /// </summary>
        /// <param name="baseValidationRuleInfo"></param>
        public static implicit operator ValidationContract(BaseValidationRuleInfoAttribute baseValidationRuleInfo)
        {
            return (ValidationContract)BaseValidationRuleInfoAttribute.GetErrorValue(baseValidationRuleInfo);
        }
    }
}
