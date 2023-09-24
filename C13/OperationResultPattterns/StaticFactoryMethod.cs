using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace OperationResultPatterns.StaticFactoryMethod
{
    public class Executor
    {
        public OperationResult Operation()
        {
            // Randomize the success indicator
            // This should be real logic
            var randomNumber = Random.Shared.Next(100);
            var success = randomNumber % 2 == 0;

            // Return the operation result
            if (success)
            {
                return OperationResult.Success(randomNumber);
            }
            else
            {
                var error = new OperationResultMessage(
                    $"Something went wrong with the number '{randomNumber}'.",
                    OperationResultSeverity.Error
                );
                return OperationResult.Failure(error);
            }
        }
    }
    public abstract record class OperationResult
    {
        private OperationResult() { }
        public abstract bool Succeeded { get; }
        public static OperationResult Success(int? value = null)
        {
            return new SuccessfulOperationResult { Value = value };
        }
        public static OperationResult Failure(params OperationResultMessage[] errors)
        {
            return new FailedOperationResult(errors);
        }
        private record class SuccessfulOperationResult : OperationResult
        {
            public override bool Succeeded { get; } = true;
            public virtual int? Value { get; init; }
        }
        private record class FailedOperationResult : OperationResult
        {
            public FailedOperationResult(params OperationResultMessage[] errors)
            {
                Messages = errors.ToImmutableList();
            }
            public override bool Succeeded { get; } = false;
            public ImmutableList<OperationResultMessage> Messages { get; }
        }
    }
    public class OperationResultMessage
    {
        public OperationResultMessage(string message, OperationResultSeverity severity)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Severity = severity;
        }

        public string Message { get; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OperationResultSeverity Severity { get; }
    }

    public enum OperationResultSeverity
    {
        Information = 0,
        Warning = 1,
        Error = 2
    }
}
