using Agava.IdleGame.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    private CameraCustom _cameraCustom;
    private CameraFollower _cameraFollower;

    private void Awake()
    {
        _cameraCustom = GetComponent<CameraCustom>();
        _cameraFollower = GetComponent<CameraFollower>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_cameraCustom.enabled == false)
                _cameraCustom.enabled = true;
            else
                _cameraCustom.enabled = false;

            if (_cameraFollower.enabled == false)
                _cameraFollower.enabled = true;
            else
                _cameraFollower.enabled = false;
        }
    }
}
