using Features.Targets.Scripts.Elements;

namespace Features.Targets.Scripts.Base
{
  public class TargetsMoveSimulator
  {
    private readonly TargetsContainer targetsContainer;

    public TargetsMoveSimulator(TargetsContainer targetsContainer)
    {
      this.targetsContainer = targetsContainer;
    }

    public void Tick()
    {
      for (int i = 0; i < targetsContainer.Presenters.Count; i++)
      {
        if (IsCanMove(targetsContainer.Presenters[i]))
          Move(targetsContainer.Presenters[i]);
      }
    }

    private bool IsCanMove(TargetPresenter presenter) => 
      presenter.Status == TargetStatus.Moving;

    private void Move(TargetPresenter presenter)
    {
      presenter.Move();
    }
  }
}