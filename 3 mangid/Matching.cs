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
    public partial class Matching : Form
    {
        Random random = new Random();
        TableLayoutPanel tableLayoutPanel1;
        Label firstClicked = null;
        Label secondClicked = null;
        Timer time;
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };
        public Matching()
        {
            this.Size = new Size(600, 600);
            MaximizeBox = false;
            tableLayoutPanel1 = new TableLayoutPanel
            {
                BackColor = ColorTranslator.FromHtml("LightBlue"),
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset,
            };
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Label lb = new Label
                    {
                        BackColor = ColorTranslator.FromHtml("LightBlue"),
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Arial", 45, FontStyle.Bold),
                        Size = new Size(45, 45),
                        Text = "match"
                    };
                    lb.Click += label1_Click;
                    tableLayoutPanel1.Controls.Add(lb, i, j);
                }
            }
            time = new Timer();
            time.Interval = 620;
            time.Tick += Tm_Tick;
            Controls.AddRange(new Control[] { tableLayoutPanel1, });
            AssignIconsToSquares();
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            time.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            if (time.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Gray)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Gray;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Gray;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                time.Start();
            }
        }
        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("Sa sobitasid kõik ikoonid!", "Palju õnne");
            Close();
        }
    }
}

