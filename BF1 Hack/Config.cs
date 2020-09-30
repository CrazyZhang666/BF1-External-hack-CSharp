namespace BF1_Hack
{
    public class Config
    {
        public class Credits
        {
            public static string Author { get; }
            public static string Inspiration { get; }

            static Credits()
            {
                Author = "Lorise";
                Inspiration = "";
            }
        }

        public class Hack
        {
            public static string Version { get; }
            public static string Name;

            static Hack()
            {
                Version = "0.3a";
                Name = "Battlefield 1 External Multihack";
            }
        }

        public class Game
        {
            public static string WindowName { get; }
            public static string Process { get; }

            static Game()
            {
                WindowName = "Battlefield™ 1";
                Process = "bf1";
            }
        }
    }
}
