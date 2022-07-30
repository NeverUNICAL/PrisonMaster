using UnityEngine;
using DG.Tweening;

namespace Agava.IdleGame
{
    public class UnlockableMapZone : UnlockableObject
    {
        [SerializeField] private GameObject _mapRoot;

        [SerializeField] private NormalBuyZonePresenter[] _nextZones;
        [SerializeField] private NormalBuyZonePresenter[] _buyZones;

        [SerializeField] private int _levelId;
        [SerializeField] private RoomBuyZone _room;
        [SerializeField] private LevelBuyZone _nextLevel;
        [SerializeField] private Vector3 _scaleTarget = new Vector3(1.2f, 1.2f, 1.2f);
        [SerializeField] private Vector3 _scaleTargetForRoomZone = new Vector3(1.1f, 1.1f, 1.1f);
        [SerializeField] private float _durationAnimation = 0.5f;

        private bool _isUnlockRoom = false;
        private int _counter = 0;

        private void OnEnable()
        {
            for (int i = 0; i < _buyZones.Length; i++)
            {
                if (_levelId < 2)
                    _buyZones[i].Unlocked += Unlock;
                else
                    _buyZones[i].Unlocked += BonusZone;
            }

            if (_nextLevel != null)
                _nextLevel.Unlocked += UnlockNextLevel;

            if (_room != null)
                _room.Unlocked += UnlockRoom;
        }

        private void OnDisable()
        {
            for (int i = 0; i < _buyZones.Length; i++)
            {
                if (_levelId < 2)
                    _buyZones[i].Unlocked -= Unlock;
                else
                    _buyZones[i].Unlocked -= BonusZone;
            }

            if (_nextLevel != null)
                _nextLevel.Unlocked -= UnlockNextLevel;

            if (_room != null)
                _room.Unlocked -= UnlockRoom;
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
                        if (_isUnlockRoom)
                            AnimationScale(_buyZones[i].transform);

                        tempCounter++;
                    }
                    else
                    {
                        _counter = 0;
                    }

                    if (_nextLevel != null && _nextLevel.gameObject.activeInHierarchy == false)
                        UnlockNextLevelZone();
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
                        AnimationScale(_room.transform);
                        AnimationOutlineRoomZone();
                        AnimationScale(_nextZones[i].transform);
                        tempCounter++;
                    }
                }
            }
        }

        private void BonusZone(BuyZonePresenter buyZone)
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
                        tempCounter++;
                    }
                    else
                    {
                        _counter = 0;
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

        private void UnlockRoom(BuyZonePresenter buyZone)
        {
            _isUnlockRoom = true;
            _counter = 1;
            Unlock(buyZone);
        }

        private void UnlockNextLevelZone()
        {
            if (_isUnlockRoom == false)
            {
                OutlineZone outline = _nextLevel.GetComponentInChildren<OutlineZone>();
                AnimationScale(_nextLevel.transform);
                outline.transform.DOScale(_scaleTarget, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            }
        }

        private void AnimationOutlineRoomZone()
        {
            OutlineZone outline = _room.GetComponentInChildren<OutlineZone>();
            AnimationScale(_room.transform);
            outline.transform.DOScale(_scaleTargetForRoomZone, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
    }
}