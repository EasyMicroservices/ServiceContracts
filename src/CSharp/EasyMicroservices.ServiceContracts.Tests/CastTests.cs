using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [Fact]
        public async Task AsyncToContract()
        {
            Task<MessageContract<string>> taskTest = Task.FromResult((MessageContract<string>)"14");

            MessageContract<long> contract = await taskTest.ToContract<string, long>(x => int.Parse(x));
            AssertMessageContract(contract);
            Assert.Equal(contract.Result, 14);

            var toContractResult = await taskTest.ToContract<int>();
            Assert.True(toContractResult.IsSuccess);
            Assert.Equal(toContractResult.Result, 0);

            var result = await taskTest;
            var toContractResult2 = await taskTest.ToContract<string>();
            AssertMessageContract<string>(result, toContractResult2);
        }

        [Fact]
        public async Task AsyncToListContract()
        {
            Task<MessageContract<string>> taskTest = Task.FromResult((MessageContract<string>)"14");

            ListMessageContract<long> contract = await taskTest.ToListContract(x => new List<long>() { int.Parse(x) });
            AssertMessageContract(contract);
            Assert.Equal(contract.Result[0], 14);

            var toContractResult = await taskTest.ToListContract<int>();
            Assert.True(toContractResult.IsSuccess);
            Assert.Equal(toContractResult.Result, null);

            var result = await taskTest;
            var toContractResult2 = await taskTest.ToContract<string>();
            AssertMessageContract<string>(result, toContractResult2);
        }

        [Fact]
        public async Task AsyncListToListContract()
        {
            Task<ListMessageContract<string>> taskTest = Task.FromResult((ListMessageContract<string>)new List<string>() { "14" });
            ListMessageContract<int> contract = await taskTest.ToListContract(x => x.Select(i => int.Parse(i)).ToList());
            AssertMessageContract(contract);
            Assert.Equal(contract.Result[0], 14);

            var toContractResult = await taskTest.ToListContract<int>();
            Assert.True(toContractResult.IsSuccess);
            Assert.Equal(toContractResult.Result, null);

            var result = await taskTest;
            var toContractResult2 = await taskTest.ToListContract<string>();
            AssertMessageContract(result, toContractResult2);
        }
    }
}
