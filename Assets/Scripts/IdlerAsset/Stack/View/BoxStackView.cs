using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Agava.IdleGame.Model;
using System.Collections;
using UnityEngine.Events;

namespace Agava.IdleGame
{
    public class BoxStackView : StackView
    {
        [Space(15f)]
        [SerializeField] private Vector3Int _size = Vector3Int.one;
        [SerializeField] private Vector3 _distanceBetweenObjects = Vector3.one;

#if UNITY_EDITOR
        [Space(5)]
        [SerializeField] private bool _drawGizmos;

        
        [SerializeField] private float _delay = 1f;
        [SerializeField] private Material _redMaterial;
        [SerializeField] private ParticleSystem _poof;
        [SerializeField] private Material[] _materials;
        [SerializeField] private PlayerFinishTrigger _finishTrigger;
        [SerializeField] private PlayerStackPresenter _playerStack;

        private bool _poofActivated = true;
        
        public event UnityAction SuitReached;

        private void OnEnable()
        {
            if (_playerStack != null)
                _playerStack.StackEmpty += OnStackEmpty;
        }

        private void OnDisable()
        {
            if (_playerStack != null)
                _playerStack.StackEmpty -= OnStackEmpty;
        }

        private void OnDrawGizmos()
        {
            if (_drawGizmos == false)
                return;

            Gizmos.color = Color.red;

            for (int y = 0; y < _size.y; y++)
            {
                for (int x = 0; x < _size.x; x++)
                {
                    for (int z = 0; z < _size.z; z++)
                    {
                        var position = transform.TransformPoint(
                            new Vector3(x * _distanceBetweenObjects.x, y * _distanceBetweenObjects.y, z * _distanceBetweenObjects.z));

                        Gizmos.DrawSphere(position, 0.2f);
                    }
                }
            }
        }
#endif

        private void OnStackEmpty()
        {
            _poofActivated = true;
            SortStack();
        }

        protected override Vector3 CalculateAddEndPosition(Transform container, Transform stackable)
        {
            var index = container.childCount;

            return Vector3.Scale(PositionByIndex(index), _distanceBetweenObjects);
        }

        protected override void Sort(IEnumerable<StackableObject> unsortedStackables, float animationDuration)
        {

            int index = 0;
            foreach (var stackable in unsortedStackables)
            {
                var position = Vector3.Scale(PositionByIndex(index), _distanceBetweenObjects);

                stackable.View.DOComplete(true);
                stackable.View.DOLocalMove(position, animationDuration);

                index++;

                if (IsShop)
                {
                    if (_playerStack.Count == 0)
                    {
                    if (stackable.Layer == 4)
                        StartCoroutine(Delay(stackable, false));

                    if (stackable.Layer == 5)
                        StartCoroutine(Delay(stackable, true));
                    }
                }
            }
        }

        private IEnumerator Delay(StackableObject stackable, bool isFood)
        {
            yield return new WaitForSeconds(_delay);

            var mat = stackable.View.GetComponentInChildren<MeshRenderer>();
            Material[] materials = mat.materials;
            Material[] redMaterials = mat.materials;

            for (int i = 0; i < redMaterials.Length; i++)
                redMaterials[i] = _redMaterial;

            yield return new WaitForSeconds(_delay);

            mat.materials = redMaterials;

            yield return new WaitForSeconds(_delay);

            mat.materials = materials;

            yield return new WaitForSeconds(_delay);

            mat.materials = redMaterials;

            yield return new WaitForSeconds(_delay);

            mat.materials = materials;

            yield return new WaitForSeconds(_delay);

            mat.materials = redMaterials;

            yield return new WaitForSeconds(_delay);

            mat.materials = materials;

            yield return new WaitForSeconds(_delay);

            Remove(stackable);

            if (_poofActivated)
                _poof.Play();

            yield return new WaitForSeconds(_delay);

            if (isFood)
                _finishTrigger.ChangeFinishState();

            if (_poofActivated)
                SuitReached?.Invoke();

            _poofActivated = false;
        }

        private Vector3 PositionByIndex(int index)
        {
            var x = index % _size.x;
            var y = index / (_size.x * _size.z);
            var z = (index / _size.x) % _size.z;

            return new Vector3(x, y, z);
        }
    }
}