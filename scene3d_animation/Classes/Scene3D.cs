using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scene3d_animation.Classes
{
    class Scene3D
    {
        private List<Model3D> models;

        public Model3D this[int index]
        {
            get => models[index];
        }

        public List<Model3D> Models
        {
            get => models;
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
