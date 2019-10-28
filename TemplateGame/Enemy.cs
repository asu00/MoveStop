using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OneButton
{
    class Enemy
    {
        Texture2D enemy;
        Vector2 pos;

        int count;
       readonly Vector2 SIZE = new Vector2(640, 960);
        const int SPEED = 1;
        const int APPEAR = 3;

        public Vector2 Pos { get{ return pos; } }
        public Vector2 Size => SIZE;

        public Enemy() { Init(); }

        public void Init()
        {
            count = 0;
            pos = new Vector2(-10, -680);
        }

        public void Load(ContentManager content)
        {
            enemy = content.Load<Texture2D>("enemy");
        }

        public void Update()
        {
            Move();
        }

        public void Move()
        {
            count++;
            if(count>=60*APPEAR)pos.Y += SPEED;
        }

        public void Draw(SpriteBatch sb ,int sc)
        {
            sb.Draw(enemy, new Vector2(pos.X,pos.Y-sc), Color.White);
        }

    }
}
