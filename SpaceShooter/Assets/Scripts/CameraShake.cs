using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private bool isShaking = false;
    [SerializeField] [Tooltip("Put your Camera here")]
    private Transform objectToShake;
    IEnumerator Shake(float Duration, float Magnitude)
    {
        Vector3 originalPos = objectToShake.localPosition;
        float elapsed = 0.0f;
        while (elapsed < Duration)
        {
            float x = Random.Range(-1, 1) * Magnitude;
            float y = Random.Range(-1, 1) * Magnitude;
            objectToShake.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;

        }
        objectToShake.localPosition = originalPos;
        isShaking = false;
    }
    public void ShakeCamera(float Magnitude, float Duration)
    {
        if (!isShaking)
        {
            StartCoroutine(Shake(Duration, Magnitude));
        }
    }
}
