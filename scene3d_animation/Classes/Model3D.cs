using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using Assimp.Configs;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Reflection;
using System.Drawing.Imaging;
using Assimp;
using System.IO;

namespace scene3d_animation.Classes
{
    class Model3D
    {
        public int displayList;
        public string name;
        public Vector3 position;
        public Vector3 size;
        public Vector3 angle;
        private Func<int, Vector3, Vector3> animationrotate;
        private Func<int, Vector3, Vector3> animationscale;
        private Func<int, Vector3, Vector3> animationtrans;

        // name - произвольное имя объекта
        private Model3D(string name = "")
        {
            this.name = name;
            position = new Vector3(0);
            size = new Vector3(1);
            angle = new Vector3(0);
            animationrotate = (t, curr) => curr;
            animationtrans = (t, curr) => curr;
            animationscale = (t, curr) => curr;
        }

        // filename - путь к файлу с моделью, name - произвольное имя объекта
        public Model3D(string filename, string name = "", bool notexture = false):this(name)
        {
            Model3DLoader loader = new Model3DLoader();
            displayList = loader.LoadPath(filename, notexture);
            
        }
        public Model3D(int displayList, string name = "") :this(name)
        {
            this.displayList = displayList;
        }

        public void Draw()
        {
            GL.PushMatrix();
            GL.Translate(position);
            GL.Scale(size);
            GL.Rotate(angle.X, 1, 0, 0);
            GL.Rotate(angle.Y, 0, 1, 0);
            GL.Rotate(angle.Z, 0, 0, 1);
            GL.CallList(displayList);
            GL.PopMatrix();
        }

        public Func<int, Vector3, Vector3> AnimationRotate
        {
            set => animationrotate = value;
        }

        public Func<int, Vector3, Vector3> AnimationScale
        {
            set => animationscale = value;
        }

        public Func<int, Vector3, Vector3> AnimationTrans
        {
            set => animationtrans = value;
        }

        public void AnimationTick(int time)
        {
            position = animationtrans(time, position);
            size = animationscale(time, size);
            angle = animationrotate(time, angle);
        }
    }
}
