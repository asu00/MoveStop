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
        readonly Vector2 DISPLAY = new Vector2(16, 32);

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

        Vector2[] commapos =new Vector2[2];
        Vector2 threeMill;
        Vector2 fourMill;

        Vector2 oneSecond;
        Vector2 twoSecond;

        Vector2 oneMinutes;
        Vector2 twoMinutes;

        public int StopTime { get { return stopTime; } }

        public Time()
        {
            Init();
        }

        public void Init()
        {
            mill = 0;
            second = 0;
            minutes = 0;


            threeMill = new Vector2(DISPLAY.X * 8+HUNDRED, 0);
            fourMill = new Vector2(DISPLAY.X * 7+HUNDRED, 0);

            commapos[1] = new Vector2(DISPLAY.X * 6+HUNDRED, 0);

            oneSecond = new Vector2(DISPLAY.X * 5+HUNDRED, 0);
            twoSecond = new Vector2(DISPLAY.X * 4+HUNDRED, 0);

            commapos[0] = new Vector2(DISPLAY.X * 3+HUNDRED,0);

            oneMinutes = new Vector2(DISPLAY.X * 2+HUNDRED, 0);
            twoMinutes = new Vector2(DISPLAY.X+HUNDRED, 0);
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
            if (size.World >= player.Y) mill += ONE_FLAME;
            stopTime = minutes * THOUSAND * THOUSAND + second * SECOND + mill;
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

            sb.Draw(tex, new Rectangle((int)threeMill.X, (int)threeMill.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(mill % THOUSAND / HUNDRED * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(tex, new Rectangle((int)fourMill.X, (int)fourMill.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(mill / THOUSAND * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);

             sb.Draw(tex, new Rectangle((int)commapos[0].X, (int)commapos[0].Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(TEN * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);

            sb.Draw(tex, new Rectangle((int)oneSecond.X, (int)oneSecond.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(second % TEN * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(tex, new Rectangle((int)twoSecond.X, (int)twoSecond.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(second / TEN * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);

            sb.Draw(tex, new Rectangle((int)commapos[1].X, (int)commapos[1].Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(TEN * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);

            sb.Draw(tex, new Rectangle((int)oneMinutes.X, (int)oneMinutes.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(minutes % TEN * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
            sb.Draw(tex, new Rectangle((int)twoMinutes.X, (int)twoMinutes.Y, (int)DISPLAY.X, (int)DISPLAY.Y),
                new Rectangle(minutes / TEN * SIZE_X, 0, SIZE_X, SIZE_Y), Color.White);
        }
    }
}
