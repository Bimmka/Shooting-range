using System;

namespace Features.Targets.Scripts.HP
{
  public class TargetHP
  {
    private readonly int maxHealth;
    
    private int currentHealth;

    public event Action<int, int> Changed;
    public event Action Overed;
    
    public TargetHP(int maxHealth)
    {
      this.maxHealth = maxHealth;
      currentHealth = maxHealth;
    }

    public void Decrease(int damage)
    {
      currentHealth -= damage;
      NotifyAboutChange();

      if (currentHealth <= 0)
        NotifyAboutOver();
    }

    public void Restore() => 
      currentHealth = maxHealth;

    private void NotifyAboutChange() => 
      Changed?.Invoke(currentHealth, maxHealth);

    private void NotifyAboutOver() => 
      Overed?.Invoke();
  }
}