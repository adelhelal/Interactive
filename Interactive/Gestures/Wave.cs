
using Microsoft.Kinect;

namespace Interactive.Gestures
{
    public class WaveLeft1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristLeft).ScaleX < skeleton.GetJoint(JointType.ElbowLeft).ScaleX
                && skeleton.GetJoint(JointType.WristLeft).ScaleY < skeleton.GetJoint(JointType.ElbowLeft).ScaleY;
        }
    }

    public class WaveLeft2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristLeft).ScaleX > skeleton.GetJoint(JointType.ElbowLeft).ScaleX
                && skeleton.GetJoint(JointType.WristLeft).ScaleY < skeleton.GetJoint(JointType.ElbowLeft).ScaleY;
        }
    }

    public class WaveRight1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristRight).ScaleX > skeleton.GetJoint(JointType.ElbowRight).ScaleX
                && skeleton.GetJoint(JointType.WristRight).ScaleY < skeleton.GetJoint(JointType.ElbowRight).ScaleY;
        }
    }

    public class WaveRight2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.WristRight).ScaleX < skeleton.GetJoint(JointType.ElbowRight).ScaleX
                && skeleton.GetJoint(JointType.WristRight).ScaleY < skeleton.GetJoint(JointType.ElbowRight).ScaleY;
        }
    }
}