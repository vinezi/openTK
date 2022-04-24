using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace openTK2
{
    class Game : GameWindow
    {
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }
        private Shader shader;//шейдер

        private static int eboCount = 0;

        int VertexBufferObject;
        int VertexArrayObject;  //дескриптор VAO  
        int ElementBufferObject; //дескриптор EAO

        /// <summary>
        /// triangle with vao
        /// </summary>
        float[] verticesTriangle = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f  //Top vertex
        };

        /// <summary>
        /// square with ebo
        /// </summary>
        float[] verticesSquare = {
         0.5f,  0.5f, 0.0f,  // Верхний правый угол
         0.5f, -0.5f, 0.0f,  // Нижний правый угол
        -0.5f, -0.5f, 0.0f,  // Нижний левый угол
        -0.5f,  0.5f, 0.0f   // Верхний левый угол
        };

        uint[] indicesSquare = {  // Помните, что нумерация начинается с 0!
        0, 1, 3,   // Первый треугольник
        1, 2, 3    // Второй треугольник
        };

        /// <summary>
        /// lab task
        /// </summary>
        float[] verticesAll = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f,  //Top vertex
            -0.5f, 0.25f, 0.0f, //Bottom-left vertex
            0.5f, 0.25f, 0.0f, //Bottom-right vertex
            0.0f,  -0.75f, 0.0f  //Top vertex
        };


        uint[] indices1 = {  // Помните, что нумерация начинается с 0!
        0, 1, 4,   // Первый треугольник
        0, 1, 3// 4 треугольник        
        };

        uint[] indices2 = {  // Помните, что нумерация начинается с 0!  //star
        0, 1, 2,   // Первый треугольник
        3, 4, 5    // Второй треугольник
        };

        uint[] indices3 = {  // Помните, что нумерация начинается с 0! //last
        0, 1, 2,   // Первый треугольник
        3, 4, 5, // Второй треугольник
        0, 1, 3,// третий треугольник
        0, 1, 4, // 4 треугольник        
        };

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            base.OnUpdateFrame(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.White);
            //vbo(verticesTriangle.Length * sizeof(float), verticesTriangle); //for vao
            //ebo0();
            //ebo1();
            ebo2();
            //ebo3();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); // ClearBufferMask.DepthBufferBit
            //vao();
            eboStart(eboCount);
            base.OnRenderFrame(e);
        }

        private void vbo(int size, float[] data)
        {
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, size, data, BufferUsageHint.StaticDraw);

            VertexArrayObject = GL.GenVertexArray(); //генерация дескриптора VAO
            GL.BindVertexArray(VertexArrayObject); // //привязка дескриптора к буферу VAO    

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            shader = new Shader("shader.vert", "shader_blue.frag");
            shader.Use();
        }

        private void vao()
        {
            //использовать шейдер
            shader.Use();
            // Связываем VAO
            GL.BindVertexArray(VertexArrayObject);
            //вывод массива
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            this.SwapBuffers();
        }

        private void ebo0()
        {

            vbo(verticesSquare.Length * sizeof(float), verticesSquare);
            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indicesSquare.Length * sizeof(uint), indicesSquare, BufferUsageHint.StaticDraw);
            eboCount = indicesSquare.Length;
        }

        private void ebo1()
        {

            vbo(verticesAll.Length * sizeof(float), verticesAll);
            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices1.Length * sizeof(uint), indices1, BufferUsageHint.StaticDraw);
            eboCount = indices1.Length;
        }

        private void ebo2()
        {

            vbo(verticesAll.Length * sizeof(float), verticesAll);
            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices2.Length * sizeof(uint), indices2, BufferUsageHint.StaticDraw);
            eboCount = indices2.Length;
        }
        
        private void ebo3()
        {

            vbo(verticesAll.Length * sizeof(float), verticesAll);
            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices3.Length * sizeof(uint), indices3, BufferUsageHint.StaticDraw);
            eboCount = indices3.Length;
        }


        private void eboStart(int count)
        {
            //использовать шейдер
            shader.Use();
            // Связываем VAO
            GL.BindVertexArray(VertexArrayObject);

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);//отрисовка контурами

            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3); //так было
            GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, 0); // так стало

            this.SwapBuffers();
        }


        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            shader.Dispose();
            base.OnUnload(e);
        }
    }
}
