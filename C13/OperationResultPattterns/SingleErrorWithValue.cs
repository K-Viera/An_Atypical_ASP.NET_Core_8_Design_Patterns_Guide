namespace OperationResultPatterns.SingleErrorWithValue
{
    public class Executor
    {
        public OperationResult Operation()
        {
            var randomNumber = Random.Shared.Next(100);
            var success = randomNumber % 2 == 0;
            return success
            ? new() { Value = randomNumber }
            : new()
            {
                ErrorMessage = $"Something went wrong with the number '{randomNumber}'.",
                Value = randomNumber,
            };
        }
    }
    public record class OperationResult
    {
        public bool Succeeded => string.IsNullOrWhiteSpace(ErrorMessage);
        public string? ErrorMessage { get; init; }
        public int? Value { get; init; }
    }
}
