using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace scene3d_animation.Classes
{
    static class Animation
    {
        public class AnimationFunc
        {
            public Func<int, Vector3, Vector3> func;
            public int start;
            public int end;

            public AnimationFunc(Func<int, Vector3, Vector3> func, int start = 0, int end = int.MaxValue)
            {
                this.func = func;
                this.start = start;
                this.end = end;
            }
        }

        public static AnimationFunc MoveTo(float x = 0, float y = 0, float z = 0, int start = 0, int end = int.MaxValue)
        {
            int duration = end - start;
            Vector3 v = new Vector3(x, y, z) / duration;
            return new AnimationFunc((t, curr) => curr * (1f - 1f/duration) + v, start, end);
        }

        public static AnimationFunc MoveOn(float x = 0, float y = 0, float z = 0, int start = 0, int end = int.MaxValue)
        {
            int duration = end - start;
            Vector3 v = new Vector3(x, y, z) / duration;
            return new AnimationFunc((t, curr) => curr + v, start, end);
        }

        public static AnimationFunc TickInc(float x = 0, float y = 0, float z = 0, int start = 0, int end = int.MaxValue)
        {
            Vector3 v = new Vector3(x, y, z);
            return new AnimationFunc((t, curr) => curr + v, start, end);
        }
    }
}
