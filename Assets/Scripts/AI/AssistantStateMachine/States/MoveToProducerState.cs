using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Agava.IdleGame;
using System.Linq;

public class MoveToProducerState : AssistantState
{
    [SerializeField] private float _delay = 1f;
    [SerializeField] private Vector3 _offset;
    private Transform _target;
    private Coroutine _coroutineInJob;
    private List<StackPresenter> _stackPresenters = new List<StackPresenter>();

    private void OnEnable()
    {
        //_offset = new Vector3(Random.Range(-0.5f, -1.3f), 0, Random.Range(-1.3f, 1.3f));
        //Debug.Log(_offset);
        _coroutineInJob = StartCoroutine(FindMinStack());
    }

    private IEnumerator FindMinStack()
    {
        while (true)
        {
            for (int i = 0; i < Assistant.ProducerItemCreators.Count; i++)
            {
                if (Assistant.ProducerItemCreators[i].gameObject.activeInHierarchy)
                    _stackPresenters.Add(Assistant.ProducerItemCreators[i]);
            }

            _stackPresenters = _stackPresenters.OrderBy(consumer => consumer.Count).ToList();
            _target = _stackPresenters[_stackPresenters.Count - 1].GetComponentInChildren<StackPresenterTrigger>().transform;
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