using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMicroservices.ServiceContracts.Tests
{
    public class CastTests
    {
        [Fact]
        public void MessageContractBaseToMessageContract()
        {
            MessageContract contract = FailedReasonType.InternalError;
            MessageContract<string> converted = contract.ToContract<string>();
            Assert.Equal(contract.Error, converted.Error);
            Assert.Equal(contract.IsSuccess, converted.IsSuccess);
        }

        [Fact]
        public void MessageContractBaseToListMessageContract()
        {
            MessageContract contract = FailedReasonType.InternalError;
            ListMessageContract<string> converted = contract.ToListContract<string>();
            Assert.Equal(contract.Error, converted.Error);
            Assert.Equal(contract.IsSuccess, converted.IsSuccess);
        }

        [Fact]
        public void MessageContractGenericToMessageContract()
        {
            MessageContract<string> contract = FailedReasonType.InternalError;
            MessageContract converted = contract.ToContract();
            Assert.Equal(contract.Error, converted.Error);
            Assert.Equal(contract.IsSuccess, converted.IsSuccess);
        }

        [Fact]
        public void MessageGenericToListMessageContract()
        {
            MessageContract<int> contract = FailedReasonType.InternalError;
            ListMessageContract<string> converted = contract.ToListContract<string>();
            Assert.Equal(contract.Error, converted.Error);
            Assert.Equal(contract.IsSuccess, converted.IsSuccess);
        }


        [Fact]
        public void ListMessageContractToMessageContract()
        {
            ListMessageContract<string> contract = FailedReasonType.InternalError;
            MessageContract converted = contract.ToContract();
            Assert.Equal(contract.Error, converted.Error);
            Assert.Equal(contract.IsSuccess, converted.IsSuccess);
        }

        [Fact]
        public void ListMessageContractToGenericMessageContract()
        {
            ListMessageContract<int> contract = FailedReasonType.InternalError;
            MessageContract converted = contract.ToContract();
            Assert.Equal(contract.Error, converted.Error);
            Assert.Equal(contract.IsSuccess, converted.IsSuccess);
        }
    }
}
