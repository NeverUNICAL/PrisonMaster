using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyShooter : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private float _angleInDegrees;
    [SerializeField] private float _delayDestroy = 1f;

    private float g = Physics.gravity.y;
    
    private void Start()
    {
        _spawnTransform.localEulerAngles = new Vector3(-_angleInDegrees, 0f, 0f);
    }

    public void Shoot(Transform targetTransform)
    {
        Vector3 fromTo = targetTransform.position - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float AngleInRadians = _angleInDegrees * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        GameObject newPrefab = Instantiate(_prefab, _spawnTransform.position, Quaternion.identity);
        newPrefab.GetComponent<Rigidbody>().velocity = _spawnTransform.forward * v;

        StartCoroutine(DestroyObject(newPrefab, _delayDestroy));
    }

    private IEnumerator DestroyObject(GameObject prefab, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(prefab);
    }
}
