namespace qwik.helpers
{
    public static class Random
    {
        private static readonly global::System.Random RandomGenerator = new global::System.Random();
        
        public static int Rand(int min, int max)
        {
            return RandomGenerator.Next(min, max);
        }

        public static int Rand(int max)
        {
            return RandomGenerator.Next(max);
        }

        public static int Rand()
        {
            return RandomGenerator.Next();
        }
    }
}