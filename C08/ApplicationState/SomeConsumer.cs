namespace ApplicationState;

public class SomeConsumer
{
    private readonly IApplicationState _myApplicationWideService;
    public SomeConsumer(IApplicationState myApplicationWideService)
    {
        _myApplicationWideService = myApplicationWideService ?? throw new ArgumentNullException(nameof(myApplicationWideService));
    }

    public void Execute()
    {
        if (_myApplicationWideService.Has<string>("some-key"))
        {
            var someValue = _myApplicationWideService.Get<string>("some-key");
            // Do something with someValue 
        }

        // Do something else like: 
        _myApplicationWideService.Set("some-key", "some-value");
        // More logic here 
    }
}
