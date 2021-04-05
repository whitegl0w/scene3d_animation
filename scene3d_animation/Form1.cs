using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
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
            // Создание объекта сцены
            scene = new Scene3D();

            // Добавление объектов на сцену
            scene.AddModel(new Model3D("models/table/table.obj", "Стол", true));
            scene["Стол"].size = new Vector3(4f);

            scene.AddModel(new Model3D("models/plates/plates.obj", "Тарелка", true));
            scene["Тарелка"].size = new Vector3(1.8f);
            scene["Тарелка"].position = new Vector3(0.6f, 1.4f, -0.1f);

            scene.AddModel(new Model3D("models/piece_of_cake/piece_of_cake.obj", "Тортик1"));
            scene["Тортик1"].size = new Vector3(0.4f);
            scene["Тортик1"].position = new Vector3(0.7f, 1.9f, -0.6f);

            scene.AddModel(new Model3D("models/piece_of_cake/piece_of_cake_full.obj", "Тортик2"));
            scene["Тортик2"].size = new Vector3(0.8f);
            scene["Тортик2"].angle.Y = 90;
            scene["Тортик2"].position = new Vector3(0.3f, 1.9f, -1.0f);

            scene.AddModel(new Model3D("models/cup/cup.obj", "Чашка"));
            scene["Чашка"].size = new Vector3(0.5f);
            scene["Чашка"].position = new Vector3(-1.1f, 1.45f, 0.5f);

            scene.AddModel(new Model3D("models/teapot/teapot.obj", "Чайник"));
            scene["Чайник"].position = new Vector3(-22.2f, 2.5f, -20.6f);
            scene["Чайник"].size = new Vector3(0.7f);
            scene["Чайник"].angle.Y = -45;

            scene.AddModel(new Model3D("models/disco_ball/disco_ball.obj", "Дискошар"));
            scene["Дискошар"].position = new Vector3(0f, 4f, 0f);
            scene["Дискошар"].size = new Vector3(0.7f);

            scene.AddModel(new Model3D("models/switch/switch.obj", "Выключатель"));
            scene["Выключатель"].position = new Vector3(-0.7f, 1.24f, 1.7f);
            scene["Выключатель"].size = new Vector3(0.2f);

            scene.AddModel(new Model3D("models/switch/button/switch_button.obj", "Кнопка"));
            scene["Кнопка"].position = new Vector3(-0.7f, 1.3f, 1.7f);
            scene["Кнопка"].size = new Vector3(0.15f);

            scene.AddModel(new Model3D("models/gas_bottle/gas_bottle.obj", "Гелий"));
            scene["Гелий"].position = new Vector3(3f, -0.25f, 3f);
            scene["Гелий"].angle.Y = -90;
            scene["Гелий"].size = new Vector3(1.4f);

            scene.AddModel(new Model3D("models/gas_bottle/cap/gas_bottle_cap.obj", "Вентиль"));
            scene["Вентиль"].position = new Vector3(3f, 1.17f, 3f);
            scene["Вентиль"].size = new Vector3(0.12f);

            scene.AddModel(new Model3D("models/balloon/balloon.obj", "Шарик"));
            scene["Шарик"].position = new Vector3(3.35f, 0.6f, 3f);
            scene["Шарик"].angle.Z = 210;
            scene["Шарик"].size = new Vector3(0.4f);

            // Анимирование объектов
            // Приближение к сцене
            scene.position.Z = -250;
            scene.AnimationTrans.Add(Animation.MoveOn(z: 250, end: 380));
            scene.AnimationRotate.Add(Animation.MoveOn(x: -15, start: 350, end: 390));
            // Облет сцены
            scene.AnimationRotate.Add(Animation.MoveOn(y: 360, start: 360, end: 740));
            scene.AnimationScale.Add(Animation.MoveOn(new Vector3(2), start: 380, end: 720));
            scene.AnimationTrans.Add(Animation.MoveOn(y: -4, start: 380, end: 740));
            // Облет 2
            scene.AnimationRotate.Add(Animation.MoveOn(x: 30, y: 360, start: 740, end: 1160));
            scene.AnimationScale.Add(Animation.MoveOn(new Vector3(-1), start: 740, end: 1060));
            // Отлет
            scene.AnimationScale.Add(Animation.MoveOn(new Vector3(0.5f), start: 1180, end: 1400));
            scene.AnimationTrans.Add(Animation.MoveOn(y: 1, start: 1180, end: 1400));
            scene.AnimationRotate.Add(Animation.MoveOn(x: -15, start: 1180, end: 1400));
            // Подъезд к торту
            scene.AnimationRotate.Add(Animation.MoveOn(y: -60, x: 50, start: 1400, end: 1600));
            scene.AnimationScale.Add(Animation.MoveOn(new Vector3(1f), start: 1400, end: 1600));
            scene.AnimationTrans.Add(Animation.MoveOn(z: -8, start: 1380, end: 1560));
            // Отрезать торт
            scene["Тортик1"].AnimationTrans.Add(Animation.MoveOn(z: 1.49f, x: 0.30f, y: -0.35f, start: 1580, end: 1620));
            // Подъезд к чайнику
            scene.AnimationRotate.Add(Animation.MoveOn(y: 80, x: -40, start: 1620, end: 1680));
            // Налить чай
            scene["Чайник"].AnimationTrans.Add(Animation.MoveOn(z: 20.7f, x: 20.7f, start: 1600, end: 1790));
            scene["Чайник"].AnimationRotate.Add(Animation.MoveOn(z: -60f, start: 1800, end: 1830));
            scene["Чайник"].AnimationRotate.Add(Animation.MoveOn(z: 60f, start: 1860, end: 1890));
            scene["Чайник"].AnimationTrans.Add(Animation.MoveOn(z: -1.7f, x: -1.7f, start: 1890, end: 1940));
            // Подъезд к чашке
            scene.AnimationRotate.Add(Animation.MoveOn(y: 45, x: 20, start: 1960, end: 2020));
            scene.AnimationScale.Add(Animation.MoveOn(new Vector3(1f), start: 1960, end: 2020));
            // Подвинуть чашку
            scene["Чашка"].AnimationTrans.Add(Animation.MoveOn(z: -0.4f, x: -0.8f, start: 2040, end: 2100));
            // Посмотреть на нерабочий шар
            scene.AnimationRotate.Add(Animation.MoveOn(x: -50, start: 2110, end: 2160));
            scene.AnimationTrans.Add(Animation.MoveOn(y: -10, start: 2110, end: 2160));
            // Посмотреть на выключатель
            scene.AnimationRotate.Add(Animation.MoveOn(x: 70, y: -35, start: 2190, end: 2230));
            scene.AnimationTrans.Add(Animation.MoveOn(y: 10, z: -12, start: 2190, end: 2230));
            // Нажатие кнопки
            scene["Кнопка"].AnimationTrans.Add(Animation.MoveOn(y: -0.05f, start: 2250, end: 2260));
            // Включение шара
            scene["Дискошар"].AnimationRotate.Add(Animation.TickInc(y: 3f, start: 2260));
            // Опять смотреть на шар
            scene.AnimationRotate.Add(Animation.MoveOn(x: -70, y: 35, start: 2280, end: 2320));
            scene.AnimationTrans.Add(Animation.MoveOn(y: -10, z: 12, start: 2280, end: 2320));
            // Облет шара
            scene.AnimationTrans.Add(Animation.MoveOn(y: -2, start: 2350, end: 2410));
            scene.AnimationRotate.Add(Animation.MoveOn(y: -790, start: 2420, end: 2550));
            // Подъезд к балону с гелием
            scene.AnimationScale.Add(Animation.MoveOn(new Vector3(-1.2f), start: 2570, end: 2630));
            scene.AnimationTrans.Add(Animation.MoveOn(x: -7f, y: 17, start: 2570, end: 2630));
            scene.AnimationRotate.Add(Animation.MoveOn(x: 40, start: 2570, end: 2630));
            // Открыть вентиль
            scene["Вентиль"].AnimationRotate.Add(Animation.TickInc(y: 20f, start: 2650, end: 2700));
            // Надуть шар
            scene["Шарик"].AnimationScale.Add(Animation.MoveOn(x: 2.4f, y: 0.10f, z: 0.10f, start: 2720, end: 3040));
            scene["Шарик"].AnimationRotate.Add(Animation.MoveOn(z: 110f, start: 2720, end: 2960));
            scene["Шарик"].AnimationTrans.Add(Animation.MoveOn(x: 0.2f, y: 0.5f, start: 2720, end: 2880));
            scene["Шарик"].AnimationTrans.Add(Animation.MoveOn(x: -0.1f, y: 0.2f, start: 2880, end: 2960));
            // Закрыть вентиль
            scene["Вентиль"].AnimationRotate.Add(Animation.TickInc(y: -10f, start: 2880, end: 3040));
            // Шарик улетает
            scene["Шарик"].AnimationTrans.Add(Animation.MoveOn(y: 25f, start: 3060, end: 3700));
            scene.AnimationRotate.Add(Animation.MoveOn(x: -100, start: 3070, end: 3400));
            scene.AnimationTrans.Add(Animation.MoveOn(y: -30f, start: 3070, end: 3200));
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            // Настрока параметров
            GL.ClearColor(Color.CornflowerBlue);
            GL.Enable(EnableCap.Texture2D);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Normalize);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.Viewport(0, 0, Width, Height);

            // Настройка проекции и положения камеры
            float aspectRatio = Width / (float)Height;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver6, aspectRatio, 1, 300);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 lookat = Matrix4.LookAt(2, 4, 30, 0, 1.5f, 0, 0, 1, 0);
            GL.LoadMatrix(ref lookat);
        }

        // Метод отрисовки 
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            scene.Draw();
            glControl1.SwapBuffers();
        }

        // Расчет нового положения элементов в текущий момент времени
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Остановить таймер, если анимация завершилась
            if (trackBar1.Value == trackBar1.Maximum)
            {
                trackBar1.Value = trackBar1.Minimum;
                timer1.Enabled = false;
            }
            trackBar1.Value += 1;
            // Расчет положения элементов сцены в момент времени trackBar1.Value
            scene.AnimationTick(trackBar1.Value);
            textBox1.Text = trackBar1.Value.ToString();
            // Обновление изображения
            glControl1.Invalidate();
        }

        // Обработка клавиш
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.P: timer1.Enabled = !timer1.Enabled; break;
            }
        }
    }
}
