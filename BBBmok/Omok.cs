using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBBmok
{
    public class Omok
    {
        public int player;

        /// <summary> 
        /// 1-based array
        /// </summary>
        public int[,] map;

        public Omok()
        {
            player = 1;
            map = new int[16, 16];
        }

        /// <summary>
        /// 돌을 놓는다. 실패 시 0, 성공시 플레이어를 반환한다.
        /// </summary>
        public bool AddStone(int x, int y)
        {
            if (map[x, y] != 0) return false;

            map[x, y] = player;
            
            return true;
        }

        /// <summary>
        /// 게임 끝났으면 player 이름
        /// </summary>
        public int Check(int x, int y)
        {
            int tempp = player;
            player *= -1;
            int basex = x - 4 <= 0 ? 1 : x - 4;   // x의 인덱스가 0이하면 1로 만들어줌
            int basey = y - 4 <= 0 ? 1 : y - 4;   // y의 인덱스가 0이하면 1로 만들어줌
            int cnt;

            #region Horizontal
            cnt = 0;
            for (int i = basex; i <= x + 4; i++)
            {
                if (i > 15) break;
                if (map[i, y] == tempp) cnt++;
                else cnt = 0;

                if (cnt >= 5) return tempp;
            }
            #endregion

            #region Vertical
            cnt = 0;
            for (int j = basey; j <= y + 4; j++)
            {
                if (j > 15) break;
                if (map[x, j] == tempp) cnt++;
                else cnt = 0;

                if (cnt >= 5) return tempp;
            }
            #endregion
            #region Diagonal
            cnt = 0;

            //좌상향 시작지점 정하기
            for(basex=x,basey=y;basex>=1&&basey>=1;basex--,basey--)
            {
                if (basex <= 1 || basey <= 1) break;
            }
            //우하향 검사
            for (; basex <= x + 4; basex++, basey++)
            {
                if (basex >= 16 || basey >= 16) break;
                if (map[basex, basey] == tempp) cnt++;
                else cnt = 0;
                if (cnt >= 5) return tempp;
            }

            cnt = 0;
            //좌하향 시작지점 정하기
            for (basex = x, basey = y; basex >= 1 && basey >= 1; basex--, basey++)
            {
                if (basex <= 1 || basey >= 15) break;
            }
            //우상향 검사
            for (; basex <= x + 4; basex++, basey--)
            {
                if (basex >= 16 || basey <=0) break;
                if (map[basex, basey] == tempp) cnt++;
                else cnt = 0;
                if (cnt >= 5) return tempp;
            }
            #endregion

            return 0;
        }
    }
}
