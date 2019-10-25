using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OneButton
{
    class Scene_Count
    {
        int count;
        const int NEW_COUNT = 120;
        public Scene_Count()
        {
            count = NEW_COUNT;
        }

        public bool Change(bool dead)
        {
            bool flag = false ;
            if (dead)
            {
                count--;
                if (count <= 0)
                {
                    count = NEW_COUNT;
                    flag = true;
                }
            }
            return flag;
        }
    }
}
