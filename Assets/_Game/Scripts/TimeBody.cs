using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding = false;
    //public float recordTime = 5f;
    public List<PointInTime> pointsInTime;
    public List<PointInTime> pointsInTimeFromBegin;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        TimeRewindController.StartRewindEvent += StartRewind;
        TimeRewindController.StopRewindEvent += StopRewind;
    }
    private void OnDisable()
    {
        TimeRewindController.StartRewindEvent -= StartRewind;
        TimeRewindController.StopRewindEvent -= StopRewind;
    }
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        pointsInTimeFromBegin = new List<PointInTime>();

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(TimeRewindController.Instance.recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }
}
