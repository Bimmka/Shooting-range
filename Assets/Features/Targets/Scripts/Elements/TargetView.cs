using System;
using UnityEngine;

namespace Features.Targets.Scripts.Elements
{
  public class TargetView : MonoBehaviour
  {
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetView(Sprite view)
    {
      spriteRenderer.sprite = view;
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
      
    }
  }
}