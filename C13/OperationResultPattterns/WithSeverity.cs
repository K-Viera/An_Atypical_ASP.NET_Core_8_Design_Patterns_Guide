using System.Collections.Immutable;

namespace OperationResultPatterns.WithSeverity
{
    public class Executor
    {
        public OperationResult Operation()
        {
            // Randomize the success indicator 
            // This should be real logic 
            var randomNumber = Random.Shared.Next(100);
            var success = randomNumber % 2 == 0;
            // Some information message 
            var information = new OperationResultMessage(
                "This should be very informative!",
                OperationResultSeverity.Information
            );
            // Return the operation result 
            if (success)
            {
                var warning = new OperationResultMessage(
                    "Something went wrong, but we will try again later automatically until it works!",
                    OperationResultSeverity.Warning
                );
                return new OperationResult(information, warning) { Value = randomNumber };
            }
            else
            {
                var error = new OperationResultMessage(
                    $"Something went wrong with the number '{randomNumber}'.",
                    OperationResultSeverity.Error
                );
                return new OperationResult(information, error) { Value = randomNumber };
            }
        }
    }
    public record class OperationResultMessage
    {
        public OperationResultMessage(string message, OperationResultSeverity severity)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Severity = severity;
        }
        public string Message { get; }
        public OperationResultSeverity Severity { get; }
    }

    public enum OperationResultSeverity
    {
        Information = 0,
        Warning = 1,
        Error = 2
    }

    public record class OperationResult
    {
        public OperationResult()
        {
            Messages = ImmutableList<OperationResultMessage>.Empty;
        }
        public OperationResult(params OperationResultMessage[] messages)
        {
            Messages = messages.ToImmutableList();
        }
        public bool Succeeded => !HasErrors();
        public int? Value { get; init; }
        public ImmutableList<OperationResultMessage> Messages { get; init; }
        public bool HasErrors()
        {
            return FindErrors().Any();
        }

        private IEnumerable<OperationResultMessage> FindErrors()
            => Messages.Where(x => x.Severity == OperationResultSeverity.Error);
    }
}
