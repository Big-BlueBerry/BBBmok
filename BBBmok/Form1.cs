using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBBmok
{
    public partial class Form1 : Form
    {

        Omok game;
        Dictionary<int, Brush> _pColor;
        int _x, _y;
        

        public Form1()
        {
            _x = 0; _y = 0;
            

            _pColor = new Dictionary<int, Brush>();
            _pColor.Add(1, Brushes.Black);
            _pColor.Add(-1, Brushes.White);
            game = new Omok();
            InitializeComponent();
            game.AddStone(8, 8);
            game.Check(8, 8);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // 판 그리기
            e.Graphics.FillRectangle(Brushes.BurlyWood, new Rectangle(30, 30, 420, 420));
            for (int i = 0; i < 15; i++)
            {
                e.Graphics.DrawLine(Pens.Black, new Point(31, 30 + i * 30), new Point(450, 30 + i * 30));
                e.Graphics.DrawLine(Pens.Black, new Point(30 + i * 30, 31), new Point(30 + i * 30, 450));
            }
            
            // 지금까지 놓은 돌 그리기
            for (int i = 1; i < 15; i++)
            {
                for (int j = 1; j < 15; j++)
                {
                    if (game.map[i, j] != 0)
                    {
                        e.Graphics.FillEllipse(_pColor[game.player],
                            new Rectangle(new Point((_x - 1) * 30 + 16,
                            (_y - 1) * 30 + 16), new Size(28, 28)));
                    }
                }
            }

            // 마우스 커서 그리기
            e.Graphics.DrawRectangle(Pens.DarkCyan,
                new Rectangle(new Point((_x - 1) * 30 + 16, (_y - 1) * 30 + 16),
                new Size(28, 28)));


        }

        private void Form1_Click(object sender, MouseEventArgs e)
        {
            if (_x == 0 || _y == 0) return;

            if (!game.AddStone(_x, _y)) return;
            Invalidate();
            int winner = game.Check(_x, _y);
            if (winner != 0)
            {
                if (winner == 1) MessageBox.Show("Player 1 이 이겼습니다!");
                if (winner == -1) MessageBox.Show("Player 2 가 이겼습니다!");
                Application.Exit();
            }
            
        }
        


        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            var location = MakeBoardXY();
            int newx = location.Item1;
            int newy = location.Item2;

            if ((newx > 0 && newy > 0 && newx <= 15 && newy <= 15) && (newx != _x || newy != _y))
            {
                _x = newx; _y = newy;
            }
        }

        /// <summary>
        /// 실제 게임판의 좌표를 가져옵니다.
        /// </summary>
        /// <returns>Return (x, y)</returns>
        private (int, int) MakeBoardXY()
        {
            int mousex = MousePosition.X - Location.X;
            int mousey = MousePosition.Y - Location.Y;
            if (!(23 < mousex && mousex < 465 && 47 < mousey && mousey < 500)) return (0, 0);

            int x = (mousex - 23) % 30 == 0 ? 0 : (mousex - 23) / 30 + 1;
            int y = (mousey - 47) % 30 == 0 ? 0 : (mousey - 47) / 30 + 1;

            return (x, y);
        }
    }
}