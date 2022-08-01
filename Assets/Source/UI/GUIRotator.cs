using UnityEngine;

public class GUIRotator : MonoBehaviour
{
    [SerializeField] private Transform _targetFinish;
    
    private void FixedUpdate()
    {
       transform.LookAt(_targetFinish);
    }
}
