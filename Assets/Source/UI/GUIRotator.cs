using UnityEngine;

public class GUIRotator : MonoBehaviour
{
    [SerializeField] private Transform _targetFinish;
    [Header("Items")]
    [SerializeField] private Transform _arrow;

    private void FixedUpdate()
    {
        _arrow.LookAt(_targetFinish);
    }
}
