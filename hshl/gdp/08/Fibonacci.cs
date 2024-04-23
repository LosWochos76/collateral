public class Fibonacci
{
    private static int fn_2 = 1;
    private static int fn_1 = 1;

    public static int Next()
    {
        int fn = fn_2 + fn_1;
        fn_2 = fn_1;
        fn_1 = fn;
        return fn;
    }

    public static void Reset()
    {
        fn_1 = 1;
        fn_2 = 1;
    }
}