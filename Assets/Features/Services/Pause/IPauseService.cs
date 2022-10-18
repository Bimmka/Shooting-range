namespace Features.Services.Pause
{
  public interface IPauseService
  {
    void Register(IPaused paused);
    void Unregister(IPaused paused);
    void Pause();
    void Unpause();
  }
}