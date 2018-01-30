using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

public class SoundManager
{
    ContentManager contentManager;
    Song song;
    List<SoundEffect> soundEffects;
    public SoundManager(ContentManager content)
    {
        contentManager = content;
        soundEffects = new List<SoundEffect>();
    }

    public void PlaySong(string name)
    {
        try
        {
            song = contentManager.Load<Song>("SoundEffects/" + name);
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }
        catch (Exception)
        {

            Console.WriteLine("Song not found!");
        }
    }

    private void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
    {
        MediaPlayer.Volume -= 0.1f;
        MediaPlayer.Play(song);
    }

    public void loadSoundEffect(string name)
    {
        try
        {
            soundEffects.Add(contentManager.Load<SoundEffect>("SoundEffects/" + name));
        }
        catch (Exception e)
        {
            Console.WriteLine("SoundEffect not found!");
        }   
    }

    public void PauseSong()
    {
        MediaPlayer.Pause();
    }

    public void ResumeSong()
    {
        MediaPlayer.Resume();
    }

    public void playSoundEffect(string name)
    {
        try
        {
            foreach (SoundEffect sfx in soundEffects)
            {
                if (sfx.Name == name)
                {
                    sfx.Play();
                }
            }
        }
        catch (Exception)
        {

            Console.WriteLine("Sound effect not playable!");
        }
        
    }
}

