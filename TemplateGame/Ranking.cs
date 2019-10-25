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

        const int BIT = 64;

        const int RANKING = 3;
        const int SIZE_X = 32;
        const int SIZE_Y = 64;

        const int SECOND = 10000;
        const int THOUSAND = 1000;
        const int HUNDRED = 100;
        const int TEN = 10;

        int playerTime;
        bool flg;

        Texture2D rankTex;
        Vector2 playerpos;
        Vector2[] pos = new Vector2[RANKING];
        List<int> rank = new List<int>();


        public Ranking()
        {
            Init();
        }
        public void Init()
        {
            playerTime = 0;
            playerpos = new Vector2(BIT*3,BIT*3);
            flg = false;
            pos[0] = new Vector2(BIT*3,BIT*4+BIT/4);
            pos[1] = new Vector2(BIT * 3, BIT * 5+ 32);
            pos[2] = new Vector2(BIT * 3, BIT * 6 + 56);
            for (int i = 0; i < RANKING; i++)
            {
                rank.Add(SECOND * SECOND * TEN);
            }
        }
        public void Load(ContentManager content)
        {
            rankTex = content.Load<Texture2D>("num");
        }

       

        public void GiveRanking(Vector2 playerpos, int time)
        {
            if (!flg)
            {
                flg = true;
                playerTime = time + THOUSAND * THOUSAND * HUNDRED;
                rank.Add(time + THOUSAND * THOUSAND * HUNDRED);
                rank.Sort();
                rank.RemoveAt(RANKING);
            }
        }

        public void Draw(SpriteBatch sb)
        {

            
            sb.Draw(rankTex, new Rectangle((int)playerpos.X + SIZE_X * 5, (int)playerpos.Y, SIZE_X, SIZE_Y),
                new Rectangle(playerTime % THOUSAND / HUNDRED * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(rankTex, new Rectangle((int)playerpos.X + SIZE_X * 4, (int)playerpos.Y, SIZE_X, SIZE_Y),
               new Rectangle(playerTime % (THOUSAND * TEN) / THOUSAND * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(rankTex, new Rectangle((int)playerpos.X + SIZE_X * 3, (int)playerpos.Y, SIZE_X, SIZE_Y),
                new Rectangle(playerTime % (THOUSAND * HUNDRED) / (THOUSAND * TEN) * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(rankTex, new Rectangle((int)playerpos.X + SIZE_X * 2, (int)playerpos.Y, SIZE_X, SIZE_Y),
                new Rectangle(playerTime % (THOUSAND * THOUSAND) / (THOUSAND * HUNDRED) * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(rankTex, new Rectangle((int)playerpos.X + SIZE_X, (int)playerpos.Y, SIZE_X, SIZE_Y),
               new Rectangle(playerTime % (THOUSAND * THOUSAND * TEN) / (THOUSAND * THOUSAND) * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(rankTex, new Rectangle((int)playerpos.X, (int)playerpos.Y, SIZE_X, SIZE_Y),
                new Rectangle(playerTime % (THOUSAND * THOUSAND * HUNDRED) / (THOUSAND * THOUSAND * TEN) * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);

            for (int i = 0; i < RANKING; i++)
            {
               
                sb.Draw(rankTex, new Rectangle((int)pos[i].X + SIZE_X * 5, (int)pos[i].Y, SIZE_X, SIZE_Y),
                    new Rectangle(rank[i] % THOUSAND / HUNDRED * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
                sb.Draw(rankTex, new Rectangle((int)pos[i].X + SIZE_X * 4, (int)pos[i].Y, SIZE_X, SIZE_Y),
                   new Rectangle(rank[i] % (THOUSAND * TEN) / THOUSAND * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
                sb.Draw(rankTex, new Rectangle((int)pos[i].X + SIZE_X * 3, (int)pos[i].Y, SIZE_X, SIZE_Y),
                    new Rectangle(rank[i] % (THOUSAND * HUNDRED) / (THOUSAND * TEN) * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
                sb.Draw(rankTex, new Rectangle((int)pos[i].X + SIZE_X * 2, (int)pos[i].Y, SIZE_X, SIZE_Y),
                    new Rectangle(rank[i] % (THOUSAND * THOUSAND) / (THOUSAND * HUNDRED) * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
                sb.Draw(rankTex, new Rectangle((int)pos[i].X + SIZE_X, (int)pos[i].Y, SIZE_X, SIZE_Y),
                   new Rectangle(rank[i] % (THOUSAND * THOUSAND * TEN) / (THOUSAND * THOUSAND) * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
                sb.Draw(rankTex, new Rectangle((int)pos[i].X, (int)pos[i].Y, SIZE_X, SIZE_Y),
                    new Rectangle(rank[i] % (THOUSAND * THOUSAND * HUNDRED) / (THOUSAND * THOUSAND * TEN) * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            }
        }

    }
}
