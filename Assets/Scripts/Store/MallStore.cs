using UnityEngine;

    public class MallStore : Store
    {
        [SerializeField] private StoreCollisionHandler _collisionHandler;

        private void OnEnable()
        {
            _collisionHandler.Triggered += OnBuyerBougth;
        }

        private void OnDisable()
        {
            _collisionHandler.Triggered -= OnBuyerBougth;            
        }

        private void OnBuyerBougth(Buyer buyer)
        {
            Debug.Log("eaz");
        }
    }

