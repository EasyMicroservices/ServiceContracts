using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMicroservices.ServiceContracts.Tests
{
    public class CastTests : BaseTests
    {
        [Fact]
        public void MessageContractBaseToMessageContract()
        {
            MessageContract contract = FailedReasonType.InternalError;
            MessageContract<string> converted = contract.ToContract<string>();
            AssertMessageContract(contract, converted);
        }

        [Fact]
        public void MessageContractBaseToListMessageContract()
        {
            MessageContract contract = FailedReasonType.InternalError;
            ListMessageContract<string> converted = contract.ToListContract<string>();
            AssertMessageContract(contract, converted);
        }

        [Fact]
        public void MessageContractGenericToMessageContract()
        {
            MessageContract<string> contract = FailedReasonType.InternalError;
            MessageContract converted = contract.ToContract();
            AssertMessageContract(contract, converted);
        }

        [Fact]
        public void MessageGenericToListMessageContract()
        {
            MessageContract<int> contract = FailedReasonType.InternalError;
            ListMessageContract<string> converted = contract.ToListContract<string>();
            AssertMessageContract(contract, converted);
        }

        [Fact]
        public void ListMessageContractToMessageContract()
        {
            ListMessageContract<string> contract = FailedReasonType.InternalError;
            MessageContract converted = contract.ToContract();
            AssertMessageContract(contract, converted);
        }

        [Fact]
        public void ListMessageContractToGenericMessageContract()
        {
            ListMessageContract<int> contract = FailedReasonType.InternalError;
            MessageContract converted = contract.ToContract();
            AssertMessageContract(contract, converted);
        }
    }
}
