using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    [SerializeField] private float offset;

    public IEnumerator Shake(float _duration, float _magnitude) {
        Vector2 originalPosition = new Vector2(0, 0);
        
        float elapsedTime = 0f;

        while (elapsedTime < _duration) {
            float xOffset = Random.Range(-offset, offset) * _magnitude;
            float yOffset = Random.Range(-offset, offset) * _magnitude;

            transform.localPosition = new Vector2(xOffset, yOffset);

            elapsedTime += Time.deltaTime;

            yield return null; //Wait one frame
        }
        transform.localPosition = originalPosition;
    }
}
