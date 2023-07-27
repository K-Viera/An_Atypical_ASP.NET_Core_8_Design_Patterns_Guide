namespace Strategy
{
    public interface ISortStrategy
    {
        IOrderedEnumerable<string> Sort(IEnumerable<string> input);
    }

    public class SortAscendingStrategy : ISortStrategy
    {
        public IOrderedEnumerable<string> Sort(IEnumerable<string> input)
            => input.OrderBy(x => x);
    }

    public class SortDescendingStrategy : ISortStrategy
    {
        public IOrderedEnumerable<string> Sort(IEnumerable<string> input)
            => input.OrderByDescending(x => x);
    }

}
