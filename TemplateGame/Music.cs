using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;


namespace OneButton
{
    class Music
    {
        private bool seflg;
        const int SE_MAX = 7, BGM_MAX = 2;
        SoundEffect[] se = new SoundEffect[SE_MAX];
        Song[] bgm = new Song[BGM_MAX];
        public SoundEffect[] Se { get { return se; } }

        public Music()
        {
            Init();
        }
        public void Init()
        {
            seflg = false;
        }
        public void Load(ContentManager content)
        {
            bgm[0] = content.Load<Song>("Alarm_Guitar");
            bgm[1] = content.Load<Song>("Alarm");
            se[0] = content.Load<SoundEffect>("Die");
            se[1] = content.Load<SoundEffect>("Clear");
            se[2] = content.Load<SoundEffect>("Fui");
            se[3] = content.Load<SoundEffect>("Powa");
            se[4] = content.Load<SoundEffect>("High");
            se[5] = content.Load<SoundEffect>("botan01");
            se[6] = content.Load<SoundEffect>("Hit");
        }
        public void SongPlayer(int song)
        {
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(bgm[song]);
            }
        }
        public void SongStopper()
        {
            MediaPlayer.Stop();
        }
        public void SePlay(int seNum)
        {
            se[seNum].Play();
        }
        public void OneSePlay(int seNum)
        {
            if (!seflg)
            {
                se[seNum].Play();
                seflg = true;
            }
        }
    }
}

