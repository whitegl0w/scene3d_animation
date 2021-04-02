using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assimp.Configs;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using scene3d_animation.Classes;

namespace scene3d_animation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Scene3D scene;

        private void Form1_Load(object sender, EventArgs e)
        {
            scene = new Scene3D();
            var table = new Model3D("models/table/table.obj", "Стол", true);
            table.size = new Vector3(2f);
            scene.AddModel(table);
            var plates = new Model3D("models/plates/plates.obj", "Тарелка", true);
            plates.position.Y = 0.7f;
            scene.AddModel(plates);
            var cake = new Model3D("models/piece_of_cake/piece_of_cake.obj", "Тортик");
            cake.position.Y = 1f;
            cake.size = new Vector3(0.2f);
            scene.AddModel(cake);
            var cup = new Model3D("models/cup/cup.obj", "Чашка");
            //cup.position.Y = 1.5f; 
            scene.AddModel(cup);
            var teapot = new Model3D("models/teapot/teapot.obj", "Чайник");
            teapot.position.Y += 2;
            teapot.AnimationRotate = (t, cur) => { cur.Y += 3; return cur; };
            scene.AddModel(teapot);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color.CornflowerBlue);

            GL.Enable(EnableCap.Texture2D);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Normalize);
            GL.FrontFace(FrontFaceDirection.Ccw);

            GL.Viewport(0, 0, Width, Height);

            float aspectRatio = Width / (float)Height;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver6, aspectRatio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 lookat = Matrix4.LookAt(2, 4, 12, 0, 1.5f, 0, 0, 1, 0);
            GL.LoadMatrix(ref lookat);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            scene.Draw();
            glControl1.SwapBuffers();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D: scene[0].angle.X += 3; break;
                case Keys.A: scene[0].angle.X -= 3; break;
                case Keys.W: scene[1].angle.Z += 3; break;
                case Keys.S: scene[1].angle.Z -= 3; break;
            }
            glControl1.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (trackBar1.Value == trackBar1.Maximum)
                trackBar1.Value = trackBar1.Minimum;
            trackBar1.Value += 1;
            scene.AnimationTick(trackBar1.Value);
            glControl1.Refresh();
        }
    }
}
