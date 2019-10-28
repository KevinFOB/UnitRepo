using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotationController : MonoBehaviour
{

    /// <summary>
    /// Rotation file under assets
    /// </summary>
    [SerializeField]
    private string _fileName = string.Empty;

    /// <summary>
    /// Rotation transition time in seconds
    /// </summary>
    [SerializeField]
    private float _lerpDuration = 1.0f;

    private RotationData rotationData = new RotationData();

    private bool _rotationsReady = false;
    

    void Start()
    {
        rotationData.RotationsReady = RotationsReady;

        // Load rotations from file
        rotationData.LoadRotations(_fileName);

        // Launch coroutine to wait to rotate object (when data is ready)
        StartCoroutine(RotateObject());
    }

    private IEnumerator RotateObject()
    {

        yield return new WaitUntil(() => _rotationsReady);

        List<Angle> sortedAngles = rotationData.Rotations.OrderBy(a => a.X).ToList<Angle>();

        // Start rotations
        foreach (Angle a in sortedAngles)
        {
            Quaternion fromQuat = transform.rotation;
            Quaternion toQuat = Quaternion.Euler(a.X, a.Y, a.Z);

            float t = 0.0f;

            while (t < _lerpDuration)
            {
                t += Time.deltaTime;
                Quaternion q = Quaternion.Slerp(fromQuat, toQuat, Mathf.Clamp(t/_lerpDuration, 0.0f, 1.0f));
                transform.rotation = q;

                yield return null;
            }

        }


    }

    /// <summary>
    /// Will be signaled when data is loaded from file
    /// </summary>
    private void RotationsReady()
    {
        _rotationsReady = true;
    }
}
