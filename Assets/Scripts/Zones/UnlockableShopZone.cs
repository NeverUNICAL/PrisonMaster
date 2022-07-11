using UnityEngine;

namespace Agava.IdleGame
{
    public class UnlockableShopZone : UnlockableObject
    {
        [SerializeField] private GameObject _shopAndFactoryRoot;

        public override GameObject Unlock(Transform parent, bool onLoad, string guid)
        {
            _shopAndFactoryRoot.SetActive(true);
            return _shopAndFactoryRoot;
        }
    }
}