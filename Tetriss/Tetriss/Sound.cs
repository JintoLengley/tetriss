using System.IO;
using System.Media;


class Sound
{
    SoundPlayer sndPlayer = new SoundPlayer();
    public void Play(UnmanagedMemoryStream soundResId)
    {
        sndPlayer = new SoundPlayer(soundResId);
        sndPlayer.Play();
    }
    public void Halt()
    { 
    sndPlayer.Stop();
    }

    ~Sound()
    {
        sndPlayer.Stop();
        sndPlayer.Dispose();
    }
}