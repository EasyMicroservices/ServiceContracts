namespace ServiceContracts.Tests
{
    public class FailedReasonTest
    {
        [Fact]
        public MessageContract CheckFailedReasonToMessageContract()
        {
            return FailedReasonType.InternalError;
        }

        [Fact]
        public MessageContract CheckFailedReasonWithMessageToMessageContract()
        {
            return (FailedReasonType.InternalError, "Server has internal error");
        }
    }
}