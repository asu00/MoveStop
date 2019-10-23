using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace OneButton
{
    class UI
    {
        Size size = new Size();

        const int NEW_COUNT_30 = 30;
        const int Y = 50;
        const int SPEED = 6;
        const int SIZE = 64;//※※

        Texture2D head, wall, posber;
        Texture2D title, tutlial, lose, back;
        Texture2D text_Lose, text_end;
        Texture2D lights, character, ber;
        Vector2[] pos = new Vector2[2];

        bool move;

        float alpha;
        //public bool Move { get { return move; } }
        Vector2[] pos_Lights = new Vector2[2];//※※
        float[] alpha_Lights = new float[2];//※※
        int count;//※※
        int x, y;//※※
        Vector2 player = new Vector2(0, 0);//※※
        bool tut;//※※

        public UI() { Ini(); }
        public void Ini()
        {
            alpha = 0;
            pos[0] = Vector2.Zero;
            pos[1] = new Vector2(0, size.Height);
            for (int i = 0; i < 2; i++) alpha_Lights[i] = 1;
            pos_Lights[0] = new Vector2(0, size.Height / 2);
            pos_Lights[1] = new Vector2(0, size.Height / 2 + 200);
            x = 0; y = 0;//※※
            count = NEW_COUNT_30;//※※
            player = new Vector2(size.Width / 2 - SIZE / 2, 400);//※※
            tut = false;//※※
        }
        public void Load(ContentManager content, GraphicsDevice graphicsDevice)
        {
            back = new Texture2D(graphicsDevice, 1, 1);
            back.SetData<Color>(new Color[] { Color.Black });
            head = content.Load<Texture2D>("head");
            wall = content.Load<Texture2D>("wall");
            title = content.Load<Texture2D>("title");
            tutlial = content.Load<Texture2D>("tutlial");
            posber = content.Load<Texture2D>("posber");
            lose = content.Load<Texture2D>("lose");
            text_Lose = content.Load<Texture2D>("lose");
            text_end = content.Load<Texture2D>("score");
            lights = content.Load<Texture2D>("star");
            character = content.Load<Texture2D>("cha");
            ber = content.Load<Texture2D>("ashiba");
        }
        public void Scroll(int sc)
        {
            for (int i = 0; i < 2; i++) if (pos[i].Y - sc <= -wall.Height) pos[i].Y = sc + wall.Height;
        }
        public bool Scene_Change(bool button)
        {
            bool flag = false;
            if (button) move = true;
            if (move)
            {
                if (alpha <= 1) alpha += 0.01f;
                else
                {
                    flag = true;
                    move = false;
                    alpha = 0;
                }
            }
            return flag;
        }


        public bool Title(bool key)//※※
        {
            jump();
            bool flag = false;
            if (key && y <= 0)
            {
                x = 0; y = 1;
                count = 10;
            }
            if (player.Y >= 0 && y == 2) player.Y += SPEED;
            if (player.Y >= size.Height)
            {
                flag = true;
                player.Y = -SIZE;
            }
            return flag;
        }
        public void jump()//※※
        {
            count--;
            if (count <= 0)
            {
                x++;
                if (x >= character.Width / SIZE)
                {
                    if (y == 1) y++;
                    x = 0;
                }
                if (y == 1) count = 10;
                else count = 30;
            }
        }
        public bool Tutlial(bool key)//※※
        {
            jump();
            bool flag = false;
            if (key && !tut)
            {
                tut = true;
                x = 0; y = 1;
                count = 10;
            }
            if (!tut)
            {
                if (player.Y + SIZE <= size.Height / 2) player.Y += SPEED;
                else y = 0;
            }
            else if (y == 2) player.Y += SPEED;
            if (player.Y >= size.Height) flag = true;
            return flag;
        }
        public void Anime()//※※
        {
            for (int i = 0; i < 2; i++)
            {
                pos_Lights[i].Y -= 0.4f;
                if (pos_Lights[i].Y <= size.Height / 2) alpha_Lights[i] -= 0.01f;
                else if (alpha_Lights[i] <= 1) alpha_Lights[i] += 0.02f;
                if (pos_Lights[i].Y + lights.Height / 4 <= size.Height / 2) pos_Lights[i].Y = size.Height - 200;
            }
        }
        public void Draw_Back(SpriteBatch sb)
        {
            sb.Draw(back, new Rectangle(0, 0, size.Win_Width, size.Height), Color.White * alpha);
        }
        public void Draw_Anime(SpriteBatch sb)//※※
        {
            sb.Draw(wall, new Vector2(-32, pos[0].Y), Color.White);
            sb.Draw(wall, new Vector2(size.Win_Width - wall.Width / 2, pos[0].Y), new Rectangle(0, 0, wall.Width, wall.Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 1.0f);
            for (int i = 0; i < 2; i++) sb.Draw(lights, pos_Lights[i], Color.White * alpha_Lights[i]);
        }
        public void Draw_Title(SpriteBatch sb)//※※
        {
            sb.Draw(character, player, new Rectangle(SIZE * x, SIZE * y, SIZE, SIZE), Color.White);
            sb.Draw(ber, new Vector2(size.Win_Width / 2 - ber.Width / 2, 464), Color.White);
            sb.Draw(title, Vector2.Zero, Color.White);
            Draw_Back(sb);
        }
        public void Draw_Tutlial(SpriteBatch sb)//※※
        {
            sb.Draw(character, player, new Rectangle(SIZE * x, SIZE * y, SIZE, SIZE), Color.White);
            sb.Draw(tutlial, Vector2.Zero, Color.White);
            Draw_Back(sb);
        }
        public void Draw_Lose(SpriteBatch sb)
        {
            sb.Draw(lose, new Vector2(size.Win_Width / 2 - lose.Width / 2, Y), Color.White);
            sb.Draw(text_Lose, new Vector2(size.Win_Width / 2 - text_Lose.Width / 2, 500), Color.White);
        }
        public void Draw_End(SpriteBatch sb)
        {
            sb.Draw(text_end, new Vector2(size.Win_Width / 2 - text_end.Width / 2, 100), Color.White);
        }
        public void Draw(SpriteBatch sb, int sc)
        {
            for (int i = 0; i < 2; i++) sb.Draw(wall, new Vector2(-32, pos[i].Y - sc), Color.White);
            for (int i = 0; i < 2; i++) sb.Draw(wall, new Vector2(size.Width - wall.Width / 2, pos[i].Y - sc), new Rectangle(0, 0, wall.Width, wall.Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 1.0f);
            sb.Draw(posber, new Vector2(size.Win_Width - posber.Width, 0), Color.White);
            sb.Draw(head, Vector2.Zero, Color.White);
        }
    }
}
