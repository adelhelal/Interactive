using Microsoft.Kinect;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System;
using Interactive.Gestures;
using System.Diagnostics;

namespace Interactive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        private Skeleton skeleton;
        private Speech speech;

        private Gesture gestureWaveLeft;
        private Gesture gestureLoop;
        private Gesture gestureShakeOff;
        private Gesture gestureTravolta;

        private bool isOpened;
        private bool isMouseActivated;
        private bool isGrabbing;

        public Main()
        {
            InitializeComponent();

            skeleton = new Skeleton(KinectCanvas);
            speech = new Speech(InteractiveLabel);

            var kinect = KinectSensor.GetDefault();
            kinect.Open();

            var bodyReader = kinect.BodyFrameSource.OpenReader();
            bodyReader.FrameArrived += bodyReader_FrameArrived;

            InitializeGesture();
        }

        private void InitializeGesture()
        {
            var wave1 = new WaveLeft1();
            var wave2 = new WaveLeft2();
            gestureWaveLeft = new Gesture(new IGesture[] { wave1, wave2, wave1, wave2 });
            gestureWaveLeft.GestureRecognized += gestureWaveLeft_GestureRecognized;

            var loop1 = new Loop1();
            var loop2 = new Loop2();
            var loop3 = new Loop3();
            var loop4 = new Loop4();
            gestureLoop = new Gesture(new IGesture[] { loop1, loop2, loop3, loop4 });
            gestureLoop.GestureRecognized += gestureLoop_GestureRecognized;

            var leftUp = new LeftUp();
            var leftDown = new LeftDown();
            gestureShakeOff = new Gesture(new IGesture[] { leftUp, leftDown, leftUp, leftDown });
            gestureShakeOff.GestureRecognized += gestureShakeOff_GestureRecognized;

            var travolta1 = new Travolta1();
            var travolta2 = new Travolta2();
            gestureTravolta = new Gesture(new IGesture[] { travolta1, travolta2 });
            gestureTravolta.GestureRecognized += gestureTravolta_GestureRecognized;
        }

        private void GetGesture()
        {
            gestureWaveLeft.GetGesture(skeleton);
            gestureLoop.GetGesture(skeleton);
            gestureShakeOff.GetGesture(skeleton);
            gestureTravolta.GetGesture(skeleton);
        }

        private void ResetGestures()
        {
            gestureWaveLeft.Reset();
            gestureTravolta.Reset();
            gestureLoop.Reset();
        }

        private void MouseClick(float x, float y)
        {
            if (!isMouseActivated) return;
            //var x = 65150 * (1 + joint.Position.X);
            //var y = 65150 * (1 - joint.Position.Y);
            var mouseX = 65150 * (1 + x) * ((0.75 * (1 + x)) / 0.75);
            var mouseY = 65150 * (1 - y) * ((0.75 * (1 - y)) / 0.75);
            Mouse.MoveTo((int)mouseX, (int)mouseY);
            Mouse.LeftClick((int)mouseX, (int)mouseY);
        }

        private void MouseDrag(float x, float y)
        {
            if (!isMouseActivated) return;
            //var x = 65150 * (1 + joint.Position.X);
            //var y = 65150 * (1 - joint.Position.Y);
            var mouseX = 65150 * (1 + x) * ((0.75 * (1 + x)) / 0.75);
            var mouseY = 65150 * (1 - y) * ((0.75 * (1 - y)) / 0.75);
            Mouse.MoveTo((int)mouseX, (int)mouseY);

            if (!isGrabbing)
            {
                Mouse.LeftDown((int)mouseX, (int)mouseY);
                isGrabbing = true;
            }
        }

        private void MouseDrop(float x, float y)
        {
            if (!isMouseActivated) return;
            //var x = 65150 * (1 + joint.Position.X);
            //var y = 65150 * (1 - joint.Position.Y);
            var mouseX = 65150 * (1 + x) * ((0.75 * (1 + x)) / 0.75);
            var mouseY = 65150 * (1 - y) * ((0.75 * (1 - y)) / 0.75);
            Mouse.MoveTo((int)mouseX, (int)mouseY);

            if (isGrabbing)
            {
                Mouse.LeftUp((int)mouseX, (int)mouseY);
                isGrabbing = false;
            }
        }

        private void bodyReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    skeleton.ClearAll();

                    var bodies = new Body[frame.BodyCount];
                    frame.GetAndRefreshBodyData(bodies);

                    foreach (var body in bodies.Where(b => b.IsTracked))
                    {
                        foreach (var joint in body.Joints.Select(j => j.Value))
                        {
                            switch (joint.JointType)
                            {
                                case JointType.HandLeft:
                                    switch (body.HandLeftState)
                                    {
                                        case HandState.Lasso:
                                            skeleton.DrawPoint(joint, Brushes.Green, body.HandLeftConfidence == TrackingConfidence.High ? 10 : 5);
                                            MouseDrag(joint.Position.X, joint.Position.Y);
                                            continue;
                                        case HandState.Closed:
                                            skeleton.DrawPoint(joint, Brushes.Blue, body.HandLeftConfidence == TrackingConfidence.High ? 10 : 5);
                                            MouseDrag(joint.Position.X, joint.Position.Y);
                                            continue;
                                        case HandState.Open:
                                            MouseDrop(joint.Position.X, joint.Position.Y);
                                            break;
                                    }
                                    break;
                                case JointType.HandRight:
                                    switch (body.HandRightState)
                                    {
                                        case HandState.Lasso:
                                            skeleton.DrawPoint(joint, Brushes.Orange, body.HandRightConfidence == TrackingConfidence.High ? 10 : 5);
                                            continue;
                                        case HandState.Closed:
                                            skeleton.DrawPoint(joint, Brushes.Red, body.HandRightConfidence == TrackingConfidence.High ? 10 : 5);
                                            speech.Initialize();
                                            continue;
                                        case HandState.Open:
                                            speech.Dispose();
                                            break;
                                    }
                                    break;
                            }

                            skeleton.DrawPoint(joint, Brushes.White);
                        }

                        GetGesture();
                    }
                }
            }
        }

        private void gestureWaveLeft_GestureRecognized(object sender, EventArgs e)
        {
            if (!isOpened)
            {
                InteractiveLabel.Content = "Left Wave";
                Process.Start("https://trello.com/b/YwsIKx6A/change-day-2017");
                ResetGestures();
                isOpened = true;
            }
        }

        private void gestureTravolta_GestureRecognized(object sender, EventArgs e)
        {
            InteractiveLabel.Content = "Travolta";
            Process.Start("http://imagesmtv-a.akamaihd.net/uri/mgid:file:http:shared:mtv.com/news/wp-content/uploads/2015/02/sat-night-fever-gif-1423215483.gif?quality=.8&height=283&width=512");
            ResetGestures();
        }

        private void gestureLoop_GestureRecognized(object sender, EventArgs e)
        {
            isMouseActivated = true;
        }

        private void gestureShakeOff_GestureRecognized(object sender, EventArgs e)
        {
            isMouseActivated = false;
        }
    }
}
