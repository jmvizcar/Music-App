public static class Program
{
  public static void Main()
  {
    MusicPlayer MP = new MusicPlayer();
    MP.Play(MP.currentPlaylist[99]);
    Console.WriteLine("Hello World! I am alive!");
  }
}