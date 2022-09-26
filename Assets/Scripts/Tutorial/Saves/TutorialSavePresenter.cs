using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Agava.IdleGame.Model;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Agava.IdleGame
{
    public class TutorialSavePresenter : GUIDMonoBehaviour
    {
        [Space(10)]
        
        private TutorialProgress _tutorialProgress;
        
        public int TutorialLevel => _tutorialProgress.TutorialLevel;
        public int PoolLevel => _tutorialProgress.PoolLevel;
        public int ArrowLevel => _tutorialProgress.ArrowLevel;
        public int PassedPrisonersCount => _tutorialProgress.PassedPrisonersCount;
        public bool IsHrBuyed => _tutorialProgress.IsHrBuyed;
        public bool IsTutorialCompleted => _tutorialProgress.IsTutorialCompleted;
        
        public event UnityAction Loaded;
        
        private void Awake()
        {
            _tutorialProgress = new TutorialProgress(GUID);
        }

        private void Start()
        {
            _tutorialProgress.Load();
            OnLoad();
        }

        public void SetTutorialLevel()
        {
            _tutorialProgress.SetTutorialLevel(_tutorialProgress.TutorialLevel+1);
            _tutorialProgress.Save();
        }
        
        public void SetPoollevel(int value)
        {
            _tutorialProgress.SetPoollevel(value);
            _tutorialProgress.Save();
        }
        
        public void SetArrowLevel(int value)
        {
            _tutorialProgress.SetArrowLevel(value);
            _tutorialProgress.Save();
        }

        public void SetPassedPrisonersCount()
        {
            _tutorialProgress.SetPassedPrisonersCount(_tutorialProgress.PassedPrisonersCount+1);
            _tutorialProgress.Save();
        }
        
        public void SetHRBuyed()
        {
            _tutorialProgress.SetHRBuyed();
            _tutorialProgress.Save();
        }

        public void SetTutorialComplete()
        {
            _tutorialProgress.SetTutorialComplete();
            _tutorialProgress.Save();
        }
        
        private void OnLoad()
        {
            {
                Loaded?.Invoke();
            }
        }
    }
}