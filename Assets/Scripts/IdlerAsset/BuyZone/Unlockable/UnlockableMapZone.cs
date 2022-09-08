using UnityEngine;
using DG.Tweening;

namespace Agava.IdleGame
{
    public abstract class UnlockableMapZone : UnlockableObject
    {
        [SerializeField] protected GameObject MapRoot;

        [SerializeField] protected NormalBuyZonePresenter[] NextZones;
        [SerializeField] protected NormalBuyZonePresenter[] BuyZones;

        [SerializeField] protected RoomBuyZone Room;
        [SerializeField] protected Transform RoomEnvirnoment;
        [SerializeField] protected LevelBuyZone NextLevel;
        [SerializeField] protected Vector3 ScaleTarget = new Vector3(1.2f, 1.2f, 1.2f);
        [SerializeField] protected Vector3 ScaleTargetForRoomZone = new Vector3(1.1f, 1.1f, 1.1f);
        [SerializeField] protected float DurationAnimation = 0.5f;

        protected bool IsUnlockRoom = false;
        protected int Counter = 0;
        protected Vector3 DefaultBuyZoneScale;

        private void OnEnable()
        {
            for (int i = 0; i < BuyZones.Length; i++)
                BuyZones[i].Unlocked += Unlock;

            if (NextLevel != null)
                NextLevel.Unlocked += UnlockNextLevel;

            if (Room != null)
                Room.Unlocked += UnlockRoom;
        }

        private void OnDisable()
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
            if (MapRoot.activeInHierarchy == false)
                MapRoot.SetActive(true);

            return MapRoot;
        }

        public abstract void Unlock(BuyZonePresenter buyZone);

        public abstract void UnlockNextLevel(BuyZonePresenter buyZone);

        protected void AnimationScale(Transform buyZone)
        {
            DefaultBuyZoneScale = buyZone.localScale;
            Sequence sequence = DOTween.Sequence();
            buyZone.gameObject.SetActive(true);
            buyZone.localScale = new Vector3(0, 0, 0);
            sequence.Append(buyZone.DOScale(ScaleTarget, DurationAnimation));

            if (DefaultBuyZoneScale != Vector3.zero)
                sequence.Append(buyZone.DOScale(DefaultBuyZoneScale, DurationAnimation));
            else
                sequence.Append(buyZone.DOScale(Vector3.one, DurationAnimation));
        }

        protected void UnlockRoom(BuyZonePresenter buyZone)
        {
            Debug.Log("unlock");
            IsUnlockRoom = true;
            Counter = 1;
            Unlock(buyZone);
            AnimationScale(RoomEnvirnoment);
        }

        protected void UnlockNextLevelZone()
        {
            OutlineZone outline = NextLevel.GetComponentInChildren<OutlineZone>();
            AnimationScale(NextLevel.transform);
            outline.transform.DOScale(ScaleTarget, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }

        protected void AnimationOutlineRoomZone()
        {
            OutlineZone outline = Room.GetComponentInChildren<OutlineZone>();
            AnimationScale(Room.transform);
            outline.transform.DOScale(ScaleTargetForRoomZone, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
    }
}