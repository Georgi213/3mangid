using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3_mangid
{
    public partial class Mathematicquiz : Form
    {
        TableLayoutPanel tl;
        Random rnd = new Random();
        char[] sümbolid = new char[] { '+', '-', '*', '/' };
        int plussÜks, plussKaks;
        int korrutadaÜks, korrutadaKaks;
        int jagaÜks, jagaKaks;
        int miinusÜks, miinusKaks;
        int aega_jäänud;
        Timer timer;
        Label lb;
        Button start;

        public Mathematicquiz()
        {
            this.Size = new Size(500, 400);
            this.Name = "Matemaatika viktoriin";
            tl = new TableLayoutPanel()
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                AutoSize = true,
                Location = new System.Drawing.Point(0, 0),
                ColumnCount = 5,
                RowCount = 4,
                TabIndex = 0,
                BackColor = System.Drawing.Color.White
            };

           

            lb = new Label
            {
                Font = new Font(Font.FontFamily, 17),
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(200, 30),
            };
            Label label = new Label
            {
                Font = new Font(Font.FontFamily, (float)15.75),
                Text = "Aega on jäänud",
                AutoSize = true,
            };
            start = new Button
            {
                Text = "Alustage viktoriini",
                Font = new Font(Font.FontFamily, 14),
                AutoSize = true,
                TabIndex = 0

            };
            start.Click += Start_Click;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            for (int i = 1; i < 5; i++)
            {
                Label num1 = new Label
                {
                    Font = new Font(FontFamily.GenericSansSerif, 17),
                    Text = "?",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(60, 70),
                };
                Label znak = new Label
                {
                    Font = new Font(FontFamily.GenericSansSerif, 17),
                    Text = sümbolid[i - 1].ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(60, 70),
                };
                Label num2 = new Label
                {
                    Font = new Font(FontFamily.GenericSansSerif, 17),
                    Text = "?",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(60, 70),
                };
                Label ravno = new Label
                {
                    Font = new Font(FontFamily.GenericSansSerif, 17),
                    Text = "=",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(60, 70),
                };
                NumericUpDown X = new NumericUpDown
                {
                    Font = new Font(FontFamily.GenericSansSerif, 17),
                    Width = 100,
                    TabIndex = i + 1,
                };
                tl.Controls.Add(num1, 0, i);
                tl.Controls.Add(znak, 1, i);
                tl.Controls.Add(num2, 2, i);
                tl.Controls.Add(ravno, 3, i);
                tl.Controls.Add(X, 4, i);
            }
            tl.Controls.Add(lb, 3, 0);
            tl.SetColumnSpan(label, 2);
            tl.SetColumnSpan(lb, 2);
            tl.Controls.Add(label, 1, 0);

            tl.SetColumnSpan(start, 2);
            tl.Controls.Add(start, 2, 5);
            Controls.Add(tl);


        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            NumericUpDown X = (NumericUpDown)tl.GetControlFromPosition(4, 1);
            NumericUpDown minX = (NumericUpDown)tl.GetControlFromPosition(4, 2);
            NumericUpDown mulX = (NumericUpDown)tl.GetControlFromPosition(4, 3);
            NumericUpDown divX = (NumericUpDown)tl.GetControlFromPosition(4, 4);
            if (CheckTheAnswer())
            {
                timer.Stop();
                MessageBox.Show("Teil on kõik õiged!",
                                 "Palju õnne!");
                start.Enabled = true;
            }
            else if (aega_jäänud > 0)
            {
                aega_jäänud = aega_jäänud - 1;
                lb.Text = aega_jäänud + " sekundit";
            }
            else
            {
                timer.Stop();
                lb.Text = "Aeg on lõpetanud";
                MessageBox.Show("Te ei lõpetanud täpneks ajaks.", "Vabandage!");
                X.Value = plussÜks + plussKaks;
                minX.Value = miinusÜks - miinusKaks;
                mulX.Value = korrutadaÜks * korrutadaKaks;
                divX.Value = jagaÜks / jagaKaks;
                start.Enabled = true;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            start.Enabled = false;
        }
        private bool CheckTheAnswer()
        {
            NumericUpDown X = (NumericUpDown)tl.GetControlFromPosition(4, 1);
            NumericUpDown minX = (NumericUpDown)tl.GetControlFromPosition(4, 2);
            NumericUpDown mulX = (NumericUpDown)tl.GetControlFromPosition(4, 3);
            NumericUpDown divX = (NumericUpDown)tl.GetControlFromPosition(4, 4);
            if ((plussÜks + plussKaks == X.Value)
                && (miinusÜks - miinusKaks == minX.Value)
                && (korrutadaÜks * korrutadaKaks == mulX.Value)
                && (jagaÜks / jagaKaks == divX.Value))
                return true;
            else
                return false;
        }
        public int[] getNums(string summ)
        {
            int num1 = 0;
            int num2 = 0;

            if (summ == "+")
            {
                num1 = rnd.Next(51);
                num2 = rnd.Next(51);
                plussÜks = num1;
                plussKaks = num2;
            }
            else if (summ == "-")
            {
                num1 = rnd.Next(1, 101);
                num2 = rnd.Next(1, num1);
                miinusÜks = num1;
                miinusKaks = num2;
            }
            else if (summ == "/")
            {
                num2 = rnd.Next(2, 11);
                int temporaryQuotient = rnd.Next(2, 11);
                num1 = num2 * temporaryQuotient;
                jagaÜks = num1;
                jagaKaks = num2;
            }
            else if (summ == "*")
            {
                num1 = rnd.Next(2, 11);
                num2 = rnd.Next(2, 11);
                korrutadaÜks = num1;
                korrutadaKaks = num2;
            }

            return new int[2] { num1, num2 };
        }
        public void StartTheQuiz()
        {
            for (int row = 1; row < 5; row++)
            {
                Label num1 = (Label)tl.GetControlFromPosition(0, row);
                Label symbol = (Label)tl.GetControlFromPosition(1, row);
                Label num2 = (Label)tl.GetControlFromPosition(2, row);
                NumericUpDown N = (NumericUpDown)tl.GetControlFromPosition(4, row);
                int[] thing = getNums(symbol.Text);
                num1.Text = thing[0].ToString();
                num2.Text = thing[1].ToString();
                N.Value = 0;
            }
            aega_jäänud = 35;
            lb.Text = "35 sekundit";
            timer.Start();
        }

       
    }
}
