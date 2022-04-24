using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace openTK3
{
    class Game : GameWindow
    {
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }
        private Shader shader;//шейдер
        private static int controller = 3;
        
        private Texture texture1;
        private Texture texture2;


        /// <summary>
        /// triangle
        /// </summary>

        int VertexBufferObject;
        int VertexArrayObject;
        private int ElementBufferObject;


        float[] verticesFir = {
            -0.5f, -0.5f, 0.0f, 
            0.5f, -0.5f, 0.0f,
            0.5f,  0.5f, 0.0f,
            -0.5f, 0.5f, 0.0f,
        };

        /// <summary>
        /// c0ordinates with color
        /// </summary>

        float[] verticesSec = { //координаты       //цвет
                    0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f, // Нижний правый угол красный
                   -0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f, // Нижний левый угол зеленый
                    -0.5f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f,  // Верхний угол синий
                    0.5f,  0.5f, 0.0f,  1.0f, 1.0f, 0.0f  // Верхний угол yellow
                    };


        /// <summary>
        /// /textiure
        /// </summary>
        float[] verticesThir = {
        //Position          Texture coordinates
         0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };

        private readonly uint[] _indices =
           {
            0, 1, 3,
            1, 2, 3
        };


        /// <summary>
        /// /Fouth texture with color
        /// </summary>
        float[] verticesFouth =
        {
        //Position           Color              Texture     
        -0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,   0.0f, 0.0f, //Нижний левый угол
         0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,   1.0f, 0.0f,//// Нижний правый угол
         0.5f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f,   1.0f, 1.0f,  //Верхний угол 
         -0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 0.0f,   0.0f, 1.0f

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

            switch (controller)
            {
                case 0:
                    vbo(verticesFir.Length * sizeof(float), verticesFir); //first
                    break;
                case 1:
                    vboCoorACol(); //second
                    break;
                case 2:
                    vboTexture(); //third
                        break;
                case 3:
                    vboTextureColor();
                    break;
                default:
                    break;
            }


            //vbo(verticesFir.Length * sizeof(float), verticesFir); //first
            //vboCoorACol(); //second
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit); // ClearBufferMask.DepthBufferBit
            vao();  //first & second
                    //int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");
                    //GL.Uniform4(vertexColorLocation, 0.0f, 1.0f, 0.0f, 1.0f); //not




            switch (controller)
            {
                case 0:
                    vao();  //first & second
                    break;
                case 1:
                    vao();  //first & second
                    break;
                case 2:
                    vaoTexture(); //Third & Fouth
                    break;
                case 3:
                    vaoTexture(); //Third & Fouth
                    break;
                default:
                    break;
            }


            this.SwapBuffers();
            //GL.End();
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
        private void vboCoorACol()
        {
            shader = new Shader("shaderColorAndCoord.vert", "shader_ColorAndCoord.frag");

            VertexArrayObject = GL.GenVertexArray(); //генерация дескриптора VAO
            GL.BindVertexArray(VertexArrayObject); // //привязка дескриптора к буферу VAO    



            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            //GL.BufferData(BufferTarget.ArrayBuffer, size, data, BufferUsageHint.StaticDraw);
            GL.BufferData(BufferTarget.ArrayBuffer, verticesSec.Length * sizeof(float), verticesSec, BufferUsageHint.StaticDraw);



            // Атрибут с координатами
            GL.VertexAttribPointer(shader.GetAttribLocation("position"), 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(shader.GetAttribLocation("position"));

            // Атрибут с цветом
            GL.VertexAttribPointer(shader.GetAttribLocation("color"), 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(shader.GetAttribLocation("color"));

            GL.EnableVertexAttribArray(0);
        }
        
        private void vboTexture()
        {
            shader = new Shader("shaderTexture.vert", "shader_Texture.frag");

            VertexArrayObject = GL.GenVertexArray(); //генерация дескриптора VAO
            GL.BindVertexArray(VertexArrayObject); // //привязка дескриптора к буферу VAO    


            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            //GL.BufferData(BufferTarget.ArrayBuffer, size, data, BufferUsageHint.StaticDraw);
            GL.BufferData(BufferTarget.ArrayBuffer, verticesThir.Length * sizeof(float), verticesThir, BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            shader.Use();




            //определение способа интерпретации атрибутов вершин - для VAO с координатами и текстурными координатами
            int vertexLocation = shader.GetAttribLocation("aPosition");
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(vertexLocation);

            int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(texCoordLocation);



            //создаем объекты
            texture1 = new Texture();
            int textureId1 = texture1.LoadTexture("texture.png");
            texture1.Use(TextureUnit.Texture0);

            texture2 = new Texture();
            int textureId2 = texture2.LoadTexture("sun.png");
            texture1.Use(TextureUnit.Texture1);


            shader.SetInt("texture1", 0);
            shader.SetInt("texture2", 1);


            //GL.EnableVertexAttribArray(0);
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
            GL.BindVertexArray(VertexArrayObject);

            texture1.Use(TextureUnit.Texture0);
            texture2.Use(TextureUnit.Texture1);

            //GL.DrawArrays(PrimitiveType.Quads, 0, 6);

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
           
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
