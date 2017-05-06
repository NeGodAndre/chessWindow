using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chessWindow
{
    public partial class Form1 : Form, IInterface
    {
        Button[,] button;
        int N = 8;
        Controller control;

        public Form1()
        {
            InitializeComponent();
            button = new Button[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    button[i, j] = new Button();
                    button[i, j].TabIndex = i * N + j;
                    button[i, j].Text = i.ToString() + j.ToString();
                    button[i, j].Size = new Size(60, 60);
                    //15?35
                    button[i, j].Location = new Point(15 + i * 60, 35 + j * 60);
                    button[i, j].BackColor = Color.Gray;
                    button[i, j].MouseClick += click;
                    this.Controls.Add(button[i, j]);
                }


            control = new Controller(this);
            control.Start("white");
        }

        private void click(object sender, System.EventArgs e)
        {
            Button b = sender as Button;
            //label1.Text = (b.TabIndex / 8).ToString() + (b.TabIndex % 8).ToString();
            control.CellAssignment((b.TabIndex / 8), (b.TabIndex % 8));
        }

        public void FillCell(Position position, string name, string color)
        {
            button[position.x, position.y].Text = name;
            if (color == "white")
                button[position.x, position.y].ForeColor = Color.White;
            if (color == "black")
                button[position.x, position.y].ForeColor = Color.Blue;
            if(color == "")
                button[position.x, position.y].ForeColor = Color.Gray;
        }

        public void Move(Position oldPosition, Position newPosition)
        {
            button[newPosition.x, newPosition.y].Text = button[oldPosition.x, oldPosition.y].Text;
            button[newPosition.x, newPosition.y].ForeColor = button[oldPosition.x, oldPosition.y].ForeColor;
            button[oldPosition.x, oldPosition.y].Text = "";
            button[oldPosition.x, oldPosition.y].BackColor = Color.Gray;
        }

        List<Position> hint;

        public void HintOn(List<Position> hint)
        {
            this.hint = hint;
            for(int i = 0; i < hint.Count; i++)
            {
                button[hint[i].x, hint[i].y].BackColor = Color.AliceBlue;
            }
        }

        public void HintOff()
        {
            for (int i = 0; i < hint.Count; i++)
            {
                button[hint[i].x, hint[i].y].BackColor = Color.Gray;
            }
            hint = new List<Position>();
        }
        public void DisplayingMessage(string message)
        {
            label1.Text = message;
        }
        public void PawnToQueen(Position position, string name)
        {
            button[position.x, position.y].Text = name;
        }
    }
}
