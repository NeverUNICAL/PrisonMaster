using UnityEngine;

namespace Agava.IdleGame
{
    public class UnlockableMapZone : UnlockableObject
    {
        [SerializeField] private GameObject _mapRoot;
        [SerializeField] private GameObject _objectForTurnOff;

        public override GameObject Unlock(Transform parent, bool onLoad, string guid)
        {
            if(_objectForTurnOff != null)
                _objectForTurnOff.SetActive(false);
            
            _mapRoot.SetActive(true);
            return _mapRoot;
        }
    }
}