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
        Texture2D barBg;

        readonly Vector2 barPos = new Vector2(20, 20);
        readonly Vector2 barSize = new Vector2(320, 32);
        const int ITEM_MAX = 5;
        int ITEM_UP;
        int nowItem;
        const int AC_SPEED=2;
        int nowBar;

        public void Init()
        {
            nowItem = 0;
            ITEM_UP = (int)barSize.X / ITEM_MAX;
            nowBar = nowItem * ITEM_UP;
        }
        public void Load(ContentManager c)
        {
            bar = c.Load<Texture2D>("barP");
            barBg = c.Load<Texture2D>("barBg");
        }
        public void GetItem()
        {
            if (nowItem < ITEM_MAX)
                nowItem++;
            nowBar = nowItem * ITEM_UP;
        }
        public bool Accele()
        {
            if (nowBar <= 0) return false;
            nowBar -=AC_SPEED;
            return true;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(bar, new Rectangle((int)barPos.X, (int)barPos.Y,nowBar, (int)barSize.Y), Color.Wheat);
        }
    }
}
