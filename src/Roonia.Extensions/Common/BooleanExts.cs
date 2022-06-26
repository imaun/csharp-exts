namespace Roonia.Extensions
{
    public static class BooleanExts
    {
        
        public static bool WhenTrue(this bool condition, Action action) {
            if(condition)
                action();

            return condition;
        }

        public static bool Else(this bool condition, Action action) {
            if(!condition)
                action();
            
            return condition;
        }
    }
}