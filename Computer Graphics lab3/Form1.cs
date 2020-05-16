using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
namespace Computer_Graphics_lab3
{
    

    public partial class Form1 : Form
    {
        View view = new View();
        bool loaded=false;
        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded)
                return;

            view.Rredraw();
            glControl1.SwapBuffers();
            GL.UseProgram(0);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            view.Start();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            glControl1.Update();
            
        }
    }
}
