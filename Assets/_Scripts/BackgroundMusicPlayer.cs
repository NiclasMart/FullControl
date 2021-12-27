using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
  [SerializeField] List<Sound> music = new List<Sound>();
  AudioSource source;
  static BackgroundMusicPlayer instance;

  int lastPlayed = -1;

  private void Awake()
  {
    if (instance != null)
    {
      Destroy(this);
      return;
    }
    instance = this;
    DontDestroyOnLoad(gameObject);

    source = gameObject.AddComponent<AudioSource>();
    PlayNewSong();

  }

  private void Update()
  {
    if (!source.isPlaying) PlayNewSong();
  }

  public static void StopMusic()
  {
    Destroy(instance.gameObject);
    instance = null;
  }

  private void PlayNewSong()
  {
    int index = GetNextSongIndex();
    ConfigurateSource(music[index]);
    source.Play();
  }

  private void ConfigurateSource(Sound sound)
  {
    source.clip = sound.clip;
    source.volume = sound.volume;

  }

  private int GetNextSongIndex()
  {
    int rand;
    do
    {
      rand = Random.Range(0, music.Count);
    } while (rand == lastPlayed);
    return rand;
  }
}
