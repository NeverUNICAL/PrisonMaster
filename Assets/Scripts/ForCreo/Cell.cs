using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ForCreo
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Transform _prisonersConteiner;

        private Door _door;
        private Prisoner[] _prisoners;

        public Prisoner[] Prisoners => _prisoners;

        public event UnityAction<Cell> DoorOpened;

        private void Awake()
        {
            _door = GetComponentInChildren<Door>();

            if (_prisonersConteiner.childCount <= 0)
                return;

            _prisoners = new Prisoner[_prisonersConteiner.childCount];
            for (int i = 0; i < _prisoners.Length; i++)
                _prisoners[i] = _prisonersConteiner.GetChild(i).GetComponent<Prisoner>();
        }

        private void OnEnable()
        {
            _door.Opened += OnOpened;
        }

        private void OnDisable()
        {
            _door.Opened -= OnOpened;
        }

        private void OnOpened()
        {
            DoorOpened?.Invoke(this);
        }
    }
}