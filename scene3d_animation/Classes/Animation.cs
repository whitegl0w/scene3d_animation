using System;
using OpenTK;

namespace scene3d_animation.Classes
{
    // Статический класс для создания анимации предопределенного вида
    static class Animation
    {
        // Класс функции анимации
        // Функция задает правило изменения параметра объекта (угол поворота, позицию или размер) при перерисовки сцены
        // Действует в промежутке времени от start до end
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

        // Функция плавно (анимированно) меняет текущее значение до заданного в течении времени от start до end
        public static AnimationFunc MoveTo(float x = 0, float y = 0, float z = 0, int start = 0, int end = int.MaxValue)
        {
            int duration = end - start;
            Vector3 v = new Vector3(x, y, z) / duration;
            return new AnimationFunc((t, curr) => curr * (1f - 1f/duration) + v, start, end);
        }

        // Функция плавно (анимированно) меняет текущее значение до заданного в течении времени от start до end
        public static AnimationFunc MoveTo(Vector3 v, int start = 0, int end = int.MaxValue)
        {
            int duration = end - start;
            v = v / duration;
            return new AnimationFunc((t, curr) => curr * (1f - 1f / duration) + v, start, end);
        }

        // Функция плавно (анимированно) меняет текущее значение на заданную величину в течении времени от start до end
        public static AnimationFunc MoveOn(float x = 0, float y = 0, float z = 0, int start = 0, int end = int.MaxValue)
        {
            int duration = end - start;
            Vector3 v = new Vector3(x, y, z) / duration;
            return new AnimationFunc((t, curr) => curr + v, start, end);
        }

        // Функция плавно (анимированно) меняет текущее значение на заданную величину в течении времени от start до end
        public static AnimationFunc MoveOn(Vector3 v, int start = 0, int end = int.MaxValue)
        {
            int duration = end - start;
            v = v / duration;
            return new AnimationFunc((t, curr) => curr + v, start, end);
        }

        // Функция меняет значение заданного параметра на указанную величину в каждый момент времени от start до end
        public static AnimationFunc TickInc(float x = 0, float y = 0, float z = 0, int start = 0, int end = int.MaxValue)
        {
            Vector3 v = new Vector3(x, y, z);
            return new AnimationFunc((t, curr) => curr + v, start, end);
        }

        // Функция меняет значение заданного параметра на указанную величину в каждый момент времени от start до end
        public static AnimationFunc TickInc(Vector3 v, int start = 0, int end = int.MaxValue)
        {
            return new AnimationFunc((t, curr) => curr + v, start, end);
        }
    }
}
