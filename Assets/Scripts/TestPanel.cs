using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : MonoBehaviour
{
    private void OnMouseDown()
    {
        transform.gameObject.SetActive(false);
    }
}
