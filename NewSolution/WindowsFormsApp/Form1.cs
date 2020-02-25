using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            IAsyncResult asyncResult = null;
            Action<string> action = this.DoSometingLong;
            asyncResult = action.BeginInvoke("btn",null,"开始");
            //asyncResult.AsyncWaitHandle.WaitOne();
            Console.WriteLine("结束了");
        }
        private void DoSometingLong(string str)
        {
            Console.WriteLine("begin");
            Thread.Sleep(1000);
            Console.WriteLine("end");
        }
    }
}
