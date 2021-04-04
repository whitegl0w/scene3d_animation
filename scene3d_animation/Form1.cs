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
        Vector3 camRot;
        float camZoom;

        private void Form1_Load(object sender, EventArgs e)
        {
            camRot = new Vector3(0);
            camZoom = 1;
            scene = new Scene3D();

            scene.AddModel(new Model3D("models/table/table.obj", "Стол", true));
            scene["Стол"].size = new Vector3(4f);

            scene.AddModel(new Model3D("models/plates/plates.obj", "Тарелка", true));
            scene["Тарелка"].size = new Vector3(1.8f);
            scene["Тарелка"].position = new Vector3(0.6f, 1.4f, -0.1f);

            Model3DLoader loader = new Model3DLoader();
            int cake = loader.LoadPath("models/piece_of_cake/piece_of_cake.obj");
            scene.AddModel(new Model3D(cake, "Тортик1"));
            scene["Тортик1"].size = new Vector3(0.4f);
            scene["Тортик1"].position = new Vector3(0.7f, 1.9f, -0.6f);
            scene["Тортик1"].AnimationTrans.Add(Animation.MoveOn(z: 1.58f, x: 0.35f, y: -0.35f, start: 70, end: 105));

            scene.AddModel(new Model3D(cake, "Тортик2"));
            scene["Тортик2"].size = new Vector3(0.4f);
            scene["Тортик2"].angle.Y = -90;
            scene["Тортик2"].position = new Vector3(-0.1f, 1.9f, -0.6f);

            scene.AddModel(new Model3D(cake, "Тортик3"));
            scene["Тортик3"].size = new Vector3(0.4f);
            scene["Тортик3"].angle.Y = 90;
            scene["Тортик3"].position = new Vector3(0.7f, 1.9f, -1.4f);

            scene.AddModel(new Model3D(cake, "Тортик4"));
            scene["Тортик4"].size = new Vector3(0.4f);
            scene["Тортик4"].angle.Y = 180;
            scene["Тортик4"].position = new Vector3(-0.1f, 1.9f, -1.4f);

            scene.AddModel(new Model3D("models/cup/cup.obj", "Чашка"));
            scene["Чашка"].size = new Vector3(0.5f);
            scene["Чашка"].position = new Vector3(-1.1f, 1.5f, 0.5f);

            scene.AddModel(new Model3D("models/teapot/teapot.obj", "Чайник"));
            scene["Чайник"].position = new Vector3(-1.3f, 2.5f, 0.3f);
            scene["Чайник"].size = new Vector3(0.7f);
            scene["Чайник"].angle.Y = -45;
            scene["Чайник"].AnimationRotate.Add(Animation.MoveOn(z: -60f, end: 35));
            scene["Чайник"].AnimationRotate.Add(Animation.MoveOn(z: 60f, start: 36, end: 70));

            scene.AddModel(new Model3D("models/disco_ball/disco_ball.obj", "Дискошар"));
            scene["Дискошар"].position = new Vector3(0f, 4f, 0f);
            scene["Дискошар"].size = new Vector3(0.7f);
            scene["Дискошар"].AnimationRotate.Add(Animation.TickInc(y: 3f));

            scene.AddModel(new Model3D("models/switch/switch.obj", "Выключатель"));
            scene["Выключатель"].position = new Vector3(-0.7f, 1.25f, 1.7f);
            scene["Выключатель"].size = new Vector3(0.2f);

            scene.AddModel(new Model3D("models/switch/button/switch_button.obj", "Кнопка"));
            scene["Кнопка"].position = new Vector3(-0.7f, 1.3f, 1.7f);
            scene["Кнопка"].size = new Vector3(0.15f);
            scene["Кнопка"].AnimationTrans.Add(Animation.MoveOn(y: -0.05f, start: 60, end: 70));

            scene.AddModel(new Model3D("models/gas_bottle/gas_bottle.obj", "Гелий"));
            scene["Гелий"].position = new Vector3(3f, -0.25f, 3f);
            scene["Гелий"].angle.Y = -90;
            scene["Гелий"].size = new Vector3(1.4f);

            scene.AddModel(new Model3D("models/gas_bottle/cap/gas_bottle_cap.obj", "Вентиль"));
            scene["Вентиль"].position = new Vector3(3f, 1.17f, 3f);
            scene["Вентиль"].size = new Vector3(0.12f);
            scene["Вентиль"].AnimationRotate.Add(Animation.TickInc(y: 10f));

            scene.AddModel(new Model3D("models/balloon/balloon.obj", "Шарик"));
            scene["Шарик"].position = new Vector3(3.3f, 0.6f, 3f);
            scene["Шарик"].angle.Z = 210;
            scene["Шарик"].size = new Vector3(0.4f);
            scene["Шарик"].AnimationScale.Add(Animation.TickInc(x: 0.01f, y: 0.0005f, z: 0.0005f));
            scene["Шарик"].AnimationRotate.Add(Animation.TickInc(z: 0.4f));
            scene["Шарик"].AnimationTrans.Add(Animation.TickInc(x: 0.002f, y: 0.002f));
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
            Matrix4 lookat = Matrix4.LookAt(2, 4, 30, 0, 1.5f, 0, 0, 1, 0);
            GL.LoadMatrix(ref lookat);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.PushMatrix();
            GL.Scale(new Vector3(camZoom));
            GL.Rotate(camRot.X, 1, 0, 0);
            GL.Rotate(camRot.Y, 0, 1, 0);
            GL.Rotate(camRot.Z, 0, 0, 1);
            scene.Draw();
            GL.PopMatrix();
            glControl1.SwapBuffers();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D: camRot.Z += 3; break;
                case Keys.A: camRot.Z -= 3; break;
                case Keys.W: camRot.X += 3; break;
                case Keys.S: camRot.X -= 3; break;
                case Keys.Q: camRot.Y += 3; break;
                case Keys.E: camRot.Y -= 3; break;
                case Keys.R: camZoom -= 0.2f; break;
                case Keys.T: camZoom += 0.2f; break;
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
