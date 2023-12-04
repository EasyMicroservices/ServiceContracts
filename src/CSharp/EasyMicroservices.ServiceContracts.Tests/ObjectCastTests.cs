using System.Collections.Generic;
using WhiteLables.GeneratedServices;

namespace EasyMicroservices.ServiceContracts.Tests
{
    public class ObjectCastTests : BaseTests
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
        public void SuccessMessageContractGenericToMessageContract()
        {
            object contract = (MessageContract<string>)"Hello World";
            MessageContract converted = contract.ToContract();
            AssertMessageContract(contract, converted);
        }

        [Fact]
        public void SuccessMessageContractGenericToMessageContractGeneric()
        {
            object contract = (MessageContract<string>)"Hello World";
            MessageContract<string> converted = contract.ToContract<string>();
            AssertMessageContract<string>(contract as MessageContract<string>, converted);
        }

        [Fact]
        public void SuccessMessageContractGenericToMessageContractGenericEndUserMessage()
        {
            object contract = (MessageContract<string>)("Hello World", "End user text");
            MessageContract<string> converted = contract.ToContract<string>();
            AssertMessageContract<string>(contract as MessageContract<string>, converted);
        }

        [Fact]
        public void MessageGenericToListMessageContract()
        {
            object contract = (MessageContract<int>)FailedReasonType.InternalError;
            ListMessageContract<string> converted = contract.ToListContract<string>();
            AssertMessageContract(contract, converted);
        }

        [Fact]
        public void SuccessMessageGenericToListMessageContract()
        {
            object contract = (MessageContract<int>)157;
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


        [Fact]
        public void MessageContractBaseToListMessageContractMultiple()
        {
            var whiteLabelClient = new WhiteLabelContractListMessageContract()
            {
                IsSuccess = false,
                Error = new WhiteLables.GeneratedServices.ErrorContract()
                {
                    Children = new List<WhiteLables.GeneratedServices.ErrorContract>()
                       {
                           new WhiteLables.GeneratedServices.ErrorContract()
                           {
                                Message = "t1"
                           },
                           new WhiteLables.GeneratedServices.ErrorContract()
                           {
                                Message = "t2"
                           },
                           new WhiteLables.GeneratedServices.ErrorContract()
                           {
                                Message = "t3"
                           }
                       },
                    EndUserMessage = "t0",
                    FailedReasonType = WhiteLables.GeneratedServices.FailedReasonType.AccessDenied
                }
            };
            ListMessageContract<string> converted = whiteLabelClient.ToListContract<string>();
            Assert.Equal(whiteLabelClient.Error.Children.Count, converted.Error.Children.Count -1);
        }

        //[Fact]
        //public void MessageContractGenericToMessageContract()
        //{
        //    object contract = (MessageContract<string>)FailedReasonType.InternalError;
        //    MessageContract converted = contract.ToContract();
        //    AssertMessageContract(contract, converted);
        //}

    }
}