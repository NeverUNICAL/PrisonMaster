using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerConteiner : MonoBehaviour
{
    [SerializeField] private PrisonerSpawner _spawnPoint;
    [SerializeField] private float _cooldown = 5f;

    private Prisoner[] _prisoners;
    

    private void Awake()
    {
        _prisoners = new Prisoner[transform.childCount];
        for (int i = 0; i < _prisoners.Length; i++)
        {
            _prisoners[i] = transform.GetChild(i).GetComponent<Prisoner>();
        }
    }

    private void Update()
    {
        if (_spawnPoint.IsEmpty)
        {
            _cooldown -= Time.deltaTime;
            if (_cooldown <= 0)
            {
                for (int i = 0; i < _prisoners.Length; i++)
                {
                    if (_prisoners[i].gameObject.activeInHierarchy == false)
                    {
                        _prisoners[i].transform.position = _spawnPoint.transform.position;
                        _prisoners[i].transform.rotation = _spawnPoint.transform.rotation;
                        _prisoners[i].gameObject.SetActive(true);
                        _cooldown = 5f;
                        return;
                    }
                }
            }
        }
    }
}
