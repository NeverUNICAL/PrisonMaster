using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField] private Store _store;
    [SerializeField] private Money _moneyTemplate;
    [SerializeField] private Transform[] _points;
    [SerializeField] private int _maxCount;
    [SerializeField] private Transform _moneyConteiner;

    private Money[] _moneys;
    private int _currentCount;
    private int _counter = 0;
    private const float Delay = 0.2f;
    private float _delayForStopMoney = 2f;
    private Vector3 _defaultScale;

    public event UnityAction MoneySpawned;

    private void Awake()
    {
        _moneys = new Money[_maxCount];
        for (int i = 0; i < _moneys.Length; i++)
        {
            _moneys[i] = Instantiate(_moneyTemplate, _points[_counter].position, transform.rotation, _moneyConteiner);
            _moneys[i].SetConteiner(_moneyConteiner);
            _defaultScale = _moneys[i].transform.localScale;
            _moneys[i].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _store.Sold += Spawn;
    }

    private void OnDisable()
    {
        _store.Sold -= Spawn;
    }

    public void ReduceCount()
    {
        _currentCount--;
    }

    private void Spawn(int count)
    {
        StartCoroutine(MoneyGenerator(count));
    }

    private IEnumerator MoneyGenerator(int count)
    {
        var delay = new WaitForSeconds(Delay);

        for (int i = 0; i < count; i++)
        {
            yield return delay;
            if (_counter > _points.Length - 1)
                _counter = 0;

            if (_currentCount < _maxCount)
            {
                Money money = GetMoney();
                if (money != null)
                {
                    money.ChangeState(false);
                    money.transform.position = _points[_counter].transform.position;
                    money.transform.localScale = _defaultScale;
                    money.transform.rotation = Quaternion.Euler(0, 90f, 0);
                    money.gameObject.SetActive(true);
                    money.SetMoneySpawner(this);
                    _currentCount++;
                    _counter++;
                    money.DisableRigidbody(_delayForStopMoney);
                }

                MoneySpawned?.Invoke();
            }
        }
    }

    private Money GetMoney()
    {
        for (int i = 0; i < _moneys.Length; i++)
        {
            if (_moneys[i].gameObject.activeInHierarchy == false)
            {

                return _moneys[i];
            }
        }

        return null;
    }
}

