using Microsoft.Kinect;

namespace Interactive.Gestures
{
    public class Loop1 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandLeft).ScaleY < skeleton.GetJoint(JointType.SpineMid).ScaleY
                && skeleton.GetJoint(JointType.HandLeft).ScaleX < skeleton.GetJoint(JointType.SpineMid).ScaleX;
        }
    }

    public class Loop2 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandLeft).ScaleY < skeleton.GetJoint(JointType.SpineMid).ScaleY
                && skeleton.GetJoint(JointType.HandLeft).ScaleX > skeleton.GetJoint(JointType.SpineMid).ScaleX;
        }
    }

    public class Loop3 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandLeft).ScaleY > skeleton.GetJoint(JointType.SpineMid).ScaleY
                && skeleton.GetJoint(JointType.HandLeft).ScaleX > skeleton.GetJoint(JointType.SpineMid).ScaleX;
        }
    }

    public class Loop4 : IGesture
    {
        public bool Update(Skeleton skeleton)
        {
            return skeleton.GetJoint(JointType.HandLeft).ScaleY > skeleton.GetJoint(JointType.SpineMid).ScaleY
                && skeleton.GetJoint(JointType.HandLeft).ScaleX < skeleton.GetJoint(JointType.SpineMid).ScaleX;
        }
    }
}