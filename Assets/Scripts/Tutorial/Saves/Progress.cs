using System;
using UnityEngine;
using UnityEngine.AI;

namespace Agava.IdleGame.Model
{
    [Serializable]
    public class Progress
    {
        [SerializeField] private int _tutorialLevel;
        [SerializeField] private int _passedPrisonersCount;
        [SerializeField] private bool _isHRBuyed;
        [SerializeField] private bool _isTutorialCompleted;
        [SerializeField] private int _poolLevel;
        [SerializeField] private int _arrowLevel;

        public Progress()
        {
            
        }
        
        public int TutorialLevel => _tutorialLevel;
        public int PoolLevel => _poolLevel;
        public int ArrowLevel => _arrowLevel;
        public int PassedPrisonersCount => _passedPrisonersCount;
        public bool IsHrBuyed => _isHRBuyed;
        public bool IsTutorialCompleted => _isTutorialCompleted;

        public void SetTutorialLevel(int value)
        {
            _tutorialLevel = value;
        }
        
        public void SetPoollevel(int value)
        {
            _poolLevel = value;
        }
        
        public void SetArrowLevel(int value)
        {
            _arrowLevel = value;
        }
        
        public void SetPassedPrisonersCount(int value)
        {
            _passedPrisonersCount = value;
        }
        
        public void SetHRBuyed()
        {
            _isHRBuyed = true;
        }
        
        public void SetTutorialComplete()
        {
            _isTutorialCompleted = true;
        }
    }
}