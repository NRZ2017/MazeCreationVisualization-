using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeCreationVisualization
{
    public partial class Form1 : Form
    {
        Graphics gfx;
        Bitmap bitmap;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
            gfx = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Brushes.Black, 10);

            pictureBox1.Image = bitmap;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            gfx.Clear(Color.White);
            Pen pen = new Pen(Brushes.Black, 10);

            var temp = Graph<Point>.Maze((int)numericUpDown2.Value, (int)numericUpDown1.Value, (int)numericUpDown3.Value);
            var graph = temp.Item1;
            var walls = temp.Item2;

            int widthPerCell = pictureBox1.Width / (int)numericUpDown1.Value;
            int heightPerCell = pictureBox1.Height / (int)numericUpDown2.Value;
            
            foreach (var wall in walls)
            {

                //vertical or horizontal
                //if vertical find the x value by taking second's x value, use first and second y values

                if (wall.first.Value.X != wall.second.Value.X)//vertical
                {
                    gfx.DrawLine(pen, wall.second.Value.X * widthPerCell, wall.first.Value.Y * heightPerCell, wall.second.Value.X * widthPerCell, (wall.first.Value.Y + 1) * heightPerCell);
                }
                else//horizontal
                {
                    gfx.DrawLine(pen, wall.first.Value.X * widthPerCell, wall.second.Value.Y * heightPerCell, (wall.first.Value.X + 1) * widthPerCell, wall.second.Value.Y * heightPerCell);
                }
            }

            pictureBox1.Image = bitmap;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random gen = new Random();
            numericUpDown3.Value = gen.Next();
        }
    }
}
