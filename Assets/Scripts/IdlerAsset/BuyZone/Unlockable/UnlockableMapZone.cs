using UnityEngine;
using DG.Tweening;

namespace Agava.IdleGame
{
    public abstract class UnlockableMapZone : UnlockableObject
    {
        [SerializeField] protected NormalBuyZonePresenter[] NextZones;
        [SerializeField] protected NormalBuyZonePresenter[] BuyZones;

        [SerializeField] protected RoomBuyZone Room;
        [SerializeField] protected Transform RoomEnvirnoment;
        [SerializeField] protected LevelBuyZone NextLevel;

        private float _durationAnimation = 0.5f;
        private Vector3 _scaleTarget = new Vector3(1.2f, 1.2f, 1.2f);
        private  Vector3 _scaleTargetForRoomZone = new Vector3(1.1f, 1.1f, 1.1f);

        protected bool IsUnlockRoom = false;
        protected int Counter = 0;
        protected Vector3 DefaultBuyZoneScale;

        protected void OnEnable()
        {
            for (int i = 0; i < BuyZones.Length; i++)
                BuyZones[i].Unlocked += Unlock;

            if (NextLevel != null)
                NextLevel.Unlocked += UnlockNextLevel;

            if (Room != null)
                Room.Unlocked += UnlockRoom;
        }

        protected void OnDisable()
        {
            for (int i = 0; i < BuyZones.Length; i++)
                BuyZones[i].Unlocked -= Unlock;

            if (NextLevel != null)
                NextLevel.Unlocked -= UnlockNextLevel;

            if (Room != null)
                Room.Unlocked -= UnlockRoom;
        }

        public override GameObject Unlock(Transform parent, bool onLoad, string guid)
        {
            if (gameObject.activeInHierarchy == false)
                gameObject.SetActive(true);

            return gameObject;
        }

        public abstract void Unlock(BuyZonePresenter buyZone);

        public abstract void UnlockNextLevel(BuyZonePresenter buyZone);

        protected void AnimationScale(Transform buyZone)
        {
            DefaultBuyZoneScale = buyZone.localScale;
            Sequence sequence = DOTween.Sequence();
            buyZone.gameObject.SetActive(true);
            buyZone.localScale = new Vector3(0, 0, 0);
            sequence.Append(buyZone.DOScale(_scaleTarget, _durationAnimation));

            if (DefaultBuyZoneScale != Vector3.zero)
                sequence.Append(buyZone.DOScale(DefaultBuyZoneScale, _durationAnimation));
            else
                sequence.Append(buyZone.DOScale(Vector3.one, _durationAnimation));
        }

        protected void UnlockRoom(BuyZonePresenter buyZone)
        {
            IsUnlockRoom = true;
            Counter = 1;
            Unlock(buyZone);
            AnimationScale(RoomEnvirnoment);
        }

        protected void UnlockNextLevelZone()
        {
            OutlineZone outline = NextLevel.GetComponentInChildren<OutlineZone>();
            AnimationScale(NextLevel.transform);
            outline.transform.DOScale(_scaleTarget, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }

        protected void AnimationOutlineRoomZone()
        {
            OutlineZone outline = Room.GetComponentInChildren<OutlineZone>();
            AnimationScale(Room.transform);
            outline.transform.DOScale(_scaleTargetForRoomZone, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
    }
}