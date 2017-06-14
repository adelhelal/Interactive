using Microsoft.Kinect;

namespace Interactive.Gestures
{
    public class LeftUp : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandLeft).ScaleY < skeleton.GetJoint(JointType.ElbowLeft).ScaleY;
        }
    }

    public class LeftDown : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandLeft).ScaleY > skeleton.GetJoint(JointType.ElbowLeft).ScaleY;
        }
    }
}