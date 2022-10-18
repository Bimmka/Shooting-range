using System.Collections;
using DG.Tweening;
using Features.Targets.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Targets.Scripts.HP
{
  public class TargetHPDisplayer : MonoBehaviour
  {
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image fillBar;
    [SerializeField] private TargetHPDisplaySettings settings;
    
    private TargetHP hp;

    private Sequence showSequence;

    public void Construct(TargetHP hp, Camera mainCamera)
    {
      this.hp = hp;
      this.hp.Changed += Display;
      canvas.worldCamera = mainCamera;
    }

    private void OnDestroy()
    {
      hp.Changed -= Display;
    }

    private void Display(int currentHealth, int maxHealth)
    {
      if (showSequence != null && showSequence.IsPlaying())
        RestartSequence(currentHealth, maxHealth);
      else
        StartSequence(currentHealth, maxHealth);
      
    
    }

    private void StartSequence(int currentHealth, int maxHealth)
    {
      showSequence = DOTween.Sequence();
      showSequence
        .Append(canvasGroup.DOFade(1f, settings.AppearDuration))
        .Append(fillBar.DOFillAmount(currentHealth * 1f / maxHealth, settings.ValueChangeDuration))
        .Append(canvasGroup.DOFade(0f, settings.DisappearDuration));
    }

    private void RestartSequence(int currentHealth, int maxHealth)
    {
      showSequence.Kill();
      showSequence = DOTween.Sequence();
      showSequence
        .Append(fillBar.DOFillAmount(currentHealth * 1f / maxHealth, settings.ValueChangeDuration))
        .Append(canvasGroup.DOFade(0f, settings.DisappearDuration));
    }
  }
}