using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OneButton
{
    class PositionBar
    {
        Size size = new Size();

        //バー上のplayer
        Texture2D playertex;
        Vector2 playerbarpos;
        public Vector2 PlayerPos { get { return playerbarpos; } }

        //バー
        Texture2D bartex;
        Vector2 barpos;
        public Vector2 BarPos { get { return barpos; } }

        public PositionBar() { Init(); }

        public  void Init()
        {
            playerbarpos = new Vector2(size.Width + 24, 0);
            barpos = new Vector2(size.Width + 16, 0);

        }

        public void Load(ContentManager content)
        {
            playertex = content.Load<Texture2D>("posbar");
            bartex = content.Load<Texture2D>("bar");
        }


        public void Update(Vector2 playerpos)
        {
            Calcu(playerpos);
        }

        public void Calcu(Vector2 playerpos)
        {          
            playerbarpos.Y = size.Height * playerpos.Y/ size.World;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(bartex, barpos, Color.White);
            sb.Draw(playertex, playerbarpos, Color.White); 
        }
    }
}
