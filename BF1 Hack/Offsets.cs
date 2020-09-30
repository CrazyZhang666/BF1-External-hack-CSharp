namespace BF1_Hack
{
    public static class Offsets
    {
        private static long PGameContext { get; }
        private static long PGameRenderer { get; }

        static Offsets()
        {
            PGameContext = 0x143472978L;
            PGameRenderer = 0x143662298L;
        }

        public static class GameContext
        {
            public static long GetInstance() => PGameContext;

            public static long PPlayerManager { get; }

            static GameContext()
            {
                PPlayerManager = 0x68L;
            }
        }

        public static class ClientPlayerManager
        {
            public static long PLocalPlayer { get; }
            public static long PClientPlayer { get; }

            static ClientPlayerManager()
            {
                PLocalPlayer = 0x578L;
                PClientPlayer = 0x100L;
            }
        }

        public static class ClientPlayer
        {
            public static long PControlledControllable { get; }

            public static long Name { get; }
            public static long TeamId { get; }
            public static long IsSpectator { get; }


            static ClientPlayer()
            {
                PControlledControllable = 0x1d48L;

                Name = 0x40L;
                TeamId = 0x1c34L;
                IsSpectator = 0x13c9L;
            }
        }

        public struct ClientSoldierPrediction
        {
            public static long PPosition;

            static ClientSoldierPrediction()
            {
                PPosition = 0x40L;
            }
        }

        public static class ClientSoldierEntity
        {
            public static long PHealthComponent { get; }
            public static long PPredictedController { get; }
            public static long PClientSoldierWeapon { get; }

            public static long Yaw { get; }
            public static long IsOccluded { get; }
            public static long PoseType { get; }

            static ClientSoldierEntity()
            {
                PHealthComponent = 0x1c0L;
                PPredictedController = 0x5A8L;
                PClientSoldierWeapon = 0x670L;

                Yaw = 0x5b4L;
                IsOccluded = 0x6ABL;
                PoseType = 0x5F8L;
            }
        }

        public static class HealthComponent
        {
            public static long Health { get; }
            public static long MaxHealth { get; }

            static HealthComponent()
            {
                Health = 0x20;
                MaxHealth = 0x24;
            }
        }

        public static class ClientPredictedController
        {
            public static long Position { get; }

            static ClientPredictedController()
            {
                Position = 0x60;
            }
        }

        public static class ClientSoldierWeapon
        {
            public static long PClientWeapon { get; }

            static ClientSoldierWeapon()
            {
                PClientWeapon = 0x4a18L;
            }
        }

        public static class ClientWeapon
        {
            public static long PWeaponFiringData { get; }

            static ClientWeapon()
            {
                PWeaponFiringData = 0x18L;
            }
        }

        public static class WeaponFiringData
        {
            public static long PGunSwayData { get; }

            static WeaponFiringData()
            {
                PGunSwayData = 0x30L;
            }
        }

        public static class GunSwayData
        {
            public static long Pitch { get; }
            public static long Yaw { get; }

            static GunSwayData()
            {
                Pitch = 0x3D4L;
                Yaw = 0x3c8L;
            }
        }

        //Game render
        public static class GameRenderer
        {
            public static long GetInstance() => PGameRenderer;

            public static long PRenderView { get; }

            static GameRenderer()
            {
                PRenderView = 0x60;
            }
        }

        public static class RenderView
        {
            public static long PViewProj { get; }

            static RenderView()
            {
                PViewProj = 0x460;
            }
        }
    }
}
