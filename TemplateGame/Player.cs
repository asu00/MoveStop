using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace OneButton
{
    class Player
    {
        Size size = new Size();
       
        const int SIZE_PLAYER = 64;

        Vector2 pos, posPre, hit;
        const int COLL = 5;
        int speed;
        const int RUDIOS = 32;


        const int HIGH_SPEED = 15;
        const int NORMAL_SPEED = 9;
        int sc;
        bool drop, accele, accelePre,muflg;
        bool goal;

        public bool DropF => drop;
        public int Coll => COLL;

        public int SC { get { return sc; } }
        public Vector2 Pos { get { return pos; } }
        public Vector2 PosPre { get { return posPre; } }
        public Vector2 Hit { get { return hit; } }
        public int R => RUDIOS;

        enum State { drop, fly, stop, acccel, dead }
        State state;
        State statePre;

        public int St { get { return (int)state; } }
        public int StPre { get { return (int)statePre; } }
        public bool Ac { get { return accele; } }
        public bool AcPre { get { return accelePre; } }

        public Player() { Ini(); }
        public void Ini()
        {
            pos = new Vector2(size.Width / 2 - SIZE_PLAYER / 2, 0);
            posPre = new Vector2(size.Width / 2 - SIZE_PLAYER / 2, 0);
            state = State.drop;
            statePre = state;
            speed = HIGH_SPEED;
            sc = 0;
            drop = true;
            accele = false;
            accelePre = accele;
            goal = false;
        }

        public void Update(Key key, Func<bool> Accele,SoundEffect move,SoundEffect high)
        {
            Debug.WriteLine("Drop:"+drop);
            posPre = pos;
            statePre = state;
            KeyPushMove(key, Accele,move,high);
            if (!drop) state = State.fly;
        }
        public void KeyPushMove(Key key, Func<bool> Accele,SoundEffect move,SoundEffect high)
        {
            accelePre = accele;
            if (key.TwoPush&& state != State.stop)
            {
                accele = true;
            }
            if (accele && Accele())
            {
                if (muflg)
                {
                    high.Play();
                    muflg = false;
                }
                drop = true;
                speed = HIGH_SPEED;
            }

            else
            {
                speed = NORMAL_SPEED;
                accele = false;
            }
            if (key.OnePush)
            {
                muflg = true;
                move.Play();
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
        }
        public void Drop()
        {
            if (drop)
            {
                pos.Y += speed;
                state = State.drop;
            }
            if (pos.Y > size.Under && drop && pos.Y <= size.EndSc&& state != State.stop) sc += speed;
        }
        public void FloorMove(float move, float fy)
        {
            state = State.stop;
            //pos.Y = fy - 30;
            pos.X += move;
            if (pos.X < 64)
                pos.X = 64;
            else if (pos.X > 64 * 9 - 64)
                pos.X = 64 * 9 - 64;

            accele = false;
        }
        public void DeadFlag()
        {
            state = State.dead;
        }
        public bool GoalFlag()
        {
            if (pos.Y >= size.World) goal = true;
            return goal;
        }
    }
}
