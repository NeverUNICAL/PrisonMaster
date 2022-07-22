using System.Collections;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField] private Store _store;
    [SerializeField] private Money _moneyTemplate;
    [SerializeField] private Transform[] _points;

    private int _counter = 0;
    private const float Delay = 0.1f;

    private void OnEnable()
    {
        _store.Sold += Spawn;
    }

    private void OnDisable()
    {
        _store.Sold -= Spawn;
    }

    private void Spawn(int count)
    {
        StartCoroutine(MoneyGenerator(count));
    }

    private IEnumerator MoneyGenerator(int count)
    {
        var delay = new WaitForSeconds(Delay);

        for (int i = 0; i < count * 2; i++)
        {
            yield return delay;
            if (_counter > _points.Length - 1)
                _counter = 0;
            
            Money money = Instantiate(_moneyTemplate, _points[_counter].position, transform.rotation, transform);
            _counter++;

            money.StopMove();
        }
    }
}

