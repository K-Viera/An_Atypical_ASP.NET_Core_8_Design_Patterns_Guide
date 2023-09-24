namespace OperationResultPatterns.SingleError
{
    public class Executor
    {
        public OperationResult Operation() {
            var randomNumber = Random.Shared.Next(100);
            var success = randomNumber % 2 == 0;
            return success
            ? new()
            : new() { ErrorMessage = $"Something went wrong with the number '{randomNumber}'." };
        }
    }
    public record class OperationResult
    {
        public bool Succeeded => string.IsNullOrWhiteSpace(ErrorMessage);
        public string? ErrorMessage { get; init; }
    }
}
