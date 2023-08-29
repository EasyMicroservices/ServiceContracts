using System;
using System.Collections.Generic;

namespace EasyMicroservices.ServiceContracts.Tests
{
    public class BaseTests
    {
        public void AssertMessageContract(object messageContract1, object messageContract2)
        {
            AssertMessageContract((MessageContract)messageContract1, (MessageContract)messageContract2);
        }

        public void AssertMessageContract(MessageContract messageContract1, MessageContract messageContract2)
        {
            AssertMessageContract(messageContract1);
            AssertMessageContract(messageContract2);
            Assert.Equal(messageContract1.IsSuccess, messageContract2.IsSuccess);
            AssertSuccessContract(messageContract1.Success, messageContract2.Success);
            AssertErrorContract(messageContract1.Error, messageContract2.Error);
        }

        public void AssertMessageContract(MessageContract messageContract)
        {
            if (!messageContract)
            {
                Assert.NotNull(messageContract.Error.StackTrace);
                Assert.NotEmpty(messageContract.Error.StackTrace);
            }
        }

        public void AssertMessageContract<T>(MessageContract<T> messageContract)
        {
            AssertMessageContract(messageContract as MessageContract);
            if (messageContract)
            {
                Assert.NotNull(messageContract.Result);
            }
        }

        public void AssertMessageContract<T>(MessageContract<T> messageContract1, MessageContract<T> messageContract2)
        {
            AssertMessageContract(messageContract1);
            AssertMessageContract(messageContract2);
            Assert.Equal(messageContract1.IsSuccess, messageContract2.IsSuccess);
            AssertSuccessContract(messageContract1.Success, messageContract2.Success);
            AssertErrorContract(messageContract1.Error, messageContract2.Error);
            Assert.Equal(messageContract1.Result, messageContract2.Result);
        }

        public void AssertMessageContract<T, T2>(MessageContract<T> messageContract1, MessageContract<T2> messageContract2)
        {
            AssertMessageContract(messageContract1);
            AssertMessageContract(messageContract2);
            Assert.Equal(messageContract1.IsSuccess, messageContract2.IsSuccess);
            AssertSuccessContract(messageContract1.Success, messageContract2.Success);
            AssertErrorContract(messageContract1.Error, messageContract2.Error);
        }

        public void AssertMessageContract<T, T2>(MessageContract<T> messageContract1, ListMessageContract<T2> messageContract2)
        {
            AssertMessageContract(messageContract1);
            AssertMessageContract(messageContract2);
            Assert.Equal(messageContract1.IsSuccess, messageContract2.IsSuccess);
            AssertSuccessContract(messageContract1.Success, messageContract2.Success);
            AssertErrorContract(messageContract1.Error, messageContract2.Error);
        }

        void AssertSuccessContract(SuccessContract contract1, SuccessContract contract2)
        {
            if (contract1 == null || contract2 == null)
                Assert.Equal(contract1, contract2);
            else
                Assert.Equal(contract1.EndUserMessage, contract2.EndUserMessage);
        }

        void AssertErrorContract(ErrorContract contract1, ErrorContract contract2)
        {
            if (contract1 == null || contract2 == null)
                Assert.Equal(contract1, contract2);
            else
            {
                Assert.Equal(contract1.EndUserMessage, contract2.EndUserMessage);
                Assert.Equal(string.Join(Environment.NewLine, contract1.StackTrace), string.Join(Environment.NewLine, contract2.StackTrace));
                Assert.Equal(contract1.Message, contract2.Message);
                Assert.Equal(contract1.Details, contract2.Details);
                Assert.Equal(contract1.FailedReasonType, contract2.FailedReasonType);
                AssertValidationContract(contract1.Validations, contract2.Validations);
                AssertErrorChildrenContract(contract1.Children, contract2.Children);
            }
        }

        void AssertValidationContract(List<ValidationContract> contract1, List<ValidationContract> contract2)
        {
            if (contract1 == null || contract2 == null)
                Assert.Equal(contract1, contract2);
            else
            {
                Assert.Equal(contract1.Count, contract2.Count);
                for (int i = 0; i < contract1.Count; i++)
                {
                    AssertValidationContract(contract1[i], contract2[i]);
                }
            }
        }

        void AssertValidationContract(ValidationContract contract1, ValidationContract contract2)
        {
            if (contract1 == null || contract2 == null)
                Assert.Equal(contract1, contract2);
            else
                Assert.Equal(contract1.Message, contract2.Message);
        }

        void AssertErrorChildrenContract(List<ErrorContract> contract1, List<ErrorContract> contract2)
        {
            if (contract1 != null && contract2 != null)
            {
                Assert.Equal(contract1.Count, contract2.Count);
                for (int i = 0; i < contract1.Count; i++)
                {
                    AssertErrorContract(contract1[i], contract2[i]);
                }
            }
        }
    }
}
