using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace openTK3
{
    class Texture
    {
        int texHandle;
        public Texture()
        {
            texHandle = GL.GenTexture();
        }

        public void Use(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, texHandle);
        }


        // загрузка текстуры из файла
        public int LoadTexture(string filename)
        {
            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException(filename);

            texHandle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texHandle);

            // Мы не будем загружать мип, поэтому отключаем мипмаппинг (иначе текстуры 		не появится).
            // Мы можем использовать GL.GenerateMipmaps () или GL.Ext.GenerateMipmaps 		() для создания
            // Мипмапов автоматически.  В этом случае используйте 				     TextureMinFilter.LinearMipmapLinear чтобы включить их.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Bitmap bmp = new Bitmap(filename);
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            return texHandle;
        }


    }
}
