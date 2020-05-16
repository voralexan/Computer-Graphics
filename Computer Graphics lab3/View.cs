using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace Computer_Graphics_lab3
{


    class View
    {
        int vbo_position;
        int BasicProgramID = 0;
        uint BasicVertexShader ;
        uint BasicFragmentShader;
        Vector3 campos = new Vector3(0, 0, 0);
        int attribute_vpos = 0;
        int uniform_pos = 0;
        int uniform_aspect = 0;
        int aspect = 0;



        Vector3 BigSphere = new Vector3(0.0f, 1.0f, 0.0f);     
        Vector3 ColorBigSphere = new Vector3(0.0f, 1.0f, 0.0f);   
        Vector3 SmallSphere = new Vector3(0.0f, 1.0f, 0.0f);   
        Vector3 ColorSmallSphere = new Vector3(0.0f, 0.1f, 0.45f); 



        public void Start()
        {
            InitShaders();
            SetBufferObjects();
        }
            public void Rredraw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.Texture2D);
            GL.EnableVertexAttribArray(attribute_vpos);
            GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            GL.DisableVertexAttribArray(attribute_vpos);
        }
        public void InitShaders()
        {
            BasicProgramID = GL.CreateProgram();  // создание объекта программы 
            loadShader("..\\..\\raytracing.vert.txt", ShaderType.VertexShader, (uint)  BasicProgramID,    out BasicVertexShader);
            loadShader("..\\..\\raytracing.frag.txt", ShaderType.FragmentShader, (uint) BasicProgramID,    out BasicFragmentShader); 
            GL.LinkProgram(BasicProgramID); // Проверяем успех компоновки 
            int status = 0; GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status); 
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));
        }
        void loadShader(String filename, ShaderType type, uint program, out uint address)
        {

            address = (uint)GL.CreateShader(type); 

            using (System.IO.StreamReader sr = new StreamReader(filename)) { GL.ShaderSource((int)address, sr.ReadToEnd()); }

            GL.CompileShader(address); 
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog((int)address));
        }

       public void SetBufferObjects()
        {
            Vector3[] vertdata = new Vector3[] { new Vector3(-1f, -1f, 0f), new Vector3(1f, -1f, 0f), new Vector3(1f, 1f, 0f), new Vector3(-1f, 1f, 0f) };

            GL.GenBuffers(1, out vbo_position);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position); GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw); 
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.Uniform3(uniform_pos, ref campos); 
            GL.Uniform1(uniform_aspect, aspect);

            GL.UseProgram(BasicProgramID);

            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "BigSphere"), ref BigSphere);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColBigSphere"), ref ColorBigSphere);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "SmallSphere"), ref SmallSphere);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColSmallSphere"), ref ColorSmallSphere);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }





    }
}
