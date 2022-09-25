using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MoneyConteiner : MonoBehaviour
{
    [SerializeField] private float _cooldawn = 0.5f;

    private Money[] _moneys;
    private bool _isWorking;
    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_cooldawn);
        _moneys = new Money[transform.childCount];

        for (int i = 0; i < _moneys.Length; i++)
            _moneys[i] = transform.GetChild(i).GetComponent<Money>();
    }

    public void GetMoney(SoftCurrencyHolder playerBalance)
    {
        StartCoroutine(TryGetMoney(playerBalance));
    }

    public void ChangeWorking(bool value)
    {
        _isWorking = value;
    }

    private IEnumerator TryGetMoney(SoftCurrencyHolder playerBalance)
    {
        while(_isWorking)
        {
            for (int i = 0; i < _moneys.Length; i++)
            {
                if (_moneys[i].gameObject.activeInHierarchy)
                {
                    _moneys[i].OnCollected(playerBalance.transform);
                    playerBalance.Add(_moneys[i].Reward);
                }
            }

            yield return _waitForSeconds;
        }
    }
}
