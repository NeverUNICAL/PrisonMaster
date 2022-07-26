using UnityEngine;
using DG.Tweening;

namespace Agava.IdleGame
{
    public class UnlockableMapZone : UnlockableObject
    {
        [SerializeField] private GameObject _mapRoot;

        [SerializeField] private NormalBuyZonePresenter[] _nextZones;
        [SerializeField] private NormalBuyZonePresenter[] _buyZones;
        [SerializeField] private RoomBuyZone _room;
        [SerializeField] private LevelBuyZone _nextLevel;
        [SerializeField] private Vector3 _scaleTarget = new Vector3(1.2f, 1.2f, 1.2f);
        [SerializeField] private float _durationAnimation = 0.5f;

        private int _counter = 0;

        private void OnEnable()
        {
            for (int i = 0; i < _buyZones.Length; i++)
            {
                _buyZones[i].Unlocked += Unlock;
            }

            if (_nextLevel != null)
                _nextLevel.Unlocked += UnlockNextLevel;
        }

        private void OnDisable()
        {
            for (int i = 0; i < _buyZones.Length; i++)
            {
                _buyZones[i].Unlocked -= Unlock;
            }

            if (_nextLevel != null)
                _nextLevel.Unlocked -= UnlockNextLevel;
        }

        public override GameObject Unlock(Transform parent, bool onLoad, string guid)
        {
            if (_mapRoot.activeInHierarchy == false)
                _mapRoot.SetActive(true);

            return _mapRoot;
        }

        private void Unlock(BuyZonePresenter buyZone)
        {
            int tempCounter = 0;
            int target = 2;
            _counter++;

            for (int i = 0; i < _buyZones.Length; i++)
            {
                if (_buyZones[i].gameObject.activeInHierarchy == false && _counter == target)
                {
                    if (tempCounter < target)
                    {
                        AnimationScale(_buyZones[i].transform);

                        if (_nextLevel != null && _nextLevel.gameObject.activeInHierarchy == false)
                        {
                            OutlineZone outline = _nextLevel.GetComponentInChildren<OutlineZone>();
                            AnimationScale(_nextLevel.transform);
                            outline.transform.DOScale(_scaleTarget, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
                        }

                        if (_room && _room.gameObject.activeInHierarchy == false)
                            AnimationScale(_room.transform);

                        tempCounter++;
                    }
                    else
                    {
                        _counter = 0;
                    }
                }

            }
        }

        private void UnlockNextLevel(BuyZonePresenter buyZone)
        {
            int tempCounter = 0;
            int target = 2;

            for (int i = 0; i < _nextZones.Length; i++)
            {
                if (_nextZones[i].gameObject.activeInHierarchy == false)
                {
                    if (tempCounter < target)
                    {
                        AnimationScale(_nextZones[i].transform);
                        tempCounter++;
                    }
                }
            }
        }

        private void AnimationScale(Transform buyZone)
        {
            Sequence sequence = DOTween.Sequence();
            buyZone.gameObject.SetActive(true);
            buyZone.localScale = new Vector3(0, 0, 0);
            sequence.Append(buyZone.DOScale(_scaleTarget, _durationAnimation));
            sequence.Append(buyZone.DOScale(new Vector3(1, 1, 1), _durationAnimation));
        }
    }
}