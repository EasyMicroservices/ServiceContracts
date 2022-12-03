namespace ServiceContracts.Tests
{
    public class FailedReasonTest
    {
        [Fact]
        public MessageContract CheckFailedReasonToMessageContract()
        {
            return FailedReasonType.InternalError;
        }
    }
}