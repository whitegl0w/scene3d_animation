using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace scene3d_animation.Classes
{
    // Класс сцены
    class Scene3D
    {
        // Список объектов сцены
        private List<Model3D> models;
        // Параметры камеры
        public Vector3 position;
        public Vector3 size;
        public Vector3 angle;
        // Анимация камеры
        private List<Animation.AnimationFunc> animationrotate;
        private List<Animation.AnimationFunc> animationscale;
        private List<Animation.AnimationFunc> animationtrans;

        // Возврат указанного объекта по его номеру в списке
        public Model3D this[int index]
        {
            get => models[index];
        }

        // Возврат указанного объекта по его имени
        public Model3D this[string name]
        {
            get => models.Find(x => x.name == name);
        }

        // Получение списка объектов сцены
        public List<Model3D> Models
        {
            get => models;
        }

        // Получение списка действий для анимации вращения 
        public List<Animation.AnimationFunc> AnimationRotate
        {
            get => animationrotate;
        }

        // Получение списка действий для анимации масштабирования 
        public List<Animation.AnimationFunc> AnimationScale
        {
            get => animationscale;
        }

        // Получение списка действий для анимации сдвига 
        public List<Animation.AnimationFunc> AnimationTrans
        {
            get => animationtrans;
        }

        // Конструктор для создания сцены
        public Scene3D()
        {
            models = new List<Model3D>();
            position = new Vector3(0);
            size = new Vector3(1);
            angle = new Vector3(0);
            animationrotate = new List<Animation.AnimationFunc>();
            animationtrans = new List<Animation.AnimationFunc>();
            animationscale = new List<Animation.AnimationFunc>();
        }

        // Добавление модели на сцену
        public void AddModel(Model3D model)
        { 
            models.Add(model);
        }

        // Отрисовка сцены 
        public void Draw()
        {
            GL.PushMatrix();
            // Поворот, сдвиг, масштаб камеры
            GL.Translate(position);
            GL.Rotate(angle.X, 1, 0, 0);
            GL.Rotate(angle.Y, 0, 1, 0);
            GL.Rotate(angle.Z, 0, 0, 1);
            GL.Scale(size);
            // Отрисовка моделей
            foreach (Model3D model in models)
            {
                model.Draw();
            }
            GL.PopMatrix();
        }

        // Расчет нового положения элементов в момент времени time
        public void AnimationTick(int time)
        {
            // Расчет новых параметров камеры
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

            // Расчет новых параметров объектов
            foreach (Model3D model in models)
            {
                model.AnimationTick(time);
            }
        }
    }
}
