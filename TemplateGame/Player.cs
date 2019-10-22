using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;

namespace OneButton
{
    class Player
    {
        Size size = new Size();
        const int SIZE_PLAYER = 64;

        Texture2D player;
        Vector2 pos;
        int speed;
       const int RUDIOS = 32;

        //***
        const int HIGH_SPEED = 6;
        const int NORMAL_SPEED = 2;
        int sc;
        bool drop,accele;
        public bool DropF => drop;

        public int SC { get { return sc; } }
        public Vector2 Pos { get { return pos; } }
        public int R => RUDIOS;
        public Player() { Ini(); }
        public void Ini()
        {
            pos = new Vector2(size.Width/2 - SIZE_PLAYER/2,0);
            //***
            speed = HIGH_SPEED;
            sc = 0;
            drop = true ;
            accele = false;

        }
        public void Load(ContentManager content)
        {
            player = content.Load<Texture2D>("player");
        }
        public void Update(Key key, Func<bool> Accele)
        {
            KeyPushMove(key,Accele);
        }
        public void KeyPushMove(Key key, Func<bool> Accele)
        {
            if (key.TwoPush ) accele = true;
            if (accele && Accele())
            {
                drop = true;
                //***
                speed = HIGH_SPEED;
            }
            //***
            else speed = NORMAL_SPEED;
            if (key.OnePush)
            {
                switch (drop)
                {
                    case true:
                        drop = false;
                        accele = false;
                        break;
                    case false:
                        drop = true;
                        accele = false;
                        break;
                }
            }
            Debug.WriteLine("加速" + accele);
        }
        public void Drop()
        {
            if(drop) pos.Y += speed;
            if (pos.Y > size.Under && drop && pos.Y <= size.EndSc) sc += speed;
        }
        public void FloorMove(float move)
        {
            pos.X += move;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(player,new Vector2(pos.X- RUDIOS, pos.Y-RUDIOS - sc),Color.White);
        }
    }
}
