using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerDeadZone : MonoBehaviour
{
    public float Speed = 3f;
    public float AccelerationValue = 2f;
    public float DecelerationValue = 1f;
    private float _speed;

    private void Awake()
    {
        _speed = Speed;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TriggerZoneAccelerate"))
        {
            Speed += AccelerationValue;
        }
        else if (other.gameObject.CompareTag("TriggerZoneStable"))
        {
            Speed = _speed;
        }
        else if (other.gameObject.CompareTag("TriggerZoneDecelerate"))
        {
            Speed -= DecelerationValue;
        }
    }
}
