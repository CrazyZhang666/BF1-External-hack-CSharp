using SharpDX;

namespace BF1_Hack.GameStruct
{
    public class LocalPlayer
    {
        public string Name { get; set; }
        public int TeamId { get; set; }
        public Vector3 Position { get; set; }
        public float Yaw { get; set; }
        public Matrix ViewProj { get; set; }
    }
}
