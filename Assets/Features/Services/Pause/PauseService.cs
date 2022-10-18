using System.Collections.Generic;

namespace Features.Services.Pause
{
  public class PauseService : IPauseService
  {
    private readonly List<IPaused> pausedObjects;

    public PauseService()
    {
      pausedObjects = new List<IPaused>(10);
    }
    
    public void Register(IPaused paused) => 
      pausedObjects.Add(paused);

    public void Unregister(IPaused paused)
    {
      if (pausedObjects.Contains(paused))
        pausedObjects.Remove(paused);
    }

    public void Pause()
    {
      for (int i = 0; i < pausedObjects.Count; i++)
      {
        pausedObjects[i].Pause();
      }
    }

    public void Unpause()
    {
      for (int i = 0; i < pausedObjects.Count; i++)
      {
        pausedObjects[i].Unpause();
      }
    }
  }
}