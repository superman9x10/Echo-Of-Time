using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindController : MonoBehaviour
{
    public static TimeRewindController Instance;

    public static event Action StartRewindEvent;
    public static event Action StopRewindEvent;

    public float recordTime = 5f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(RewindLoop_ie());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartRewindEvent?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            StopRewindEvent?.Invoke();
        }
    }

    private IEnumerator RewindLoop_ie()
    {
        while(true)
        {
            yield return new WaitForSeconds(recordTime);
            StartRewindEvent?.Invoke();
        }
    } 
}
