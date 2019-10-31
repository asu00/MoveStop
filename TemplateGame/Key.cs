using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace OneButton
{
    class Key
    {
        const int NEWCOUNT = 10;//twoPushの判定許容フレ数
        const int RETAIR = 200;//長押しのフレ数
        const int KIND_OF_KEY = 2;
        const int KIND_OF_FlAG = 3;
        const int KIND_OF_NUM = 2;

        enum name_key { pre, now }//押したときのkeyの状態
        enum name_flag { onePush, twoPush, longPush }//返す条件の種類
        enum name_num { push_Num, push_Long_Num }//押した回数と長押しのフレ数

        bool[] key = new bool[KIND_OF_KEY];
        bool[] flag = new bool[KIND_OF_FlAG];
        int[] num = new int[KIND_OF_NUM];

        bool counting;//onePushしてから数え始める
        bool isPushKey;//keyが押されたか否か(1フレ)

        int count;
        int i;  

        public bool OnePush { get { return flag[(int)name_flag.onePush]; } }
        public bool TwoPush { get { return flag[(int)name_flag.twoPush]; } }
        public bool LongPush { get { return flag[(int)name_flag.longPush]; } }
        public bool IsPushKey { get { return isPushKey; } }
        public Key() { Ini(); }
        public void Ini()
        {
            count = NEWCOUNT;
            isPushKey = false;
            counting = false;
            for (int i = 0; i < KIND_OF_KEY; i++) key[i] = false;
            for (int i = 0; i < KIND_OF_FlAG; i++) flag[i] = false;
            for (int i = 0; i < KIND_OF_NUM; i++) num[i] = 0;

            i = 0;  
        }
        public void Update()
        {
            Push_Long();
            Push_One_Two();
            Re();  
        }
        public void Push_Triger()//スペースkeyのトリガー
        {
            isPushKey = false;

            flag[(int)name_flag.onePush] = false; flag[(int)name_flag.twoPush] = false;

            key[(int)name_key.pre] = key[(int)name_key.now];
            KeyboardState keyboard_Sp = Keyboard.GetState();
            key[(int)name_key.now] = keyboard_Sp.IsKeyDown(Keys.Space);

            if (key[(int)name_key.now] && !key[(int)name_key.pre]) isPushKey = true;
        }

        //長押しのフレ数を数える
        //const RETAIR分長押ししていたら長押ししたと判定
        public void Push_Long()
        {
            if (key[(int)name_key.now]) num[(int)name_num.push_Long_Num]++;
            else num[(int)name_num.push_Long_Num] = 0;

            if (num[(int)name_num.push_Long_Num] >= RETAIR) flag[(int)name_flag.longPush] = true;
        }

        //ワンプッシュかダブルプッシュか
        //const NEWCOUNT分数えてからワンかダブルか判定(スペース押すたびに判定)
        public void Push_One_Two()
        {
            if (isPushKey)
            {
                num[(int)name_num.push_Num]++;
                counting = true;
            }
            if (counting) count--;
            if (count <= 0)
            {
                if (num[(int)name_num.push_Num] >= 2) flag[(int)name_flag.twoPush] = true;
                else flag[(int)name_flag.onePush] = true;

                counting = false;
                count = NEWCOUNT;
                num[(int)name_num.push_Num] = 0;
            }
        }
        public int Re()  
        {
            if (flag[(int)name_flag.onePush]) i = 1;
            if (flag[(int)name_flag.twoPush]) i = 2;
            return i;
        }
    }
}
