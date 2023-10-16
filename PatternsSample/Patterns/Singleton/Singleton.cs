namespace PatternsSample.Patterns.Singleton;

public class Singleton
{
    private static Singleton? _singleton;

    public static Singleton Instance
    {
        get
        {
            if (_singleton is null)
            {
                _singleton = new Singleton();
            }

            return _singleton;
        }
    }
}