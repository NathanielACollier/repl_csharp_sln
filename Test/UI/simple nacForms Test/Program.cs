using System;

namespace simple_nacForms_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = nac.Forms.Form.NewForm();

            f.Text("Hello World!");
            f.Display();
        }
    }
}
