using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace MAD_MAN_Ping_Pong
{
    

    public partial class Form2 : Form
    {
   
        public Form2()
        {
            InitializeComponent();
           
        }



        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Form1 fr1 = new Form1();

            //відкриваємо форму
            fr1.Show();
            //this.Hide();
            this.Close();
         
        }
       

       private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
          
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
            //this.Close();
        }




        private void pictureBox2_Click(object sender, EventArgs e)
        {
   
        }
        private void button2_Click(object sender, EventArgs e)
        {
       

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
    
        }
         private void button1_Click_1(object sender, EventArgs e)
        {

        }

      
    }
    
}
