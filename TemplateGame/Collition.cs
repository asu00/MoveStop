using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace OneButton
{

    class Collition
    {

        public int FloorColl(Vector2 pPos, int pR, Vector2[] otherPos, Vector2 oSize) //床
        {
            for (int i = 0; i < otherPos.Length; i++)
            {
                Vector2 oPos = otherPos[i];

                if (pPos.X + 32 > oPos.X && pPos.X < oPos.X + oSize.X && pPos.Y + 32 > oPos.Y && pPos.Y < oPos.Y + oSize.Y)
                    return i;
            }
            return -1;
        }
        public int PrColl(Vector2 pPos, int pR, Vector2[] othrerPos, Vector2 oSize, Vector2 oSize_s, bool[] prMoveF) //とげ
        {
            for (int i = 0; i < othrerPos.Length; i++)
            {
                Vector2 oPos = othrerPos[i];

                if (!prMoveF[i]) //停止
                {
                    if (pPos.X + pR > oPos.X && pPos.X - pR < oPos.X + oSize_s.X && pPos.Y + pR > oPos.Y && pPos.Y - pR < oPos.Y + oSize_s.Y)
                        return i;
                }
                else
                {
                    if (pPos.X + pR > oPos.X && pPos.X - pR < oPos.X + oSize.X && pPos.Y + pR > oPos.Y && pPos.Y - pR < oPos.Y + oSize.Y)
                        return i;
                }
            }
            return -1;
        }

        public int ItemColl(Vector2 pPos, int pR, Vector2[] othrerPos, Vector2 oSize, bool[] nowGet, SoundEffect item) //アイテム
        {
            for (int i = 0; i < othrerPos.Length; i++)
            {
                if (nowGet[i]) continue;
                Vector2 oPos = othrerPos[i];

                if (pPos.X + pR > oPos.X && pPos.X - pR < oPos.X + oSize.X && pPos.Y + pR > oPos.Y && pPos.Y - pR < oPos.Y + oSize.Y)
                {
                    item.Play();
                    return i;
                }
            }
            return -1;
        }

        public bool EnemyColl(Vector2 pPos, int pR, Vector2 oPos, Vector2 oSize) //敵　上だけ
        {

            if (pPos.Y - pR < oPos.Y + oSize.Y)
                return true;

            return false;
        }

    }
}

