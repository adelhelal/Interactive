using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Interactive
{
    public class Skeleton : List<JointScale>
    {
        public Skeleton(Canvas canvas)
        {
            this.Canvas = canvas;
        }

        public Canvas Canvas { get; set; }

        public void ClearAll()
        {
            this.Clear();
            this.Canvas.Children.Clear();
        }

        public void DrawPoint(Joint joint, Brush brush, double thickness = 5.0)
        {
            var x = Scale.X(joint.Position.X, this.Canvas.ActualWidth);
            var y = Scale.Y(joint.Position.Y, this.Canvas.ActualHeight);

            var line = new Line
            {
                X1 = x,
                Y1 = y,
                X2 = x + thickness,
                Y2 = y + thickness,
                StrokeThickness = thickness * 1.5,
                Stroke = brush
            };

            this.Add(new JointScale { Joint = joint, ScaleX = x, ScaleY = y });
            this.Canvas.Children.Add(line);
        }

        public JointScale GetJoint(JointType type)
        {
            return this.Find(j => j.Joint.JointType == type);
        }
    }
}
