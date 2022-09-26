using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace Agava.IdleGame.Model
{
    [Serializable]
    public class TutorialProgress : SavedObject<TutorialProgress>
    {
        [SerializeField] private Progress _progress;
        
        public TutorialProgress(string guid)
            : base(guid)
        {
            _progress = new Progress();
        }
        
        public int TutorialLevel => _progress.TutorialLevel;
        public int PoolLevel => _progress.PoolLevel;
        public int ArrowLevel => _progress.ArrowLevel;
        public int PassedPrisonersCount => _progress.PassedPrisonersCount;
        public bool IsHrBuyed => _progress.IsHrBuyed;
        public bool IsTutorialCompleted => _progress.IsTutorialCompleted;
        
        public void SetTutorialLevel(int value)
        {
            _progress.SetTutorialLevel(value);
        }
        
        public void SetPoollevel(int value)
        {
            _progress.SetPoollevel(value);
        }
        
        public void SetArrowLevel(int value)
        {
            _progress.SetArrowLevel(value);
        }
        
        public void SetPassedPrisonersCount(int value)
        {
            _progress.SetPassedPrisonersCount(value);
        }

        public void SetHRBuyed()
        {
            _progress.SetHRBuyed();
        }
        
        public void SetTutorialComplete()
        {
            _progress.SetTutorialComplete();
        }
        
        protected override void OnLoad(TutorialProgress loadedObject)
        {
            _progress = loadedObject._progress;
        }
    }
}