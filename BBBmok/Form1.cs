﻿using System;
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
        Graphics GBoard;
        Graphics GMouse;
        Omok game;
        Dictionary<int, Brush> _pColor;
        

        public Form1()
        {
            GBoard = this.CreateGraphics();
            GMouse = this.CreateGraphics();

            _pColor = new Dictionary<int, Brush>();
            _pColor.Add(1, Brushes.Black);
            _pColor.Add(-1, Brushes.White);
            game = new Omok();
            InitializeComponent();
            game.AddStone(8, 8);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.BurlyWood, new Rectangle(30, 30, 420, 420));
            for (int i = 0; i < 15; i++)
            {
                e.Graphics.DrawLine(Pens.Black, new Point(31, 30 + i * 30), new Point(450, 30 + i * 30));
                e.Graphics.DrawLine(Pens.Black, new Point(30 + i * 30, 31), new Point(30 + i * 30, 450));
            }
        }

        private void Form1_Click(object sender, MouseEventArgs e)
        {
            var location = MakeBoardXY();
            int x = location.Item1;
            int y = location.Item2;

            if (x == 0 || y == 0) return;

            int nowPlayer = game.AddStone(x, y);
            if (nowPlayer != 0) PaintStone(x, y, nowPlayer);
            int winner = game.Check(x, y);
            if (winner != 0)
            {
                if (winner == 1) MessageBox.Show("Player 1 이 이겼습니다!");
                if (winner == -1) MessageBox.Show("Player 2 가 이겼습니다!");
                Application.Exit();
            }
        }

        private void PaintStone(int x, int y, int player)
        {
            GBoard.FillEllipse(_pColor[player], new Rectangle(new Point((x - 1) * 30 + 16, (y - 1) * 30 + 16), new Size(28, 28)));
        }

        
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            var location = MakeBoardXY();
            int x = location.Item1;
            int y = location.Item2;

        }

        /// <summary>
        /// 실제 게임판의 좌표를 가져옵니다.
        /// </summary>
        /// <returns>Return (x, y)</returns>
        private (int, int) MakeBoardXY()
        {
            int mousex = MousePosition.X - Location.X;
            int mousey = MousePosition.Y - Location.Y;
            if (!(23 < mousex && mousex < 465 && 47 < mousey && mousey < 465)) return (0, 0);

            int x = (mousex - 23) % 30 == 0 ? 0 : (mousex - 23) / 30 + 1;
            int y = (mousey - 47) % 30 == 0 ? 0 : (mousey - 47) / 30 + 1;

            return (x, y);
        }
    }
}