using Assets.Source.Stack;
using UnityEngine;

namespace Assets.Source.Stickmans
{
    [RequireComponent(typeof(HandStateHandler))]
    public class Mover : MonoBehaviour
    {
        protected HandStateHandler _stackHandler;

        public bool IsEmptyHand { get; private set; } = true;


        private void OnEnable()
        {
            _stackHandler = GetComponent<HandStateHandler>();
            _stackHandler.HandsStateChanged += SetHandState;
        }

        private void OnDisable()
        {
            _stackHandler.HandsStateChanged -= SetHandState;
        }

        protected void SetHandState(bool isEmptyHand)
        {
            IsEmptyHand = isEmptyHand;
        }
    }
}
