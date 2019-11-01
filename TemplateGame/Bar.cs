using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Threading.Tasks;

namespace OneButton
{
    class Bar
    {
        Texture2D bar;
        readonly Vector2 barPos = new Vector2(8, 32);
        readonly Vector2 barSize = new Vector2(320, 32);
        const int ITEM_MAX = 5;
        int ITEM_UP; //一回の回復量
        const float AC_SPEED = 1; //ゲージ消費量
        float nowBar;

        public Bar() { Init(); }
        public void Init()
        {
            nowBar = 0;
            ITEM_UP = (int)barSize.X / ITEM_MAX; //最大アイテム数から1回の回復量を計算
        }
        public void Load(ContentManager c)
        {
            bar = c.Load<Texture2D>("gage");
        }
        public bool GetItem() 
        {
            if (nowBar > barSize.X) return false; //最初から超えていたら上げない

            float nextUp = nowBar + ITEM_UP;
            //足して最大値を超えなければ普通に+
            //超えるなら最大値に合わせる
            if (nextUp <= barSize.X)
                nowBar = nextUp;
            else if (nextUp > barSize.X)
                nowBar = (int)barSize.X;
            return true;
        }
        public bool Accele()
        {
            if (nowBar <= 0) return false;
            nowBar -= AC_SPEED;
            return true;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(bar, new Rectangle((int)barPos.X, (int)barPos.Y, (int)nowBar, (int)barSize.Y), Color.Wheat);
        }
    }
}
