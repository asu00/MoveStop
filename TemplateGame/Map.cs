using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace OneButton
{
    class Map
    {
        //針山
        Texture2D prickle;
        const int PR_SPEED = 4;
        bool[] prDrawF;
        readonly Vector2 prSize = new Vector2(128, 32);//針の部分のサイズ

        readonly Vector2[] prPosBase = {new Vector2(300,1800),new Vector2(210,3000),new Vector2(300,3650),new Vector2(210,3900),
            new Vector2(300,4300),new Vector2(210,5300),new Vector2(210,5600),new Vector2 (290,5900),new Vector2(210,6200),
            new Vector2(290,6400),new Vector2(290,7400),new Vector2(210,7600),new Vector2(210,8500),new Vector2(290,8600),
            new Vector2(210,9600),new Vector2(290,9700),new Vector2(245,9800)};

        Vector2[] prPos;
        public Vector2[] PrPos => prPos;
        public Vector2 PrSize => prSize;

        //床
        Texture2D floor;
        bool[] floorDrawF;
        const int F_SPEED = 5;
        readonly Vector2 fSize = new Vector2(128, 32);

        readonly Vector2[] floorPosBase = { new Vector2(300, 900), new Vector2(200, 1400),
            new Vector2(210,2100),new Vector2(240,2200),new Vector2(260,2300),new Vector2 (270,2400),new Vector2(290,2500),
            new Vector2 (210,2600),new Vector2(270,2700),new Vector2(230,2800),new Vector2 (210,3400),new Vector2(260,3800),
            new Vector2(290,4900),new Vector2(210,6000),new Vector2(290,7000),new Vector2(210,7200),
            new Vector2(290,8000),new Vector2(210,8300),new Vector2(210,9400)};

        Vector2[] floorPos; //書き込み用
        float[] movePos;
        public float[] MovePos => movePos;
        public Vector2[] FloorPos => floorPos;
        public Vector2 Fsize => fSize;

        //床の向き
        enum Dir
        {
            NONE = 0,
            RIGHT = 1,
            LEFT = -1,
        }
        readonly Dir[] floorDirBase = { Dir.LEFT,Dir.RIGHT,Dir.RIGHT,Dir.RIGHT,Dir.RIGHT,Dir.RIGHT,Dir.RIGHT,Dir.RIGHT,Dir.RIGHT,
            Dir.RIGHT,Dir.RIGHT,Dir.LEFT,Dir.LEFT,Dir.RIGHT,Dir.LEFT,Dir.RIGHT,Dir.LEFT,Dir.LEFT,Dir.RIGHT};
        int[] fd; //書き込み用
        readonly Dir[] prDirBase = { Dir.LEFT,Dir.RIGHT,Dir.LEFT,Dir.RIGHT,Dir.LEFT,Dir.RIGHT,Dir.RIGHT,Dir.LEFT,
            Dir.RIGHT,Dir.LEFT,Dir.LEFT,Dir.RIGHT,Dir.LEFT,Dir.RIGHT,Dir.RIGHT,Dir.LEFT,Dir.RIGHT};
        int[] prd; //書き込み用

        //アイテム
        Texture2D item;
        readonly Vector2 iSize = new Vector2(64, 64);
        public Vector2 ISize => iSize;
        readonly Vector2[] itemPosBase = { new Vector2(220,500),new Vector2(400, 850),new Vector2 (100,2100), new Vector2(300, 2400),
            new Vector2(220,4850),new Vector2(220,7150),new Vector2(80,8250),new Vector2(450,8250),new Vector2(230,9350)};
        Vector2[] itemPos;
        public Vector2[] ItemPos => itemPos;
        bool[] inowGet, inowDraw;
        public bool[] InowGet => inowGet;

        public Map()
        {
            Init();
        }

        public void Init()
        {

            prDrawF = new bool[prPosBase.Length];
            for (int i = 0; i < prPosBase.Length; i++)
            {
                prDrawF[i] = false;
            }
            prPos = prPosBase;
            prd = new int[prDirBase.Length];
            for (int i = 0; i < prDirBase.Length; i++)
            {
                prd[i] = (int)prDirBase[i]; //intに変換
                prDrawF[i] = false;
            }

            //床の情報を書き込み用に入れる
            floorPos = floorPosBase;
            fd = new int[floorDirBase.Length];
            floorDrawF = new bool[floorPosBase.Length];
            for (int i = 0; i < floorDirBase.Length; i++)
            {
                fd[i] = (int)floorDirBase[i]; //intに変換
                floorDrawF[i] = false;
            }
            movePos = new float[floorPosBase.Length];

            //アイテム
            itemPos = itemPosBase;
            inowDraw = new bool[itemPosBase.Length];
            inowGet = new bool[itemPosBase.Length];
            for (int i = 0; i < itemPosBase.Length; i++)
            {
                inowDraw[i] = false;
                inowGet[i] = false;
            }
        }

        public void Load(ContentManager content)
        {
            prickle = content.Load<Texture2D>("thornber");
            floor = content.Load<Texture2D>("ber");
            item = content.Load<Texture2D>("item");
        }

        public void FlagChange(Vector2 pPos)
        {
            for (int i = 0; i < floorPos.Length; i++)
            {
                if (64 * 15 > floorPos[i].Y - pPos.Y && -(64 * 15) < floorPos[i].Y - pPos.Y)
                    floorDrawF[i] = true;
                else
                    floorDrawF[i] = false;
            }
            for (int i = 0; i < prPosBase.Length; i++)
            {
                if (64 * 15 > PrPos[i].Y - pPos.Y && -(64 * 15) < PrPos[i].Y - pPos.Y)
                    prDrawF[i] = true;
                else
                    prDrawF[i] = false;
            }
            for (int i = 0; i < itemPosBase.Length; i++)
            {
                if (64 * 15 > itemPos[i].Y - pPos.Y && -(64 * 15) < itemPos[i].Y - pPos.Y)
                    inowDraw[i] = true;
                else
                    inowDraw[i] = false;
            }
        }
        public void FloorMove(int wid)
        {
            for (int i = 0; i < floorPos.Length; i++)
            {
                if (!floorDrawF[i]) continue;
                movePos[i] = fd[i] * F_SPEED;
                floorPos[i].X += movePos[i];
                if (floorPos[i].X + fSize.X > wid-32 || floorPos[i].X < 32) fd[i] = -fd[i]; //反転
            }
            for (int i = 0; i < prPosBase.Length; i++)
            {
                if (!prDrawF[i]) continue;
                prPos[i].X += prd[i] * PR_SPEED;
                if (prPos[i].X + PrSize.X > wid - 32 || prPos[i].X < 32) prd[i] = -prd[i]; //反転
            }
        }

        public void ItemGet(int i)
        {
            inowGet[i] = true;
        }

        public void Draw(SpriteBatch sb, int sc)
        {
            for (int i = 0; i < floorPosBase.Length; i++)
            {
                if (!floorDrawF[i]) continue;
                sb.Draw(floor, new Vector2(floorPos[i].X, floorPos[i].Y - sc), Color.White);
            }
            for (int i = 0; i < prPosBase.Length; i++)
            {
                if (!prDrawF[i]) continue;
                sb.Draw(prickle, new Vector2(prPosBase[i].X, prPosBase[i].Y - sc), Color.White);
            }
            for (int i = 0; i < itemPosBase.Length; i++)
            {
                if (!inowDraw[i] || inowGet[i]) continue;
                sb.Draw(item, new Vector2(itemPos[i].X, itemPos[i].Y - sc), Color.White);
            }
        }
    }
}
