using UnityEngine;
using DG.Tweening;

namespace Agava.IdleGame
{
    public class UnlockableMapZone : UnlockableObject
    {
        [SerializeField] protected GameObject MapRoot;

        public override GameObject Unlock(Transform parent, bool onLoad, string guid)
        {
            if (MapRoot.activeInHierarchy == false)
                MapRoot.SetActive(true);

            return MapRoot;
        }
    }
}