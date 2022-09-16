using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Agava.IdleGame;
using System.Linq;

public class MoveToConsumerState : AssistantState
{
    [SerializeField] private float _delay = 1f;
    [SerializeField] private Vector3 _offset;

    private Transform _target;
    private Coroutine _coroutineInJob;
    private List<StackPresenter> _stackPresenters = new List<StackPresenter>();

    public Vector3 TargetPosition => _target.position + _offset;

    private void OnEnable()
    {
        _coroutineInJob = StartCoroutine(FindMinStack());
    }

    private IEnumerator FindMinStack()
    {
        while (true)
        {
            for (int i = 0; i < Assistant.ConsumersItemCreators.Count; i++)
            {
                if (Assistant.ConsumersItemCreators[i].gameObject.activeInHierarchy)
                    _stackPresenters.Add(Assistant.ConsumersItemCreators[i]);
            }

            _stackPresenters = _stackPresenters.OrderBy(consumer => consumer.Count).ToList();
            _target = _stackPresenters[0].transform;
            Assistant.ChangeTargetTransform(_target);
            Assistant.Move(_target.transform.position + _offset);

            yield return new WaitForSeconds(_delay);
        }
    }

    public void StopCoroutine()
    {
        StopCoroutine(_coroutineInJob);
    }
}
