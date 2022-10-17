using System;
using System.Collections;
using Features.Services.Coroutine;
using UnityEngine;

namespace Features.Timer
{
  public class GameTimer
  {
    private readonly ICoroutineRunner coroutineRunner;
    private Coroutine timerCoroutine;
    public int LeftSeconds { get; private set; }

    public event Action<int> Changed;
    public event Action TimeOut;
    
    public GameTimer(ICoroutineRunner coroutineRunner)
    {
      this.coroutineRunner = coroutineRunner;
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