using System;
using DG.Tweening;
using UnityEngine;

namespace Features.Targets.Scripts.Elements
{
  public class TargetView : MonoBehaviour
  {
    private GameObject view;

    private Tweener shakeTweener;
    public void Initialize(GameObject view)
    {
      this.view = view;
    }

    public void Show(Action callback = null)
    {
      gameObject.SetActive(true);
      callback?.Invoke();
    }

    public void Hide(Action callback = null)
    {
      gameObject.SetActive(false);
      callback?.Invoke();
    }

    public void DisplayHit()
    {
      if (shakeTweener != null && shakeTweener.IsPlaying())
        return;
      
      shakeTweener = view.transform.DOShakePosition(1f, 0.5f, 2, 50f).OnComplete(EndShake);
    }

    private void EndShake()
    {
      shakeTweener = null;
    }
  }
}