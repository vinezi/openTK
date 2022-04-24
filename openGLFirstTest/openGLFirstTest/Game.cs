using System;
using System.Drawing;
using System.Threading;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace openGLFirstTest
{
    class Game : GameWindow
    {
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        private static int FigureNumber = 10;
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }


            if (input.IsKeyDown(Key.Space))
            {
                if (FigureNumber != 9)
                    FigureNumber++;
                else
                    FigureNumber = 0;

                Thread.Sleep(200);
            }
            base.OnUpdateFrame(e);
        }
        protected override void OnLoad(EventArgs e)
        {

            GL.ClearColor(Color.DarkBlue);//определение цвета фона

            base.OnLoad(e);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            switch (FigureNumber)
            {
                case 0:
                    Lines(0.5);
                    break;
                case 1:
                    LineStrip(0.5);
                    break;
                case 2:
                    LineLoop(0.5);
                    break;
                case 3:
                    Polygon(0.5);
                    break;
                case 4:
                    Triangles(0.5);
                    break;
                case 5:
                    TriangleStrip(0.5);
                    break;
                case 6:
                    Quads(0.5);
                    break;
                case 7:
                    QuadStrip(0.5);
                    break;
                case 8:
                    TriangleFan(0.5);
                    break;
                default:

                    Lines(0.5);
                    LineStrip(0.5);
                    LineLoop(0.5);
                    Polygon(0.5);
                    Triangles(0.5);
                    TriangleStrip(0.5);
                    Quads(0.5);
                    QuadStrip(0.5);
                    TriangleFan(0.5);
                    break;
            }
            this.SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        void Lines(double r)
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(-0.90, 0.90, 0);
            GL.Vertex3(-0.90, 0.70, 0);
            GL.End();
        }
        void LineStrip(double r)
        {
            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex3(-0.8, 0.7, 0);
            GL.Vertex3(-0.7, 0.9, 0);
            GL.Vertex3(-0.6, 0.7, 0);
            GL.End();
        }
        void LineLoop(double r)
        {
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex3(-0.5, 0.7, 0);
            GL.Vertex3(-0.4, 0.9, 0);
            GL.Vertex3(-0.3, 0.7, 0);
            GL.End();
        }
        void Polygon(double r)
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Vertex3(-0.2, 0.7, 0);
            GL.Vertex3(-0.2, 0.9, 0);
            GL.Vertex3(-0.1, 0.9, 0);
            GL.Vertex3(0.0, 0.8, 0);
            GL.Vertex3(-0.1, 0.4, 0);
            GL.End();
        }
        void Triangles(double r)
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex3(0.1, 0.7, 0);
            GL.Vertex3(0.2, 0.9, 0);
            GL.Vertex3(0.1, 0.9, 0);
            GL.End();
        }
        void TriangleStrip(double r)
        {
            GL.Begin(PrimitiveType.TriangleStrip);
            GL.Vertex3(0.3, 0.7, 0);
            GL.Vertex3(0.3, 0.9, 0);
            GL.Vertex3(0.4, 0.9, 0);
            GL.Vertex3(0.4, 0.8, 0);
            GL.End();

        }
        void Quads(double r)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(-0.9, 0.6, 0);
            GL.Vertex3(-0.7, 0.6, 0);
            GL.Vertex3(-0.7, 0.4, 0);
            GL.Vertex3(-0.9, 0.4, 0);
            GL.End();
        }
        void QuadStrip(double r)
        {
            GL.Begin(PrimitiveType.QuadStrip);
            GL.Vertex3(-0.5, 0.2, 0);
            GL.Vertex3(-0.5, 0.3, 0);
            GL.Vertex3(-0.4, 0.2, 0);
            GL.Vertex3(-0.4, 0.3, 0);

            GL.Vertex3(-0.3, 0.1, 0);
            GL.Vertex3(-0.3, 0.4, 0);

            GL.Vertex3(-0.1, 0.05, 0);
            GL.Vertex3(-0.1, 0.35, 0);
            GL.End();
        }

        void TriangleFan(double r)
        {
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Vertex3(0.5, -0.5, 0);
            GL.Vertex3(0.55, -0.4, 0);
            GL.Vertex3(0.6, -0.45, 0);


            GL.Vertex3(0.61, -0.5, 0);

            GL.Vertex3(0.60, -0.55, 0);

            GL.End();
        }


    }
}
