using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRotation : MonoBehaviour
{

    public Text _txtAttitudeX;
    public Text _txtAttitudeY;
    public Text _txtAttitudeZ;

    /// <summary>
    /// Transform of sphere
    /// </summary>
    private Transform _transform;

	// Use this for initialization
	void Start ()
    {
        _transform = gameObject.transform;

        StartCoroutine(UpdateAttitude());
	}
	
    private IEnumerator UpdateAttitude()
    {
        while (true)
        {
            _txtAttitudeX.text = _transform.rotation.eulerAngles.x.ToString("0.0");
            _txtAttitudeY.text = _transform.rotation.eulerAngles.y.ToString("0.0");
            _txtAttitudeZ.text = _transform.rotation.eulerAngles.z.ToString("0.0");

            yield return new WaitForSeconds(0.05f);
        }
    }
}
