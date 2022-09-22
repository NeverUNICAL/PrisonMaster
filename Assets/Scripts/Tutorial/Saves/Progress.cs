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

        public Progress()
        {
            
        }
        
        public int TutorialLevel => _tutorialLevel;
        public int PassedPrisonersCount => _passedPrisonersCount;
        public bool IsHrBuyed => _isHRBuyed;
        public bool IsTutorialCompleted => _isTutorialCompleted;
        
        public void SetTutorialLevel(int value)
        {
            _tutorialLevel = value;
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