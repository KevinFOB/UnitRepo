using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHits : MonoBehaviour
{

    public Text _txtHitX;
    public Text _txtHitY;
    public Text _txtHitZ;

    public Text _txtXRange;
    public Text _txtYRange;
    public Text _txtZRange;

    public float FreezeTimeSeconds = 1.0f;
    private float _freezeTime;

    private TargetDetector _targetDetector;


    private int _xCount = 0;
    private int _yCount = 0;
    private int _zCount = 0;


    public bool FreezeOnHit { get; set; }


    void Start()
    {
        _targetDetector = gameObject.GetComponent<TargetDetector>();
        _targetDetector.XTargetHit = XTargetHitDetected;
        _targetDetector.YTargetHit = YTargetHitDetected;
        _targetDetector.ZTargetHit = ZTargetHitDetected;

        _txtXRange.text = "Range: " + _targetDetector.XRange;
        _txtYRange.text = "Range: " + _targetDetector.YRange;
        _txtZRange.text = "Range: " + _targetDetector.ZRange;
   

    }


    private void XTargetHitDetected(float angle)
    {
        _xCount++;
        _txtHitX.text = string.Format("{0} :  {1:0.0}", _xCount, angle);

        if (FreezeOnHit)
            StartCoroutine(Freeze(FreezeTimeSeconds));
    }

    private void YTargetHitDetected(float angle)
    {
        _yCount++;
        _txtHitY.text = string.Format("{0} :  {1:0.0}", _yCount, angle);

        if (FreezeOnHit)
            StartCoroutine(Freeze(FreezeTimeSeconds));
    }

    private void ZTargetHitDetected(float angle)
    {
        _zCount++;
        _txtHitZ.text = string.Format("{0} :  {1:0.0}", _zCount, angle);

        if (FreezeOnHit)
            StartCoroutine(Freeze(FreezeTimeSeconds));
    }

    private IEnumerator Freeze(float seconds)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1.0f;
    }

}
