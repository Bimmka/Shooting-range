using System;
using System.Collections.Generic;
using Features.Targets.Scripts.Elements;

namespace Features.Targets.Scripts.Base
{
    public class TargetsContainer
    {
        public List<TargetPresenter> Presenters { get; }

        public event Action TargetDied;
        
        public TargetsContainer()
        {
            Presenters = new List<TargetPresenter>(30);
        }

        public void AddTarget(TargetPresenter presenter)
        {
            Presenters.Add(presenter);
            presenter.Died += OnPresenterDied;
        }

        public void Cleanup()
        {
            for (int i = 0; i < Presenters.Count; i++)
            {
                Presenters[i].Died -= OnPresenterDied;
            }
        }

        public bool IsContainsDisabledPresenter(TargetType type)
        {
            for (int i = 0; i < Presenters.Count; i++)
            {
                if (Presenters[i].Type == type)
                    return true;
            }

            return false;
        }
        
        public TargetPresenter PresenterOrNull(TargetType type)
        {
            for (int i = 0; i < Presenters.Count; i++)
            {
                if (Presenters[i].Type == type)
                    return Presenters[i];
            }

            return null;
        }

        private void OnPresenterDied() => 
            TargetDied?.Invoke();
    }
}
