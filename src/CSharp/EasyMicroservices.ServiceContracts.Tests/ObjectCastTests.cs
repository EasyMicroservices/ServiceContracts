namespace EasyMicroservices.ServiceContracts.Tests
{
    public class ObjectCastTests: BaseTests
    {
        [Fact]
        public void MessageContractBaseToMessageContract()
        {
            object contract = (MessageContract)FailedReasonType.InternalError;
            MessageContract<string> converted = contract.ToContract<string>();
            AssertMessageContract(contract, converted);
        }

        [Fact]
        public void MessageContractBaseToListMessageContract()
        {
            object contract = (MessageContract)FailedReasonType.InternalError;
            ListMessageContract<string> converted = contract.ToListContract<string>();
            AssertMessageContract(contract, converted);
        }

        [Fact]
        public void MessageContractGenericToMessageContract()
        {
            object contract = (MessageContract<string>)FailedReasonType.InternalError;
            MessageContract converted = contract.ToContract();
            AssertMessageContract(contract, converted);
        }

        [Fact]
        public void MessageGenericToListMessageContract()
        {
            object contract = (MessageContract<int>)FailedReasonType.InternalError;
            ListMessageContract<string> converted = contract.ToListContract<string>();
            AssertMessageContract(contract, converted);
        }


        [Fact]
        public void ListMessageContractToMessageContract()
        {
            object contract = (ListMessageContract<string>)FailedReasonType.InternalError;
            MessageContract converted = contract.ToContract();
            AssertMessageContract(contract, converted);
        }

        [Fact]
        public void ListMessageContractToGenericMessageContract()
        {
            object contract = (ListMessageContract<int>)FailedReasonType.InternalError;
            MessageContract converted = contract.ToContract();
            AssertMessageContract(contract, converted);
        }
    }
}