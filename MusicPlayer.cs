using NAudio.Wave;
using System.Collections.Generic;

public class MusicPlayer
{
  public void Play(string songmp3){
    using(var audioFile = new AudioFileReader(songmp3))
    using(var outputDevice = new WaveOutEvent())
    {
      outputDevice.Init(audioFile);
      outputDevice.Play();
      while (outputDevice.PlaybackState == PlaybackState.Playing)
      {
        Thread.Sleep(1000);
      }
    }
  }
}