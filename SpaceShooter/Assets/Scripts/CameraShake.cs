using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    bool isShaking = false;
   [SerializeField] Transform cameraTransform;
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = cameraTransform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;
            cameraTransform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;

        }
        cameraTransform.localPosition = originalPos;
        isShaking = false;
    }
    public void ShakeCamera(float Magnitude, float duration)
    {
        if (!isShaking)
        {
            StartCoroutine(Shake(duration, Magnitude));
        }
    }
}
