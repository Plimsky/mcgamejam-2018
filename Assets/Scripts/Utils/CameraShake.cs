using System.Collections;
using UnityEngine;

namespace Utils
{
    public class CameraShake : MonoBehaviour
    {
        public float Frequency = 10f;

        private static float _magnitude;
        private static float _duration;
        private static int _count;

        private Vector3 _originPosition;
        private Vector3 _originRotation;

        private void Awake()
        {
            _originPosition = transform.localPosition;
            _originRotation = transform.eulerAngles;
        }

        public IEnumerator Shake(float duration, float magnitude)
        {
            float elapsed = 0.0f;

            ++_count;
            _magnitude += magnitude;
            _duration = duration;

            while (elapsed < _duration)
            {
                Vector2 perlinVector2 = PerlinShake(_magnitude);
                Vector2 perlinVector2Rotation = PerlinShake(_magnitude * 10);

                transform.localPosition = new Vector3(perlinVector2.x, perlinVector2.y, _originPosition.z);
                transform.eulerAngles = new Vector3(_originRotation.x, _originRotation.y, perlinVector2Rotation.x);
                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = _originPosition;
            transform.eulerAngles = _originRotation;
            _magnitude = 0;
            _duration = 0;
        }

        public Vector2 PerlinShake(float magnitude)
        {
            Vector2 result;
            float seed = Time.time * Frequency;
            result.x = Mathf.Clamp01(Mathf.PerlinNoise(seed, 0f)) - 0.5f;
            result.y = Mathf.Clamp01(Mathf.PerlinNoise(0f, seed)) - 0.5f;
            result = result * magnitude;
            return result;
        }
    }
}