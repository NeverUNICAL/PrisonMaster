using UnityEngine;

public class GUIRotator : MonoBehaviour
{
    [SerializeField] private Transform _targetFinish;
    [Header("Items")]
    [SerializeField] private Transform _arrow;

    private void FixedUpdate()
    {
        if (_targetFinish != null)
            _arrow.LookAt(_targetFinish);
    }

    public void ChangeTarget(Transform target)
    {
        _targetFinish = target;
    }
}
