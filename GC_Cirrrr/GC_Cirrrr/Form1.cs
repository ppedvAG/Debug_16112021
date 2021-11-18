using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GC_Cirrrr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var list = new List<A>(10000000);
            for (int i = 0; i < 10000000; i++)
            {
                var a = new A();
                var b = new B() { A = a };
                a.B = b;
                list.Add(a);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var list = new List<A>(10000);
            for (int i = 0; i < 10000; i++)
            {
                var a = new A();
                list.Add(a);
            }
        }

        int zahl = 12;

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                MachMehr();
            }
        }

        private void MachMehr()
        {
            Console.WriteLine(zahl);
            zahl += 1;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100000000; i++)
            {
                var xx = Math.Pow(i / 2, i);
            }
        }
    }

    class A
    {
        public B B { get; set; }
    }

    class B
    {
        public A A { get; set; }
    }
}
