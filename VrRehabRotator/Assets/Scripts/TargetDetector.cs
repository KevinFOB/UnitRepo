using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TargetRange
{
    [Range(0.0f, 359.9f)]
    public float TargetRange1;

    [Range(0.0f, 359.9f)]
    public float TargetRange2;

    private float _min = -1;
    private float _max = -1;

    public bool WithinRange(float value)
    {
        if (_min < 0)
        {
            // First time init
            InitMinMax();
        }

        return (value >= _min && value <= _max);
    }

    public override string ToString()
    {
        if (_min < 0)
        {
            // First time init
            InitMinMax();
        }
        return string.Format("{0:0.0} - {1:0.0}", _min, _max);
    }


    private void InitMinMax()
    {
        _min = TargetRange1;
        _max = TargetRange2;

        if (_max < _min)
        {
            _min = TargetRange2;
            _max = TargetRange1;
        }
    }
}

public class TargetDetector : MonoBehaviour
{

    /// <summary>
    /// Used to ignore minimal changes since last detection
    /// </summary>
    [Range(0.0f, 1.0f)]
    public float ChangeThreshold;

    private Vector3 _lastAngles;


    public TargetRange XRange;
    public TargetRange YRange;
    public TargetRange ZRange;

    public Action<float> XTargetHit;

    public Action<float> YTargetHit;

    public Action<float> ZTargetHit;

    private void Start()
    {
        _lastAngles = transform.rotation.eulerAngles;
    }


    private void LateUpdate()
    {
        Vector3 currentAngles = transform.rotation.eulerAngles;

        CheckTargetHit(XTargetHit, _lastAngles.x, currentAngles.x, XRange);
        CheckTargetHit(YTargetHit, _lastAngles.y, currentAngles.y, YRange);
        CheckTargetHit(ZTargetHit, _lastAngles.z, currentAngles.z, ZRange);

        _lastAngles = currentAngles;

    }

    /// <summary>
    /// Checks if current angle is within supplied range
    /// </summary>
    /// <param name="TargetHit"></param>
    /// <param name="priorAnge"></param>
    /// <param name="currentAngle"></param>
    /// <param name="range"></param>
    private void CheckTargetHit(Action<float> TargetHit, float priorAnge, float currentAngle, TargetRange range)
    {
        float delta = Mathf.Abs(priorAnge - currentAngle);

        if (delta >= ChangeThreshold)
        {
            if (range.WithinRange(currentAngle) && TargetHit != null)
                TargetHit(currentAngle);
        }

    }
}
