using System;
using DG.Tweening;
using Features.Targets.Data;
using Features.Targets.Scripts.Animations;
using UnityEngine;

namespace Features.Targets.Scripts.Elements
{
  public class TargetView : MonoBehaviour
  {
    [SerializeField] private TargetViewShakeSettings shakeSettings;
    
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
      
      shakeTweener = view.transform.DOShakePosition(shakeSettings.Duration, shakeSettings.Strength, shakeSettings.Vibrato).OnComplete(OnEndShake);
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