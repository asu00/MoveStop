using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace OneButton
{
    class UI_Button
    {
        Size size = new Size();
        const int INDEX_ALPHA = 2;
        const float SPEED_ALPHA = 0.01f;

        enum Alpha { alpha, inc_or_dec };
        float[] alpha = new float[INDEX_ALPHA];

        Texture2D button;
        public UI_Button() { Ini(); }
        public void Ini()
        {
            alpha[(int)Alpha.alpha] = 1;
            alpha[(int)Alpha.inc_or_dec] = -SPEED_ALPHA;
        }
        public void Load(ContentManager content)
        {
            button = content.Load<Texture2D>("ui_button");
        }
        public void Button()
        {
            if (alpha[(int)Alpha.alpha] < 0 || alpha[(int)Alpha.alpha] > 1)
            {
                alpha[(int)Alpha.inc_or_dec] *= -1;
            }
            alpha[(int)Alpha.alpha] += alpha[(int)Alpha.inc_or_dec];
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(button, new Vector2(size.Win_Width / 2 - button.Width / 2, size.Height - button.Height*3), Color.White * alpha[(int)Alpha.alpha]);
        }
    }
}
    