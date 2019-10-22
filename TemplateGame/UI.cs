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

        const int Y = 100;

        Texture2D head,wall,posber;
        Texture2D title,tutlial,lose,back;
        Texture2D text_Lose;
        Vector2[] pos = new Vector2[2];

        bool move;

        float alpha;

        public bool Move { get { return move; } }
        public UI() { Ini(); }
        public void Ini()
        {
            alpha = 0;
            pos[0] = Vector2.Zero;
            pos[1] = new Vector2(0, size.Height);
        }
        public void Load(ContentManager content,GraphicsDevice graphicsDevice)
        {
            back = new Texture2D(graphicsDevice,1,1);
            back.SetData<Color>(new Color[] { Color.Black});
            head = content.Load<Texture2D>("head");
            wall = content.Load<Texture2D>("wall");
            title = content.Load<Texture2D>("title");
            tutlial = content.Load<Texture2D>("tutlial");
            posber = content.Load<Texture2D>("posber");
            lose = content.Load<Texture2D>("lose");
            text_Lose = content.Load<Texture2D>("lose");
        }
        public void Scroll(int sc)
        {
            for(int i =0; i < 2; i++) if (pos[i].Y - sc <= -wall.Height) pos[i].Y = sc + wall.Height;
        }
        public bool Scene_Change(bool button)
        {
            bool flag = false ;
            if (button) move = true;
            if (move)
            {
                if(alpha <= 1)alpha += 0.01f;
                else
                {
                    flag = true;
                    move = false;
                    alpha = 0;
                }
            }
            return flag;
        }
        public void Draw_Back(SpriteBatch sb)
        {
            sb.Draw(back,new Rectangle(0,0,size.Win_Width,size.Height),Color.White*alpha);
        }
        public void Draw_Title(SpriteBatch sb)
        {
            sb.Draw(title,new Vector2(size.Win_Width/2 - title.Width/2,Y),Color.White);
            Draw_Back(sb);
        }
        public void Draw_Tutlial(SpriteBatch sb)
        {
            sb.Draw(tutlial, new Vector2(size.Win_Width/ 2 - tutlial.Width/2, Y), Color.White);
            Draw_Back(sb);
        }
        public void Draw_Lose(SpriteBatch sb)
        {
            sb.Draw(lose,new Vector2(size.Win_Width / 2 - lose.Width / 2, Y),Color.White);
            sb.Draw(text_Lose,new Vector2(size.Win_Width/2 - text_Lose.Width/2,500),Color.White);
        }
        public void Draw(SpriteBatch sb,int sc)
        {
            for(int i = 0; i < 2; i++)sb.Draw(wall, new Vector2(-32,pos[i].Y - sc), Color.White);
            for (int i = 0; i < 2; i++) sb.Draw(wall, new Vector2(size.Width-wall.Width/2, pos[i].Y - sc),new Rectangle(0,0,wall.Width,wall.Height), Color.White,0.0f,Vector2.Zero,1.0f,SpriteEffects.FlipHorizontally,1.0f);
            sb.Draw(posber,new Vector2(size.Win_Width - posber.Width,0),Color.White);
            sb.Draw(head,Vector2.Zero,Color.White);
        }
    }
}
