using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

/// <summary>
/// Manages Background Music and SFX
/// </summary>
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

    /// <summary>
    /// Plays a song as background music
    /// </summary>
    public void PlaySong(string name)
    {
        try
        {
            // Load the song
            song = contentManager.Load<Song>("SoundEffects/" + name);

            // Play the song
            MediaPlayer.Play(song);

            // Make the song repeat
            MediaPlayer.IsRepeating = true;

            // Eventhandler
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }
        catch (Exception)
        {
            Console.WriteLine("Song not found!");
        }
    }

    private void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
    {
        MediaPlayer.Volume = 0.5f;
        MediaPlayer.Play(song);
    }

    /// <summary>
    /// Loads a sound effect into the contentmanager
    /// </summary>
    public void loadSoundEffect(string name)
    {
        try
        {
            // Adds a sfx to the sfx list
            soundEffects.Add(contentManager.Load<SoundEffect>("SoundEffects/" + name));
        }
        catch (Exception e)
        {
            Console.WriteLine("SoundEffect not found!");
        }   
    }

    /// <summary>
    /// Pauses a song
    /// </summary>
    public void PauseSong()
    {
        MediaPlayer.Pause();
    }

    /// <summary>
    /// Resumes a paused song
    /// </summary>
    public void ResumeSong()
    {
        MediaPlayer.Resume();
    }

    /// <summary>
    /// Plays a sound effect
    /// </summary>
    public void playSoundEffect(string name)
    {
        try
        {
            foreach (SoundEffect sfx in soundEffects)
            {
                if (sfx.Name == "SoundEffects/" + name)
                {
                    // Make an instance of the SFX (This is so we can change the volume)
                    SoundEffectInstance instance = sfx.CreateInstance();

                    // Change the sfx volume
                    instance.Volume = 0.1f;

                    // Play the sfx
                    instance.Play();
                }
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Sound effect not playable!");
        }
        
    }
}

