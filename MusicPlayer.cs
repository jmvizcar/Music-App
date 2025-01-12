using NAudio.Wave;
using System.Collections.Generic;

public class MusicPlayer :IDisposable
{
  private readonly WaveOutEvent outputDevice;
  private string path;
  private bool running;
  // Variable to hold the full list of albums currently in the Music directory.
  private List<string> musicDirect;
  public List<string> currentPlaylist;
  public string CurrentSong
  {get; set;}
  public double CurrentTime
  {get; set;}

  public bool Shuffle
  {get; set;}

  public MusicPlayer()
  {
    outputDevice = new WaveOutEvent();
    path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
    // Adds all mp3 files to the directory list
    musicDirect = new List<string>(Directory.GetFileSystemEntries(path, "*.mp3", SearchOption.AllDirectories));
    // Adds all m4a files to the directory list
    musicDirect.AddRange(Directory.GetFileSystemEntries(path, "*.m4a", SearchOption.AllDirectories));
    currentPlaylist = musicDirect;    
    CurrentSong = "";
    CurrentTime = 0;
    Shuffle = false;
    running = true;
  }
  public void Menu()
  {
    while(running)
    {
      int option = 0;
      Console.Write("1) Play\n0) Exit\nSelect what you would like to do: ");
      try
      {
        option = Convert.ToInt32(Console.ReadLine());
      }
      catch (FormatException)
      {
        Console.WriteLine("That is not an integer.");
        option = Int32.MaxValue;
      }
      switch(option)
      {
        case 1: this.Play(this.currentPlaylist[99]); break;
        case 0: running = false; break;
      }
    }
  }
  public void Play(string songmp3)
  {
    bool playing;
    var audioFile = new AudioFileReader(songmp3);
    playing = true;
    outputDevice.Init(audioFile);
    outputDevice.Play();
    Console.WriteLine($"Currently playing {songmp3}.");
    while (outputDevice.PlaybackState == PlaybackState.Playing && playing)
    {
      Console.Write("Hit Enter to stop playing: ");
      Console.ReadLine();
      playing = false;
    }
    audioFile.Dispose();
  }

  public void Dispose()
  {
    outputDevice.Dispose();
  }
}