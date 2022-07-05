using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DDA_Line
{
    public partial class Form1 : Form
    {
        private Graphics g1;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // drawing line using DDA
        {
            int k = 0, steps;
            int x1 = Convert.ToInt32(textBox2.Text);
            int y1 = Convert.ToInt32(textBox1.Text);

            int x2 = Convert.ToInt32(textBox3.Text);
            int y2 = Convert.ToInt32(textBox4.Text);
            float xinc, yinc, x0 = x1, y0 = y1;
            var abrush = Brushes.Red;
            var g = pictureBox1.CreateGraphics();

            int dY = y2 - y1;
            int dX = x2 - x1;
            if (Math.Abs(dX) > Math.Abs(dY))
                steps = Math.Abs(dX);
            else
                steps = Math.Abs(dY);
            xinc = (float)dX / (float)steps;
            yinc = (float)dY / (float)steps;
           g.FillRectangle(abrush, (int)Math.Round(x0) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (int)Math.Round(y0), 2, 2);

            for (int i = 0; i < steps; i++)
            {
                x0 += xinc;
                y0 += yinc;
                g.FillRectangle(abrush, (int)Math.Round(x0)+ (pictureBox1.Size.Width/ 2), (pictureBox1.Size.Height / 2) - (int)Math.Round(y0), 2, 2);
            }
        }

        private void button2_Click(object sender, EventArgs e) //drawing circle
        {
            int xCenter = Convert.ToInt32(textBox5.Text);
            int yCenter = Convert.ToInt32(textBox6.Text);
            int Radius = Convert.ToInt32(textBox7.Text);
            int p = 1 - Radius, k = 1;
            var g = pictureBox1.CreateGraphics();
            var abrush = Brushes.Black;
            g.FillRectangle(abrush, xCenter + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - Radius - yCenter, 2, 2);
            while(k <= Radius)
            {
                if(p < 0)
                {
                    g.FillRectangle(abrush, xCenter + k + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - Radius - yCenter, 2, 2);
                    octants(k, Radius, xCenter, yCenter);
                    p = p + (2 * k) + 1;
                }
                    
                else if(p > 0)
                {
                    Radius--;
                    g.FillRectangle(abrush,xCenter+ k+ (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2)- Radius-yCenter, 2, 2);
                    octants(k, Radius, xCenter, yCenter);
                    p = p + (2 * k) + 1 - (2 * Radius);
                }
                k++;
            }
        }
        void octants(int k, int Radius, int xc, int yc)
        {
            var abrush = Brushes.Black;
            pictureBox1.CreateGraphics().FillRectangle(abrush, xc + k + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - Radius - yc, 2, 2);
            pictureBox1.CreateGraphics().FillRectangle(abrush, xc + -k + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - Radius - yc, 2, 2);
            pictureBox1.CreateGraphics().FillRectangle(abrush, xc + k + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - -Radius - yc, 2, 2);
            pictureBox1.CreateGraphics().FillRectangle(abrush, xc + -k + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - -Radius - yc, 2, 2);
            pictureBox1.CreateGraphics().FillRectangle(abrush, xc + Radius + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - k - yc, 2, 2);
            pictureBox1.CreateGraphics().FillRectangle(abrush, xc + -Radius + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - k - yc, 2, 2);
            pictureBox1.CreateGraphics().FillRectangle(abrush, xc + Radius + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - -k - yc, 2, 2);
            pictureBox1.CreateGraphics().FillRectangle(abrush, xc + -Radius + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - -k - yc, 2, 2);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) //drawing x-axis and y-axis
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.White);
            g.DrawLine(p, pictureBox1.Size.Width / 2, 0, pictureBox1.Size.Width / 2, pictureBox1.Height);
            g.DrawLine(p, 0, pictureBox1.Size.Height / 2, pictureBox1.Size.Width, pictureBox1.Size.Height / 2);

        }

        private void button3_Click_1(object sender, EventArgs e) // drawing line using Bresenham algorithm
        {
            var g = pictureBox1.CreateGraphics();
            var abrush = Brushes.Black;

            int x1 = Convert.ToInt32(textBox8.Text);
            int y1 = Convert.ToInt32(textBox9.Text);
            int x2 = Convert.ToInt32(textBox10.Text);
            int y2 = Convert.ToInt32(textBox11.Text);

            float slope = ((float)y2 - (float)y1) / ((float)x2 - (float)x1);
            int x = x1, y = y1, dx = x2 - x1, dy = y2 - y1;
            int p = 2 * dy - dx;
            int c1 = 2 * dy, c2 = 2 * (dy - dx);
            //int[] myarray = new int[(Math.Abs(x2 - x)) * 2];
            //int check = Math.Abs(x-x2);
            g.FillRectangle(abrush, x + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y, 2, 2);
            //int i = 0;
            //for(int z = 0; z < check; z++)

            if(slope > 0 && slope < 1)
            {
                while (x < x2)
                {
                    if (p < 0)
                    {
                        p += c1;
                    }

                    else
                    {
                        p += c2;
                        y += 1;
                    }
                    x++;
                    /*myarray[i] = x;
                    myarray[i + 1] = y;
                    i += 2;*/

                    g.FillRectangle(abrush, x + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Width / 2) - y, 2, 2);

                }
            }

            /*for(int z = 0; z < myarray.Length; z+=2)
            {
                g.FillRectangle(abrush, myarray[z] + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - myarray[z+1], 2, 2);
                g.FillRectangle(abrush, -myarray[z] + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - myarray[z + 1], 2, 2);
                g.FillRectangle(abrush, myarray[z] + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - -myarray[z + 1], 2, 2);
                g.FillRectangle(abrush, -myarray[z] + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - -myarray[z + 1], 2, 2);
                g.FillRectangle(abrush, myarray[z+1] + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - myarray[z], 2, 2);
                g.FillRectangle(abrush, -myarray[z+1] + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - myarray[z], 2, 2);
                g.FillRectangle(abrush, myarray[z+1] + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - -myarray[z], 2, 2);
                g.FillRectangle(abrush, -myarray[z+1] + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - -myarray[z], 2, 2);
            }*/


        }

        private void button4_Click(object sender, EventArgs e)  // Drawing Ellipse
        {

            int xc = Convert.ToInt32(textBox12.Text);
            int yc = Convert.ToInt32(textBox13.Text);
            int Rx = Convert.ToInt32(textBox14.Text);
            int Ry = Convert.ToInt32(textBox15.Text);

            int Rx2 = Rx * Rx;
            int Ry2 = Ry * Ry;
            int twoRx2 = 2 * Rx2;
            int twoRy2 = 2 * Ry2;
            int p;

            int x = 0;
            int y = Ry;
            int px = 0;
            int py = twoRx2 * y;
            Ellipse_Octants(xc, yc, x, y);


            /* Region 1 */

            p = ((int)(Ry2 - (Rx2 * Ry) + (0.25 * Rx2)));

            while (px < py)
            {
                x++;
                px += twoRy2;
                if (p < 0)
                    p += Ry2 + px;
                else
                {
                    y--;
                    py -= twoRx2;
                    p += Ry2 + px - py;
                }
                Ellipse_Octants(xc, yc, x, y);
            }

            /* Region 2 */

            p = ((int)(Ry2 * (x + 0.5) * (x + 0.5) + Rx2 * (y - 1) * (y - 1) - Rx2 * Ry2));
            while (y > 0)
            {
                y--;
                py -= twoRx2;
                if (p > 0)
                    p += Rx2 - py;
                else
                {
                    x++;
                    px += twoRy2;
                    p += Rx2 - py + px;
                }

                Ellipse_Octants(xc, yc, x, y);
            }
            void Ellipse_Octants(int xc, int yc, int x, int y)
            {
                var abrush = Brushes.Red;
                pictureBox1.CreateGraphics().FillRectangle(abrush, xc + x + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - yc + y, 3, 3);
                pictureBox1.CreateGraphics().FillRectangle(abrush, xc - x + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - yc + y, 3, 3);
                pictureBox1.CreateGraphics().FillRectangle(abrush, xc + x + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - yc - y, 3, 3);
                pictureBox1.CreateGraphics().FillRectangle(abrush, xc - x + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - yc - y, 3, 3);


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button6_Click(object sender, EventArgs e)   // 2D Draw Rectangle
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);

            Pen pen = new Pen(Color.Yellow , 3);

            Point p1 = new Point(x1 + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y1);
            Point p2 = new Point(x2 + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y2);
            Point p3 = new Point(x3 + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y3);
            Point p4 = new Point(x4 + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y4);

            pictureBox1.CreateGraphics().DrawLine(pen, p1, p2);
            pictureBox1.CreateGraphics().DrawLine(pen, p2, p3);
            pictureBox1.CreateGraphics().DrawLine(pen, p3, p4);
            pictureBox1.CreateGraphics().DrawLine(pen, p4, p1);



        }

        private void button7_Click(object sender, EventArgs e)  // 2D Translation 
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);


            int TX = Convert.ToInt32(textBox24.Text);
            int TY = Convert.ToInt32(textBox25.Text);

            Pen pen = new Pen(Color.Red, 3);


            Point p1 = new Point(x1 + TX + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (y1 + TY));
            Point p2 = new Point(x2 + TX + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (y2 + TY));
            Point p3 = new Point(x3 + TX + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (y3 + TY));
            Point p4 = new Point(x4 + TX + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (y4 + TY));

            pictureBox1.CreateGraphics().DrawLine(pen, p1, p2);
            pictureBox1.CreateGraphics().DrawLine(pen, p2, p3);
            pictureBox1.CreateGraphics().DrawLine(pen, p3, p4);
            pictureBox1.CreateGraphics().DrawLine(pen, p4, p1);




        }

        private void button8_Click(object sender, EventArgs e)  // 2D Scaling 
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);


            int SX = Convert.ToInt32(textBox24.Text);
            int SY = Convert.ToInt32(textBox25.Text);

            Pen pen = new Pen(Color.Red, 3);

            Point p1 = new Point((x1 * SX) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (y1 * SY));
            Point p2 = new Point((x2 * SX) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (y2 * SY));
            Point p3 = new Point((x3 * SX) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (y3 * SY));
            Point p4 = new Point((x4 * SX) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (y4 * SY));

            pictureBox1.CreateGraphics().DrawLine(pen, p1, p2);
            pictureBox1.CreateGraphics().DrawLine(pen, p2, p3);
            pictureBox1.CreateGraphics().DrawLine(pen, p3, p4);
            pictureBox1.CreateGraphics().DrawLine(pen, p4, p1);
        }

        private void button10_Click(object sender, EventArgs e) // 2D Rotation
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);


            Pen pen = new Pen(Color.Red, 3);

            double angle = Convert.ToInt32(textBox26.Text);

            double sine = Math.Sin((angle * (Math.PI)) / 180);
            double cosine = Math.Cos((angle * (Math.PI)) / 180);


            Point p1 = new Point(Convert.ToInt32((x1 * cosine) - (y1 * sine)) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (Convert.ToInt32((x1 * sine) + (y1 * cosine))));
            Point p2 = new Point(Convert.ToInt32((x2 * cosine) - (y2 * sine)) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (Convert.ToInt32((x2 * sine) + (y2 * cosine))));
            Point p3 = new Point(Convert.ToInt32((x3 * cosine) - (y3 * sine)) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (Convert.ToInt32((x3 * sine) + (y3 * cosine))));
            Point p4 = new Point(Convert.ToInt32((x4 * cosine) - (y4 * sine)) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (Convert.ToInt32((x4 * sine) + (y4 * cosine))));


            pictureBox1.CreateGraphics().DrawLine(pen, p1, p2);
            pictureBox1.CreateGraphics().DrawLine(pen, p2, p3);
            pictureBox1.CreateGraphics().DrawLine(pen, p3, p4);
            pictureBox1.CreateGraphics().DrawLine(pen, p4, p1);

        }

        private void button11_Click(object sender, EventArgs e) // 2D Shearing in X
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);

            Pen pen = new Pen(Color.Red, 3);


            int ShX = Convert.ToInt32(textBox27.Text);

            Point p1 = new Point((x1 + (ShX * y1)) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y1);
            Point p2 = new Point((x2 + (ShX * y2)) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y2);
            Point p3 = new Point((x3 + (ShX * y3)) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y3);
            Point p4 = new Point((x4 + (ShX * y4)) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y4);

            pictureBox1.CreateGraphics().DrawLine(pen, p1, p2);
            pictureBox1.CreateGraphics().DrawLine(pen, p2, p3);
            pictureBox1.CreateGraphics().DrawLine(pen, p3, p4);
            pictureBox1.CreateGraphics().DrawLine(pen, p4, p1);
        }

        private void button12_Click(object sender, EventArgs e)  // 2D Shearing in Y
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);

            Pen pen = new Pen(Color.Red, 3);


            int ShY = Convert.ToInt32(textBox27.Text);

            Point p1 = new Point(x1 + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - ((ShY * x1) + y1));
            Point p2 = new Point(x2 + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - ((ShY * x2) + y2));
            Point p3 = new Point(x3 + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - ((ShY * x3) + y3));
            Point p4 = new Point(x4 + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - ((ShY * x4) + y4));



            pictureBox1.CreateGraphics().DrawLine(pen, p1, p2);
            pictureBox1.CreateGraphics().DrawLine(pen, p2, p3);
            pictureBox1.CreateGraphics().DrawLine(pen, p3, p4);
            pictureBox1.CreateGraphics().DrawLine(pen, p4, p1);
        }

        private void button13_Click(object sender, EventArgs e)  // 3D Translation
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int z1 = Convert.ToInt32(textBox30.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int z2 = Convert.ToInt32(textBox31.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int z3 = Convert.ToInt32(textBox33.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);
            int z4 = Convert.ToInt32(textBox32.Text);

            int TX = Convert.ToInt32(textBox24.Text);
            int TY = Convert.ToInt32(textBox25.Text);
            int TZ = Convert.ToInt32(textBox29.Text);

            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();

            string n_x1 = Convert.ToString(x1 + TX);
            string n_y1 = Convert.ToString(y1 + TY);
            string n_z1 = Convert.ToString(z1 + TZ);

            textBox34.Text += "(" + n_x1 + " , " + n_y1 + " , " + n_z1 + ")";

            string n_x2 = Convert.ToString(x2 + TX);
            string n_y2 = Convert.ToString(y2 + TY);
            string n_z2 = Convert.ToString(z2 + TZ);

            textBox35.Text += "(" + n_x2 + " , " + n_y2 + " , " + n_z2 + ")";

            string n_x3 = Convert.ToString(x3 + TX);
            string n_y3 = Convert.ToString(y3 + TY);
            string n_z3 = Convert.ToString(z3 + TZ);

            textBox37.Text += "(" + n_x3 + " , " + n_y3 + " , " + n_z3 + ")";

            string n_x4 = Convert.ToString(x4 + TX);
            string n_y4 = Convert.ToString(y4 + TY);
            string n_z4 = Convert.ToString(z4 + TZ);

            textBox36.Text += "(" + n_x4 + " , " + n_y4 + " , " + n_z4 + ")";

        }

        private void button14_Click(object sender, EventArgs e)  // 3D Scaling
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int z1 = Convert.ToInt32(textBox30.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int z2 = Convert.ToInt32(textBox31.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int z3 = Convert.ToInt32(textBox33.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);
            int z4 = Convert.ToInt32(textBox32.Text);

            int SX = Convert.ToInt32(textBox24.Text);
            int SY = Convert.ToInt32(textBox25.Text);
            int SZ = Convert.ToInt32(textBox29.Text);

            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();

            string n_x1 = Convert.ToString(x1 * SX);
            string n_y1 = Convert.ToString(y1 * SY);
            string n_z1 = Convert.ToString(z1 * SZ);

            textBox34.Text += "(" + n_x1 + " , " + n_y1 + " , " + n_z1 + ")";

            string n_x2 = Convert.ToString(x2 * SX);
            string n_y2 = Convert.ToString(y2 * SY);
            string n_z2 = Convert.ToString(z2 * SZ);

            textBox35.Text += "(" + n_x2 + " , " + n_y2 + " , " + n_z2 + ")";

            string n_x3 = Convert.ToString(x3 * SX);
            string n_y3 = Convert.ToString(y3 * SY);
            string n_z3 = Convert.ToString(z3 * SZ);

            textBox37.Text += "(" + n_x3 + " , " + n_y3 + " , " + n_z3 + ")";

            string n_x4 = Convert.ToString(x4 * SX);
            string n_y4 = Convert.ToString(y4 * SY);
            string n_z4 = Convert.ToString(z4 * SZ);

            textBox36.Text += "(" + n_x4 + " , " + n_y4 + " , " + n_z4 + ")";
        }

        private void button15_Click(object sender, EventArgs e) // 3D Reflection Over XY
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int z1 = Convert.ToInt32(textBox30.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int z2 = Convert.ToInt32(textBox31.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int z3 = Convert.ToInt32(textBox33.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);
            int z4 = Convert.ToInt32(textBox32.Text);


            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();

            string n_x1 = Convert.ToString(x1);
            string n_y1 = Convert.ToString(y1);
            string n_z1 = Convert.ToString(z1 * -1);

            textBox34.Text += "(" + n_x1 + " , " + n_y1 + " , " + n_z1 + ")";

            string n_x2 = Convert.ToString(x2);
            string n_y2 = Convert.ToString(y2);
            string n_z2 = Convert.ToString(z2 * -1);

            textBox35.Text += "(" + n_x2 + " , " + n_y2 + " , " + n_z2 + ")";

            string n_x3 = Convert.ToString(x3);
            string n_y3 = Convert.ToString(y3);
            string n_z3 = Convert.ToString(z3 * -1);

            textBox37.Text += "(" + n_x3 + " , " + n_y3 + " , " + n_z3 + ")";

            string n_x4 = Convert.ToString(x4);
            string n_y4 = Convert.ToString(y4);
            string n_z4 = Convert.ToString(z4 * -1);

            textBox36.Text += "(" + n_x4 + " , " + n_y4 + " , " + n_z4 + ")";
        }

        private void button16_Click(object sender, EventArgs e)  // 3D Reflection Over XZ
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int z1 = Convert.ToInt32(textBox30.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int z2 = Convert.ToInt32(textBox31.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int z3 = Convert.ToInt32(textBox33.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);
            int z4 = Convert.ToInt32(textBox32.Text);


            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();

            string n_x1 = Convert.ToString(x1);
            string n_y1 = Convert.ToString(y1 * -1);
            string n_z1 = Convert.ToString(z1);

            textBox34.Text += "(" + n_x1 + " , " + n_y1 + " , " + n_z1 + ")";

            string n_x2 = Convert.ToString(x2);
            string n_y2 = Convert.ToString(y2 * -1);
            string n_z2 = Convert.ToString(z2);

            textBox35.Text += "(" + n_x2 + " , " + n_y2 + " , " + n_z2 + ")";

            string n_x3 = Convert.ToString(x3);
            string n_y3 = Convert.ToString(y3 * -1);
            string n_z3 = Convert.ToString(z3);

            textBox37.Text += "(" + n_x3 + " , " + n_y3 + " , " + n_z3 + ")";

            string n_x4 = Convert.ToString(x4);
            string n_y4 = Convert.ToString(y4 * -1);
            string n_z4 = Convert.ToString(z4);

            textBox36.Text += "(" + n_x4 + " , " + n_y4 + " , " + n_z4 + ")";
        }

        private void button17_Click(object sender, EventArgs e)  // 3D Reflection Over YZ
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int z1 = Convert.ToInt32(textBox30.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int z2 = Convert.ToInt32(textBox31.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int z3 = Convert.ToInt32(textBox33.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);
            int z4 = Convert.ToInt32(textBox32.Text);


            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();

            string n_x1 = Convert.ToString(x1 * -1);
            string n_y1 = Convert.ToString(y1);
            string n_z1 = Convert.ToString(z1);

            textBox34.Text += "(" + n_x1 + " , " + n_y1 + " , " + n_z1 + ")";

            string n_x2 = Convert.ToString(x2 * -1);
            string n_y2 = Convert.ToString(y2);
            string n_z2 = Convert.ToString(z2);

            textBox35.Text += "(" + n_x2 + " , " + n_y2 + " , " + n_z2 + ")";

            string n_x3 = Convert.ToString(x3 * -1);
            string n_y3 = Convert.ToString(y3);
            string n_z3 = Convert.ToString(z3);

            textBox37.Text += "(" + n_x3 + " , " + n_y3 + " , " + n_z3 + ")";

            string n_x4 = Convert.ToString(x4 * -1);
            string n_y4 = Convert.ToString(y4);
            string n_z4 = Convert.ToString(z4);

            textBox36.Text += "(" + n_x4 + " , " + n_y4 + " , " + n_z4 + ")";
        }

        private void button18_Click(object sender, EventArgs e)  // 3D Rotation Around X
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int z1 = Convert.ToInt32(textBox30.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int z2 = Convert.ToInt32(textBox31.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int z3 = Convert.ToInt32(textBox33.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);
            int z4 = Convert.ToInt32(textBox32.Text);


            double angle = Convert.ToInt32(textBox26.Text);

            double sine = Math.Sin((angle * (Math.PI)) / 180);
            double cosine = Math.Cos((angle * (Math.PI)) / 180);

            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();


            string n_x1 = Convert.ToString(x1);
            string n_y1 = Convert.ToString((y1 * cosine) - (z1 * sine));
            string n_z1 = Convert.ToString((y1 * sine) + (z1 * cosine));

            textBox34.Text += "(" + n_x1 + " , " + n_y1 + " , " + n_z1 + ")";

            string n_x2 = Convert.ToString(x2);
            string n_y2 = Convert.ToString((y2 * cosine) - (z2 * sine));
            string n_z2 = Convert.ToString((y2 * sine) + (z2 * cosine));

            textBox35.Text += "(" + n_x2 + " , " + n_y2 + " , " + n_z2 + ")";

            string n_x3 = Convert.ToString(x3);
            string n_y3 = Convert.ToString((y3 * cosine) - (z3 * sine));
            string n_z3 = Convert.ToString((y3 * sine) + (z3 * cosine));

            textBox37.Text += "(" + n_x3 + " , " + n_y3 + " , " + n_z3 + ")";

            string n_x4 = Convert.ToString(x4);
            string n_y4 = Convert.ToString((y4 * cosine) - (z4 * sine));
            string n_z4 = Convert.ToString((y4 * sine) + (z4 * cosine));

            textBox36.Text += "(" + n_x4 + " , " + n_y4 + " , " + n_z4 + ")";

        }

        private void button19_Click(object sender, EventArgs e)  // 3D Rotation Around Y
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int z1 = Convert.ToInt32(textBox30.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int z2 = Convert.ToInt32(textBox31.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int z3 = Convert.ToInt32(textBox33.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);
            int z4 = Convert.ToInt32(textBox32.Text);


            double angle = Convert.ToInt32(textBox26.Text);

            double sine = Math.Sin((angle * (Math.PI)) / 180);
            double cosine = Math.Cos((angle * (Math.PI)) / 180);

            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();


            string n_x1 = Convert.ToString((z1 * sine) + (x1 * cosine));
            string n_y1 = Convert.ToString(y1);
            string n_z1 = Convert.ToString((y1 * cosine) - (x1 * sine));

            textBox34.Text += "(" + n_x1 + " , " + n_y1 + " , " + n_z1 + ")";

            string n_x2 = Convert.ToString((z2 * sine) + (x2 * cosine));
            string n_y2 = Convert.ToString(y2);
            string n_z2 = Convert.ToString((y2 * cosine) - (x2 * sine));

            textBox35.Text += "(" + n_x2 + " , " + n_y2 + " , " + n_z2 + ")";

            string n_x3 = Convert.ToString((z3 * sine) + (x3 * cosine));
            string n_y3 = Convert.ToString(y3);
            string n_z3 = Convert.ToString((y3 * cosine) - (x3 * sine));

            textBox37.Text += "(" + n_x3 + " , " + n_y3 + " , " + n_z3 + ")";

            string n_x4 = Convert.ToString((z4 * sine) + (x4 * cosine));
            string n_y4 = Convert.ToString(y4);
            string n_z4 = Convert.ToString((y4 * cosine) - (x4 * sine));

            textBox36.Text += "(" + n_x4 + " , " + n_y4 + " , " + n_z4 + ")";
        }

        private void button20_Click(object sender, EventArgs e)  // 3D Rotation Around Z
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int z1 = Convert.ToInt32(textBox30.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int z2 = Convert.ToInt32(textBox31.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int z3 = Convert.ToInt32(textBox33.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);
            int z4 = Convert.ToInt32(textBox32.Text);


            double angle = Convert.ToInt32(textBox26.Text);

            double sine = Math.Sin((angle * (Math.PI)) / 180);
            double cosine = Math.Cos((angle * (Math.PI)) / 180);

            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();


            string n_x1 = Convert.ToString((x1 * cosine) - (y1 * sine));
            string n_y1 = Convert.ToString((x1 * sine) + (y1 * cosine));
            string n_z1 = Convert.ToString(z1);

            textBox34.Text += "(" + n_x1 + " , " + n_y1 + " , " + n_z1 + ")";

            string n_x2 = Convert.ToString((x2 * cosine) - (y2 * sine));
            string n_y2 = Convert.ToString((x2 * sine) + (y2 * cosine));
            string n_z2 = Convert.ToString(z2);

            textBox35.Text += "(" + n_x2 + " , " + n_y2 + " , " + n_z2 + ")";

            string n_x3 = Convert.ToString((x3 * cosine) - (y3 * sine));
            string n_y3 = Convert.ToString((x3 * sine) + (y3 * cosine));
            string n_z3 = Convert.ToString(z3);

            textBox37.Text += "(" + n_x3 + " , " + n_y3 + " , " + n_z3 + ")";

            string n_x4 = Convert.ToString((x4 * cosine) - (y4 * sine));
            string n_y4 = Convert.ToString((x4 * sine) + (y4 * cosine));
            string n_z4 = Convert.ToString(z4);

            textBox36.Text += "(" + n_x4 + " , " + n_y4 + " , " + n_z4 + ")";
        }

        private void button21_Click(object sender, EventArgs e) // 3D Shearing in X
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int z1 = Convert.ToInt32(textBox30.Text);

            int shy = Convert.ToInt32(textBox40.Text);
            int shz = Convert.ToInt32(textBox42.Text);

            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();

            string n_x1 = Convert.ToString(x1);
            string n_y1 = Convert.ToString(y1 + (shy * x1));
            string n_z1 = Convert.ToString(z1 + (shz * x1));

            textBox34.Text += "(" + n_x1 + " , " + n_y1 + " , " + n_z1 + ")";

        }

        private void button22_Click(object sender, EventArgs e) // 3D Shearing in Y
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int z1 = Convert.ToInt32(textBox30.Text);

            int shx = Convert.ToInt32(textBox41.Text);
            int shz = Convert.ToInt32(textBox42.Text);

            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();

            string n_x1 = Convert.ToString(x1 + (shx * y1));
            string n_y1 = Convert.ToString(y1);
            string n_z1 = Convert.ToString(z1 + (shz * y1));

            textBox34.Text += "(" + n_x1 + " , " + n_y1 + " , " + n_z1 + ")";

        }

        private void button23_Click(object sender, EventArgs e)  // 3D Shearing in Z
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int z1 = Convert.ToInt32(textBox30.Text);

            int shx = Convert.ToInt32(textBox41.Text);
            int shy = Convert.ToInt32(textBox40.Text);

            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();

            string n_x1 = Convert.ToString(x1 + (shx * z1));
            string n_y1 = Convert.ToString(y1 + (shy * z1));
            string n_z1 = Convert.ToString(z1);

            textBox34.Text += "(" + n_x1 + " , " + n_y1 + " , " + n_z1 + ")";
        }
    
        private void button24_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);


            Pen pen = new Pen(Color.Red, 3);


            /* Second Quarter */

            Point p1 = new Point((-1 * x1) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y1);
            Point p2 = new Point((-1 * x2) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y2);
            Point p3 = new Point((-1 * x3) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y3);
            Point p4 = new Point((-1 * x4) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - y4);

            pictureBox1.CreateGraphics().DrawLine(pen, p1, p2);
            pictureBox1.CreateGraphics().DrawLine(pen, p2, p3);
            pictureBox1.CreateGraphics().DrawLine(pen, p3, p4);
            pictureBox1.CreateGraphics().DrawLine(pen, p4, p1);

        }

        private void button25_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);


            Pen pen = new Pen(Color.Red, 3);


            /* Fourth Quarter */

            Point p9 = new Point(x1 + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (-1 * y1));
            Point p10 = new Point(x2 + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (-1 * y2));
            Point p11 = new Point(x3 + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (-1 * y3));
            Point p12 = new Point(x4 + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (-1 * y4));

            pictureBox1.CreateGraphics().DrawLine(pen, p9, p10);
            pictureBox1.CreateGraphics().DrawLine(pen, p10, p11);
            pictureBox1.CreateGraphics().DrawLine(pen, p11, p12);
            pictureBox1.CreateGraphics().DrawLine(pen, p12, p9);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox16.Text);
            int y1 = Convert.ToInt32(textBox17.Text);
            int x2 = Convert.ToInt32(textBox18.Text);
            int y2 = Convert.ToInt32(textBox19.Text);
            int x3 = Convert.ToInt32(textBox20.Text);
            int y3 = Convert.ToInt32(textBox21.Text);
            int x4 = Convert.ToInt32(textBox22.Text);
            int y4 = Convert.ToInt32(textBox23.Text);


            Pen pen = new Pen(Color.Red, 3);


            /* Third Quarter */

            Point p5 = new Point((-1 * x1) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (-1 * y1));
            Point p6 = new Point((-1 * x2) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (-1 * y2));
            Point p7 = new Point((-1 * x3) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (-1 * y3));
            Point p8 = new Point((-1 * x4) + (pictureBox1.Size.Width / 2), (pictureBox1.Size.Height / 2) - (-1 * y4));

            pictureBox1.CreateGraphics().DrawLine(pen, p5, p6);
            pictureBox1.CreateGraphics().DrawLine(pen, p6, p7);
            pictureBox1.CreateGraphics().DrawLine(pen, p7, p8);
            pictureBox1.CreateGraphics().DrawLine(pen, p8, p5);


        }
    }

}
