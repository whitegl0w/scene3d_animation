using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace scene3d_animation.Classes
{
    class Scene3D
    {
        private List<Model3D> models;
        public Vector3 position;
        public Vector3 size;
        public Vector3 angle;
        private List<Animation.AnimationFunc> animationrotate;
        private List<Animation.AnimationFunc> animationscale;
        private List<Animation.AnimationFunc> animationtrans;

        public Model3D this[int index]
        {
            get => models[index];
        }

        public Model3D this[string name]
        {
            get => models.Find(x => x.name == name);
        }

        public List<Model3D> Models
        {
            get => models;
        }

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

        public Scene3D()
        {
            models = new List<Model3D>();
        }

        public void AddModel(Model3D model)
        { 
            models.Add(model);
        }

        public void Draw()
        {
            foreach(Model3D model in models)
            {
                model.Draw();
            }
        }

        public void AnimationTick(int time)
        {
            foreach (Model3D model in models)
            {
                model.AnimationTick(time);
            }
        }
    }
}
