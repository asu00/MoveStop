using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using System.Timers;

namespace OneButton
{
    
    class Time
    {
        Size size = new Size();
        Texture2D tex;

        //画像サイズ
        const int SIZE_X = 32;
        const int SIZE_Y = 64;

        //表示サイズ
        readonly Vector2 DISPLAY = new Vector2(16,32);

        const int ONE_FLAME = 167;
        const int MINUTES = 60;
        const int SECOND = 10000;
        const int THOUSAND = 1000;
        const int HUNDRED = 100;
        const int TEN = 10;

        int mill;
        int second;
        int minutes;

        int stopTime;

        Vector2 oneMill;
        Vector2 twoMill;
        Vector2 threeMill;
        Vector2 fourMill;

        Vector2 oneSecond;
        Vector2 twoSecond;

        Vector2 oneMinutes;
        Vector2 twoMinutes;

        public int StopTime { get{ return stopTime; } }

        public Time()
        {
            Init();
        }

        public void Init()
        {
            mill = 0;
            second = 0;
            minutes = 0;

            oneMill = new Vector2(DISPLAY.X*8, SIZE_X);
            twoMill = new Vector2(DISPLAY.X * 7, SIZE_X);
            threeMill = new Vector2(DISPLAY.X * 6, SIZE_X);
            fourMill = new Vector2(DISPLAY.X * 5, SIZE_X);

            oneSecond = new Vector2(DISPLAY.X* 4, SIZE_X);
            twoSecond = new Vector2(DISPLAY.X * 3, SIZE_X);

            oneMinutes = new Vector2(DISPLAY.X * 2, SIZE_X);
            twoMinutes = new Vector2(DISPLAY.X, SIZE_X);
        }

        public void Load(ContentManager content)
        {
            tex = content.Load<Texture2D>("num");
        }

       public void Updae(Vector2 player)
        {
            Timer(player);
        }

        private void Timer(Vector2 player)
        {
            if(size.World >= player.Y)mill += ONE_FLAME;
            if(size.World==player.Y)
            {
                stopTime = minutes * THOUSAND * THOUSAND + second * SECOND + mill;
            }
            if (mill >= SECOND)
            {
                second++;
                mill = 0;
            }
            if (second == MINUTES)
            {
                minutes++;
                second = 0;
            }

        }

        public void Draw(SpriteBatch sb)
        {

            sb.Draw(tex, new Rectangle((int)oneMill.X, (int)oneMill.Y, (int)DISPLAY.X,(int) DISPLAY.Y), 
                new Rectangle(mill %TEN*SIZE_X,0, SIZE_X, SIZE_Y),Color.White);
            sb.Draw(tex, new Rectangle((int)twoMill.X, (int)twoMill.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(mill % HUNDRED/ TEN*SIZE_X,0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(tex, new Rectangle((int)threeMill.X, (int)threeMill.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(mill%THOUSAND / HUNDRED*SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(tex, new Rectangle((int)fourMill.X, (int)fourMill.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(mill / THOUSAND * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(tex, new Rectangle((int)oneSecond.X, (int)oneSecond.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(second % TEN*SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(tex, new Rectangle((int)twoSecond.X, (int)twoSecond.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(second / TEN*SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(tex, new Rectangle((int)oneMinutes.X, (int)oneMinutes.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(minutes% TEN*SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(tex, new Rectangle((int)twoMinutes.X, (int)twoMinutes.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(minutes / TEN*SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
        }
    }
}
