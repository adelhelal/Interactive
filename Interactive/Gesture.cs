using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactive
{
    public class Gesture
    {
        IGesture[] gestures;
        int gestureIndex = 0;
        int frameCount = 0;

        public event EventHandler GestureRecognized;

        public Gesture(IGesture[] gestures)
        {
            this.gestures = gestures;
        }

        public int GetGesture(Skeleton skeleton)
        {
            if (gestureIndex >= gestures.Count() || gestures[gestureIndex].Update(skeleton))
            {
                gestureIndex++;
                if (gestureIndex >= gestures.Count())
                {
                    if (GestureRecognized != null)
                    {
                        GestureRecognized(this, new EventArgs());
                        Reset();
                    }
                }
            }
            else if (frameCount > 100)
            {
                Reset();
            }

            frameCount++;
            return gestureIndex;
        }

        public void Reset()
        {
            frameCount = 0;
            gestureIndex = 0;
        }
    }

    public interface IGesture
    {
        bool Update(Skeleton skeleton);
    }
}
