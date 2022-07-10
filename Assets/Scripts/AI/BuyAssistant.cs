using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;
using Agava.IdleGame.Model;
using TMPro;

public class BuyAssistant : MonoBehaviour
{
    [SerializeField] private int _price = 10;
    [SerializeField] private GameObject _prefabAssistant;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _poolAssistants;
    [SerializeField] private TMP_Text _text;

    private Assistant[] _assistants;
    private bool _isBuyed = false;

    private void Awake()
    {
        _assistants = new Assistant[_poolAssistants.childCount];
        for (int i = 0; i < _assistants.Length; i++)
        {
            _assistants[i] = _poolAssistants.GetChild(i).GetComponent<Assistant>();
        }

        _text.text = _price.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SoftCurrencyHolder player))
        {
            for (int i = 0; i < _assistants.Length; i++)
            {
                if (_assistants[i].gameObject.activeInHierarchy == false)
                {
                    if (player.Value >= _price)
                    {
                        player.Spend(_price);
                        _assistants[i].gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
