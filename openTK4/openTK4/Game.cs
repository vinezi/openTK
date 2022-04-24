using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace openTK4
{
    class Game : GameWindow
    {
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }
        private Shader shader;//шейдер

        private Texture texture1;
        private Texture texture2;

        private double time;

        int VertexBufferObject;
        int VertexArrayObject;
        private int ElementBufferObject;

        float[] verticesFouth =
        {
        //Position           Color              Texture     
        -0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,   0.0f, 0.0f, //Нижний левый угол
         0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,   1.0f, 0.0f,//// Нижний правый угол
         0.5f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f,   1.0f, 1.0f,  //Верхний угол 
         -0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 0.0f,   0.0f, 1.0f

        };

        private readonly uint[] indices =
          {
            0, 1, 3,
            1, 2, 3
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
            vboTextureColor();
        }



        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit); // ClearBufferMask.DepthBufferBit
            
            vaoTexture();
            shader.Use();
            //Matrix4 rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(90.0f)); //поворот вокраг оси Z

            time += 4.0 * e.Time;
            Matrix4 rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Convert.ToSingle(time)));
            Matrix4 scale = Matrix4.CreateScale(1.5f, 1.5f, 1.5f);                          //масштабирование
            Matrix4 translation = Matrix4.CreateTranslation(0.1f, 0.1f, 0.1f);              //перемещение
            Matrix4 transform = rotation * scale * translation;                                   //результирующая трансформация
            int location = GL.GetUniformLocation(shader.Handle, "transform");  //поиск Uniform параметра
            GL.UniformMatrix4(location, true, ref transform);                                   //передача результирующей матрицы в шейдер

            


            this.SwapBuffers();
        }

        private void vboTextureColor()
        {
            shader = new Shader("shaderTextureColor.vert", "shader_TextureColor.frag");

            VertexArrayObject = GL.GenVertexArray(); //генерация дескриптора VAO
            GL.BindVertexArray(VertexArrayObject); // //привязка дескриптора к буферу VAO    


            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            //GL.BufferData(BufferTarget.ArrayBuffer, size, data, BufferUsageHint.StaticDraw);
            GL.BufferData(BufferTarget.ArrayBuffer, verticesFouth.Length * sizeof(float), verticesFouth, BufferUsageHint.StaticDraw);

            //определение способа интерпретации атрибутов вершин - для VAO с координатами, цветами и текстурными координатами
            //вершинные координаты 
            int vertexLocation = shader.GetAttribLocation("aPosition");
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(vertexLocation);

            //цвета вершин
            int colorLocation = shader.GetAttribLocation("aColor");
            GL.VertexAttribPointer(colorLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(colorLocation);

            //текстурные координаты.  
            int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(texCoordLocation);




            //создаем объекты
            texture1 = new Texture();
            int textureId1 = texture1.LoadTexture("texture.png");
            shader.SetInt("texture1", 0);

            texture2 = new Texture();
            int textureId2 = texture2.LoadTexture("sun.png");
            shader.SetInt("texture2", 1);


            //GL.EnableVertexAttribArray(0);
        }

        private void vao()
        {

            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Quads, 0, 6);
            shader.Use();//check
            //this.SwapBuffers();
        }

        private void vaoTexture()
        {
            vao();
            GL.BindVertexArray(VertexArrayObject);

            texture1.Use(TextureUnit.Texture0);
            texture2.Use(TextureUnit.Texture1);

            //GL.DrawArrays(PrimitiveType.Quads, 0, 6);

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            //this.SwapBuffers();
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
            GL.DeleteBuffer(VertexArrayObject);
            shader.Dispose();
            base.OnUnload(e);
        }
    }
}
