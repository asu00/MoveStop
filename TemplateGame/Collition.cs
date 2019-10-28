﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace OneButton
{

    class Collition
    {
        public int FloorColl(Vector2 pPos, int pR, Vector2[] otherPos, Vector2 oSize) //床
        {
            for (int i = 0; i < otherPos.Length; i++)
            {
                Vector2 oPos = otherPos[i];

                if (pPos.X+64 > oPos.X && pPos.X < oPos.X + oSize.X && pPos.Y + 32 > oPos.Y && pPos.Y < oPos.Y + oSize.Y)
                    return i;
            }
            return -1;
        }
        public bool PrColl(Vector2 pPos, int pR, Vector2[] othrerPos, Vector2 oSize) //とげ
        {
            for (int i = 0; i < othrerPos.Length; i++)
            {
                Vector2 oPos = othrerPos[i];

                if (pPos.X + pR > oPos.X && pPos.X - pR < oPos.X + oSize.X && pPos.Y + pR > oPos.Y && pPos.Y - pR < oPos.Y + oSize.Y)
                    return true;
            }
            return false;
        }

        public int ItemColl(Vector2 pPos, int pR, Vector2[] othrerPos, Vector2 oSize,bool[] nowGet) //アイテム
        {
            for (int i = 0; i < othrerPos.Length; i++)
            {
                if (nowGet[i]) continue;
                Vector2 oPos = othrerPos[i];

                if (pPos.X + pR > oPos.X && pPos.X - pR < oPos.X + oSize.X && pPos.Y + pR > oPos.Y && pPos.Y - pR < oPos.Y + oSize.Y)
                    return i;
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

