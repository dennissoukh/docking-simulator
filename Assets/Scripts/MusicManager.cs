using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
  private AudioSource source;
  private void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
    source = GetComponent<AudioSource>();
  }

  public void PlayMusic()
  {
    if (source.isPlaying) return;
    source.Play();
  }

  public void StopMusic()
  {
    source.Stop();
  }
}