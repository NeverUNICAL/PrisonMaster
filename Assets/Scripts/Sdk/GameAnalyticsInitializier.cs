using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAnalyticsInitializier : MonoBehaviour
{
    private void Awake()
    {
        GameAnalytics.Initialize();
    }

    private void Start()
    {
        SceneManager.LoadScene(1);
    }
}
