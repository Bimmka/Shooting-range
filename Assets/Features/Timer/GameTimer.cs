using System;
using System.Collections;
using Features.Services.Coroutine;
using Features.Services.Pause;
using UnityEngine;

namespace Features.Timer
{
  public class GameTimer : IPaused
  {
    private readonly ICoroutineRunner coroutineRunner;
    private readonly IPauseService pauseService;
    private Coroutine timerCoroutine;
    public int LeftSeconds { get; private set; }

    public event Action<int> Changed;
    public event Action TimeOut;
    
    public GameTimer(ICoroutineRunner coroutineRunner, IPauseService pauseService)
    {
      this.coroutineRunner = coroutineRunner;
      this.pauseService = pauseService;
      pauseService.Register(this);
    }

    public void Cleanup()
    {
      pauseService.Unregister(this);
    }

    public void Start(int seconds)
    {
      LeftSeconds = seconds;
      NotifyAboutChange();
      if (timerCoroutine != null)
        coroutineRunner.StopCoroutine(timerCoroutine);

      timerCoroutine = coroutineRunner.StartCoroutine(UpdateTime());
    }

    public void Stop()
    {
      if (timerCoroutine == null)
        return;
      
      coroutineRunner.StopCoroutine(timerCoroutine);
      ResetCoroutine();
    }

    public void Pause()
    {
      coroutineRunner.StopCoroutine(timerCoroutine);
    }

    public void Unpause()
    {
      timerCoroutine = coroutineRunner.StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
      WaitForSeconds delay = new WaitForSeconds(1f);
      while (LeftSeconds > 0)
      {
        yield return delay;
        LeftSeconds--;
        NotifyAboutChange();
      }

      ResetCoroutine();
      TimeOut?.Invoke();
    }

    private void NotifyAboutChange() => 
      Changed?.Invoke(LeftSeconds);

    private void ResetCoroutine() => 
      timerCoroutine = null;
  }
}