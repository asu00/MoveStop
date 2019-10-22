using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace OneButton
{
    class Ranking
    {

        Size size = new Size();

        const int RANKING = 20;
        const int SIZE_X = 32;
        const int SIZE_Y = 64;

        const int SECOND = 10000;
        const int THOUSAND = 1000;
        const int HUNDRED = 100;
        const int TEN = 10;

        Texture2D rankTex, backTex;
        Vector2[] pos = new Vector2[RANKING];
        List<int> rank = new List<int>();


        public Ranking()
        {
            Init();
        }
        public void Init()
        {
            for (int i = 0; i < RANKING; i++)
            {
                rank.Add(1000000000);
                pos[i] = new Vector2(SIZE_X + SIZE_X, SIZE_Y + SIZE_Y * i);
            }
        }
        public void Load(ContentManager content)
        {
            rankTex = content.Load<Texture2D>("num");
        }

        public void Update(Vector2 playerpos, int time)
        {
            GiveRanking(playerpos, time);
        }

        public void GiveRanking(Vector2 playerpos, int time)
        {
            if (playerpos.Y < size.World)
            {
                rank.Add(time+THOUSAND*THOUSAND*HUNDRED);
                rank.Sort();
                rank.RemoveAt(RANKING);
            }
        }

        public void Draw(SpriteBatch sb)
        {

            for (int i = 0; i < RANKING; i++)
            {
                sb.Draw(rankTex, new Rectangle((int)pos[i].X+SIZE_X*7, (int)pos[i].Y, SIZE_X, SIZE_Y),
                    new Rectangle(rank[i] % TEN * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
                sb.Draw(rankTex, new Rectangle((int)pos[i].X + SIZE_X*6, (int)pos[i].Y, SIZE_X, SIZE_Y),
                    new Rectangle(rank[i] % HUNDRED/TEN * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
                sb.Draw(rankTex, new Rectangle((int)pos[i].X + SIZE_X * 5, (int)pos[i].Y, SIZE_X, SIZE_Y),
                    new Rectangle(rank[i] % THOUSAND/HUNDRED * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
                sb.Draw(rankTex, new Rectangle((int)pos[i].X + SIZE_X * 4, (int)pos[i].Y, SIZE_X, SIZE_Y),
                   new Rectangle(rank[i] % (THOUSAND*TEN)/THOUSAND * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
                sb.Draw(rankTex, new Rectangle((int)pos[i].X + SIZE_X * 3, (int)pos[i].Y, SIZE_X, SIZE_Y),
                    new Rectangle(rank[i] % (THOUSAND*HUNDRED)/(THOUSAND*TEN) * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
                sb.Draw(rankTex, new Rectangle((int)pos[i].X + SIZE_X*2, (int)pos[i].Y, SIZE_X, SIZE_Y),
                    new Rectangle(rank[i] % (THOUSAND*THOUSAND)/(THOUSAND*HUNDRED) * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
                 sb.Draw(rankTex, new Rectangle((int)pos[i].X+SIZE_X, (int)pos[i].Y, SIZE_X, SIZE_Y),
                    new Rectangle(rank[i] %(THOUSAND*THOUSAND*TEN)/(THOUSAND*THOUSAND) * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
                sb.Draw(rankTex, new Rectangle((int)pos[i].X, (int)pos[i].Y, SIZE_X, SIZE_Y),
                    new Rectangle(rank[i] %(THOUSAND*THOUSAND*HUNDRED)/(THOUSAND*THOUSAND*TEN)*SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            }
        }

    }
}
