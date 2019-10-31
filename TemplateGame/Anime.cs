using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;

namespace OneButton
{
    class Anime
    {
        Size size = new Size();
        List<Vector2> pos = new List<Vector2>();
        List<int> hp = new List<int>();
        List<float> ppx = new List<float>();
        List<float> ppy = new List<float>();
        System.Random rnd = new System.Random();

        const int SIZE_PLAYER = 64;
        const int RUDIOS = 32;
        const int NEW_COUNT_10 = 10;
        const int NEW_COUNT_30 = 30;

        const int TIME_LIGHT = 40;
        const int HP_LIGHT = 30;
        const int MAX_LIGHT = 3;
        const int MIN_LIGHT = 1;
        const float SPEED_LIGHT = 0.4f;

        Texture2D player, enemy, maru, lights;

        int enemy_x, enemy_y;

        enum State { drop, fly, stop, accele, dead }
        enum tex { dropPre, drop, flyPre, fly, stop, dead }
        enum name { chara, lights }
        int x, y;
        int count;
        int Pnum;
        float[] scale = new float[2];  
        float[] sca = new float[2];  
        float alpha_Lights;  
        float alphaNum_Lights;  
        bool dead;
        public Anime() { Ini(); }
        public bool Dead { get { return dead; } }
        public void Ini()
        {
            enemy_x = 0; enemy_y = 0;
            x = 0; y = 0;
            for (int i = 0; i < 2; i++) scale[i] = 1.0f;  
            sca[(int)name.chara] = 0.04f;  
            sca[(int)name.lights] = 0.02f;  
            alpha_Lights = 1;  
            alphaNum_Lights = 0.01f;  
            count = NEW_COUNT_10;
            Pnum = MIN_LIGHT;
            dead = false;
            pos.Clear();
            hp.Clear();
            ppx.Clear();
            ppy.Clear();
        }

        public void Load(ContentManager content)
        {
            player = content.Load<Texture2D>("player");
            maru = content.Load<Texture2D>("light");
            lights = content.Load<Texture2D>("lights");
        }
        public void Update(int state, int statePre, Vector2 playerPos, int sc, bool accel, bool accelePre)  
        {
            Move_X();
            
            Lights_Dead(playerPos, sc);
            if (state == (int)State.dead) Lights_Last();
            else
            {
                Pre(state, statePre, accel, accelePre);
                Lights_Emission();
                Lights_Bone(playerPos, accel);
            }
        }
        public void Pre(int state, int statePre, bool accele, bool accelePre)
        {
            switch (statePre)
            {
                case (int)State.drop://落下から浮遊
                    if (state == (int)State.fly)
                    {
                        y = (int)tex.fly;
                        x = 0;
                    }
                    else if (state == (int)State.stop)
                    {
                        y = (int)tex.stop;
                        x = 0;
                    }
                    break;
                case (int)State.fly://浮遊から落下
                    if (state == (int)State.stop)
                    {
                        y = (int)tex.stop;
                        x = 0;
                    }
                    if (state == (int)State.drop)
                    {
                        y = (int)tex.drop;
                        x = 0;
                    }
                    break;
                case (int)State.stop://静止から浮遊
                    if (state == (int)State.fly)
                    {
                        y = (int)tex.flyPre;
                        x = 0;
                    }
                    break;
            }

            if (!accelePre && accele)
            {
                y = (int)tex.dropPre;
                x = 0;
            }
            if (state == (int)State.fly)
            {
                if (scale[(int)name.chara] >= 1) sca[(int)name.chara] = -0.004f;
                else if (scale[(int)name.chara] <= 0.9f) sca[(int)name.chara] = 0.004f;
                scale[(int)name.chara] += sca[(int)name.chara];
            }
            else scale[(int)name.chara] = 1;
            
        }
        public void Move_X()
        {
            count--;
            if (count <= 0)
            {
                x++;
                if (x >= player.Width / SIZE_PLAYER)
                {
                    x = 0;
                    if (y == (int)tex.dropPre) y = (int)tex.drop;
                    if (y == (int)tex.flyPre) y = (int)tex.fly;
                    if (y == (int)tex.dead) dead = true;
                }
                if (y == (int)tex.drop || y == (int)tex.fly) count = NEW_COUNT_30;
                else count = NEW_COUNT_10;
            }
        }
        public void Lights_Emission()  
        {
            if (scale[(int)name.lights] >= 1.1f) sca[(int)name.lights] = -0.003f;
            else if (scale[(int)name.lights] <= 0.9f) sca[(int)name.lights] = 0.003f;
            scale[(int)name.lights] += sca[(int)name.lights];
            if (alpha_Lights >= 1) alphaNum_Lights = -0.01f;
            else if (alpha_Lights <= 0) alphaNum_Lights = 0.01f;
            alpha_Lights += alphaNum_Lights;
        }
        public void Lights_Last()  
        {
            if (alpha_Lights >= 0) alpha_Lights -= 0.01f;
        }
        public void DD()
        {
            y = (int)tex.dead;
            x = 0;
        }
        public void Lights_Bone(Vector2 playerPos, bool accel)  
        {
            if (accel) Pnum = MAX_LIGHT;
            else Pnum = MIN_LIGHT;
            for (int i = 0; i < Pnum; i++)
            {
                pos.Add(new Vector2(rnd.Next((int)playerPos.X - RUDIOS, (int)(playerPos.X - RUDIOS) + SIZE_PLAYER - maru.Width), rnd.Next((int)playerPos.Y - SIZE_PLAYER / 2, (int)playerPos.Y - RUDIOS + (SIZE_PLAYER - SIZE_PLAYER / 4))));
                hp.Add(HP_LIGHT);
                ppx.Add(0);
                ppy.Add(0);
            }
        }
        public void Lights_Dead(Vector2 playerPos, int sc)  
        {
            for (int i = 0; i < hp.Count; i++)
            {
                hp[i]--;
                ppy[i] += SPEED_LIGHT;
                if (hp[i] <= HP_LIGHT - 5)
                {
                    if (pos[i].X <= playerPos.X - RUDIOS + SIZE_PLAYER / 4) ppx[i] -= SPEED_LIGHT;
                    else if (pos[i].X >= playerPos.X - RUDIOS + SIZE_PLAYER - (SIZE_PLAYER / 4)) ppx[i] += SPEED_LIGHT;
                }
                if (hp[i] <= 0)
                {
                    hp.Remove(hp[i]);
                    pos.Remove(pos[i]);
                    ppx.Remove(ppx[i]);
                    ppy.Remove(ppy[i]);
                }
            }
        }
        public void Draw(SpriteBatch sb, Vector2 pos, int sc, int state)
        {
            if (!dead)
            {
                for (int i = 0; i < this.pos.Count; i++) sb.Draw(maru, new Vector2(this.pos[i].X + ppx[i], this.pos[i].Y - sc - ppy[i]), Color.White);//パーティクル
                sb.Draw(lights, new Vector2(pos.X - RUDIOS - SIZE_PLAYER / 2, pos.Y - RUDIOS - SIZE_PLAYER / 2 - sc) + new Vector2(64, 64), new Rectangle(0, 0, 128, 128), Color.White * alpha_Lights, 0.0f, new Vector2(64, 64), scale[(int)name.lights], SpriteEffects.None, 1.0f);
                sb.Draw(player, new Vector2(pos.X - RUDIOS, pos.Y - RUDIOS - sc) + new Vector2(SIZE_PLAYER / 2, SIZE_PLAYER / 2), new Rectangle(SIZE_PLAYER * x, SIZE_PLAYER * y, SIZE_PLAYER, SIZE_PLAYER), Color.White, 0.0f, new Vector2(SIZE_PLAYER / 2, SIZE_PLAYER / 2), scale[(int)name.chara], SpriteEffects.None, 1.0f);//動作確認
            }
        }
    }
}
