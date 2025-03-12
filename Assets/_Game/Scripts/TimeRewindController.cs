using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TimeRewindController : MonoBehaviour
{
    public static TimeRewindController Instance;

    public static event Action<Action<TimeBody>> StartRewindEvent;
    public static event Action<Action<TimeBody>> StopRewindEvent;

    public float recordTime = 5f;
    public GameObject clonePrefab;
    public Transform spawnPos;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartRewindEvent?.Invoke(SpawnClone);
        }

        //if (Input.GetKeyUp(KeyCode.K))
        //{
        //    StopRewindEvent?.Invoke(SpawnClone);
        //}
    }

    private void SpawnClone(TimeBody _player)
    {
        GameObject _clone = Instantiate(clonePrefab);
        _clone.transform.position = spawnPos.position;
        StartCoroutine(ReplayClone(_clone, _player));
    }

    IEnumerator ReplayClone(GameObject clone, TimeBody _timeBody)
    {
        for(int i = _timeBody.pointsInTimeFromBegin.Count - 1; i >= 0 ; i--)
        {
            clone.transform.position = _timeBody.pointsInTimeFromBegin[i].position;
            clone.transform.rotation = _timeBody.pointsInTimeFromBegin[i].rotation;
            yield return new WaitForSeconds(0.02f);
        }

        _timeBody.pointsInTimeFromBegin.Clear();
        //Destroy(clone);
    }
}
