using System;
using DG.Tweening;
using Features.Targets.Scripts.Animations;
using UnityEngine;

namespace Features.Targets.Scripts.Elements
{
  public class TargetView : MonoBehaviour
  {
    private TargetDissolver view;

    private Tweener shakeTweener;

    private event Action savedCallback;
    public void Initialize(TargetDissolver view)
    {
      this.view = view;
    }

    public void Show(Action callback = null)
    {
      gameObject.SetActive(true);
      SaveCallback(callback);
      view.Appear(OnAppear);
    }

    public void Hide(Action callback = null)
    {
     SaveCallback(callback);
     view.Dissolve(OnHide);
    }

    public void DisplayHit()
    {
      if (shakeTweener != null && shakeTweener.IsPlaying())
        return;
      
      shakeTweener = view.transform.DOShakePosition(1f, 0.5f, 2, 50f).OnComplete(OnEndShake);
    }

    private void OnAppear()
    {
      savedCallback?.Invoke();
    }

    private void OnHide()
    {
      gameObject.SetActive(false);
      savedCallback?.Invoke();
    }

    private void OnEndShake() => 
      shakeTweener = null;

    private void SaveCallback(Action callback) => 
      savedCallback = callback;
  }
}