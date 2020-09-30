using GameHackFramework.Code.Struct;
using SharpDX;

namespace BF1_Hack.GameStruct
{
    public class Player
    {
        public enum Pose { Standing, Seated, Lying }

        public string Name { get; set; } = string.Empty;
        public int TeamId { get; set; }
        public bool IsSpectator { get; set; }
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public Vector3 Position { get; set; }
        public float Yaw { get; set; }
        public bool IsVisible { get; set; }
        public Pose PoseType { get; set; }

        public BoundingBox3D BoundingBox()
        {
            BoundingBox3D boundingBox = new BoundingBox3D();

            switch( PoseType )
            {
                case Pose.Standing:
                    boundingBox.Min = new Vector3( -0.35F, 0, -0.35F );
                    boundingBox.Max = new Vector3( 0.35F, 1.7F, 0.35F );
                    break;

                case Pose.Seated:
                    boundingBox.Min = new Vector3( -0.4F, 0, -0.4F );
                    boundingBox.Max = new Vector3( 0.4F, 1F, 0.4F );
                    break;

                case Pose.Lying:
                    boundingBox.Min = new Vector3( -1.5F, 0, -0.35F );
                    boundingBox.Max = new Vector3( 0.2F, 0.55F, 0.35F );
                    break;
            }


            return boundingBox;
        }

        public int Height => (int) (BoundingBox().Max.Y - BoundingBox().Min.Y);

        public bool IsValid() => ( Health > 0.1f ) && ( Health <= 100f ) && !Position.IsZero;
    }
}
