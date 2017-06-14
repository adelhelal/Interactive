using Microsoft.Kinect;

namespace Interactive.Gestures
{
    public class Travolta1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandRight).ScaleY < skeleton.GetJoint(JointType.SpineMid).ScaleY
                && skeleton.GetJoint(JointType.HandRight).ScaleX > skeleton.GetJoint(JointType.SpineMid).ScaleX
                && skeleton.GetJoint(JointType.KneeRight).ScaleX - skeleton.GetJoint(JointType.KneeLeft).ScaleX > 30;
        }
    }

    public class Travolta2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandRight).ScaleY > skeleton.GetJoint(JointType.SpineMid).ScaleY
                && skeleton.GetJoint(JointType.HandRight).ScaleX < skeleton.GetJoint(JointType.SpineMid).ScaleX
                && skeleton.GetJoint(JointType.KneeRight).ScaleX - skeleton.GetJoint(JointType.KneeLeft).ScaleX > 30;
        }
    }
}