using System;
using System.Collections.Generic;
using Features.Targets.Scripts.Elements;

namespace Features.Targets.Scripts.Base
{
    public class TargetsContainer
    {
        private readonly List<TargetPresenter> presenters;
        public event Action TargetDied;
        
        public TargetsContainer()
        {
            presenters = new List<TargetPresenter>(30);
        }

        public void AddTarget(TargetPresenter presenter)
        {
            presenters.Add(presenter);
            presenter.Died += OnPresenterDied;
        }

        public void Cleanup()
        {
            for (int i = 0; i < presenters.Count; i++)
            {
                presenters[i].Died -= OnPresenterDied;
            }
        }

        public void DisableTargets()
        {
            for (int i = 0; i < presenters.Count; i++)
            {
                presenters[i].Disable();
            }
        }

        public bool IsContainsDisabledPresenter(TargetType type)
        {
            for (int i = 0; i < presenters.Count; i++)
            {
                if (IsDisabled(presenters[i].Status) && IsSameType(presenters[i].Type, type))
                    return true;
            }

            return false;
        }

        public TargetPresenter PresenterOrNull(TargetType type)
        {
            for (int i = 0; i < presenters.Count; i++)
            {
                if (IsDisabled(presenters[i].Status) && IsSameType(presenters[i].Type, type))
                    return presenters[i];
            }

            return null;
        }

        private void OnPresenterDied() => 
            TargetDied?.Invoke();

        private bool IsDisabled(TargetStatus status) => 
            status == TargetStatus.Disabled;

        private bool IsSameType(TargetType presenterType, TargetType type) => 
            presenterType == type;
    }
}
