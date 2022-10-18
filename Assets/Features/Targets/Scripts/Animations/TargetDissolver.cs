using System;
using System.Collections;
using Features.Targets.Data;
using UnityEngine;

namespace Features.Targets.Scripts.Animations
{
  public class TargetDissolver : MonoBehaviour
  {
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private TargetDissolveSettings settings;

    private MaterialDissolveProcess[] processes;

    private void Awake()
    {
      processes = new MaterialDissolveProcess[targetRenderer.materials.Length];
      for (int i = 0; i < targetRenderer.materials.Length; i++)
      {
        processes[i] = new MaterialDissolveProcess(targetRenderer.materials[i], settings.AlphaClipParameterName, settings.AnimationEase );
      }
    }

    public void Dissolve(Action callback = null)
    {
      StartProcesses(settings.Duration, settings.MinAlphaClipValue);
      StartCoroutine(WaitEndProcesses(callback));
    }

    public void Appear(Action callback = null)
    {
      StartProcesses(settings.Duration, settings.MaxAlphaClipValue);
      StartCoroutine(WaitEndProcesses(callback));
    }

    private IEnumerator WaitEndProcesses(Action callback)
    {
      while (IsHaveAnimatedProcess())
      {
        yield return null;
      }
      
      callback?.Invoke();
    }

    private void StartProcesses(float duration, float alphaEndValue)
    {
      for (int i = 0; i < processes.Length; i++)
      {
        processes[i].StartProcess(duration, alphaEndValue);
      }
    }

    private bool IsHaveAnimatedProcess()
    {
      for (int i = 0; i < processes.Length; i++)
      {
        if (processes[i].IsAnimating)
          return true;
      }

      return false;
    }
  }
}