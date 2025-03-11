using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBodyClone : MonoBehaviour
{
    public List<PointInTime> pointsInTime = new();
    public List<PointInTime> tmpPointsInTime = new();
    public bool isRewind { set; get; }

    private void FixedUpdate()
    {
        if (isRewind)
        {
            Rewind();
        }
        else
        {
            Forward();
        }
    }

    public void Forward()
    {
        if (tmpPointsInTime.Count > 0)
        {
            PointInTime pointInTime = tmpPointsInTime[tmpPointsInTime.Count - 1];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            tmpPointsInTime.RemoveAt(tmpPointsInTime.Count - 1);
        }
    }
    public void Rewind()
    {
        if (tmpPointsInTime.Count > 0)
        {
            PointInTime pointInTime = tmpPointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            tmpPointsInTime.RemoveAt(0);
        }
    }

    public void InitPointsInTime(List<PointInTime> _pointsInTime)
    {
        for(int i = 0; i < _pointsInTime.Count; i++)
        {
            pointsInTime.Add(_pointsInTime[i]);
        }
    }

    public void ResetPointInTime()
    {
        tmpPointsInTime.Clear();

        for (int i = 0; i < pointsInTime.Count; i++)
        {
            tmpPointsInTime.Add(pointsInTime[i]);
        }
    }
}
