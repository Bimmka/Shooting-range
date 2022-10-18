using DG.Tweening;
using UnityEngine;

namespace Features.Targets.Scripts.Animations
{
  public class MaterialDissolveProcess
  {
    private readonly Material rendererMaterial;
    private readonly string alphaClipParameterName;
    private readonly Ease processEase;

    public bool IsAnimating { get; private set; }

    public MaterialDissolveProcess(Material rendererMaterial, string alphaClipParameterName, Ease processEase)
    {
      this.rendererMaterial = rendererMaterial;
      this.alphaClipParameterName = alphaClipParameterName;
      this.processEase = processEase;
    }

    public void StartProcess(float duration, float endValue)
    {
      IsAnimating = true;
      rendererMaterial.DOFloat(endValue, alphaClipParameterName, duration).SetEase(processEase).OnComplete(OnFinishProcess);
    }
    
    private void OnFinishProcess()
    {
      IsAnimating = false;
    }
  }
}