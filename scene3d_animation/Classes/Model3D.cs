using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace scene3d_animation.Classes
{
    class Model3D
    {
        // displayList с объектом
        public int displayList;
        public string name;
        // Параметры объекта
        public Vector3 position;
        public Vector3 size;
        public Vector3 angle;
        // Анимация объекта
        private List<Animation.AnimationFunc> animationrotate;
        private List<Animation.AnimationFunc> animationscale;
        private List<Animation.AnimationFunc> animationtrans;

        // Создание объекта
        // name - заданное имя объекта
        private Model3D(string name = "")
        {
            this.name = name;
            position = new Vector3(0);
            size = new Vector3(1);
            angle = new Vector3(0);
            animationrotate = new List<Animation.AnimationFunc>();
            animationtrans = new List<Animation.AnimationFunc>();
            animationscale = new List<Animation.AnimationFunc>();
        }

        // filename - путь к файлу с моделью, name - произвольное имя объекта
        public Model3D(string filename, string name = "", bool notexture = false):this(name)
        {
            Model3DLoader loader = new Model3DLoader();
            displayList = loader.LoadPath(filename, notexture);
            
        }

        // displayList - скомпилированный displayList с объектом, name - произвольное имя объекта
        public Model3D(int displayList, string name = ""):this(name)
        {
            this.displayList = displayList;
        }

        // отрисовка объекта
        public void Draw()
        {
            GL.PushMatrix();
            GL.Translate(position);
            GL.Rotate(angle.X, 1, 0, 0);
            GL.Rotate(angle.Y, 0, 1, 0);
            GL.Rotate(angle.Z, 0, 0, 1);
            GL.Scale(size);
            GL.CallList(displayList);
            GL.PopMatrix();
        }

        // Получение списка действий для анимации 
        public List<Animation.AnimationFunc> AnimationRotate
        {
            get => animationrotate;
        }

        public List<Animation.AnimationFunc> AnimationScale
        {
            get => animationscale;
        }

        public List<Animation.AnimationFunc> AnimationTrans
        {
            get => animationtrans;
        }

        // Расчет новых параметров объекта в момент времени time
        public void AnimationTick(int time)
        {
            // Расчет новых параметров объекта
            animationtrans.ForEach(x =>
            {
                if (x.start <= time && time <= x.end)
                    position = x.func(time, position);
            });
            animationrotate.ForEach(x =>
            {
                if (x.start <= time && time <= x.end)
                    angle = x.func(time, angle);
            });
            animationscale.ForEach(x =>
            {
                if (x.start <= time && time <= x.end)
                    size = x.func(time, size);
            });
            if (angle.X >= 360) angle.X -= 360;
            if (angle.Y >= 360) angle.Y -= 360;
            if (angle.Z >= 360) angle.Z -= 360;
        }
    }
}
