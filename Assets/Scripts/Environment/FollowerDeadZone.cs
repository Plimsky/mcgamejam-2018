using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerDeadZone : MonoBehaviour
{
    public float Speed = 3f;
    public float AccelerationValue = 2f;
    public float DecelerationValue = 1f;
    private float _speed;

	//For Following Player
	public float FollowThreshold;
	public float FollowSpeed;
	public Transform Player;
	private float _ySpeed;

	//Make the big mass follow the player.
	//find difference of y values, have the mass slowly approach by Math.Min of deltaY, 1

    private void Awake()
    {
		//For Following Player
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
        _speed = Speed;
    }

    private void Update()
	{
		//For Following Player
		_ySpeed = 0.0f;
		if (Mathf.Abs (Player.position.y - transform.position.y) > FollowThreshold)
			_ySpeed = Mathf.Min (Player.position.y - transform.position.y, 1.0f);
		Vector3 movementVector = Vector3.right + new Vector3 (0.0f, _ySpeed, 0.0f) * FollowSpeed / Speed;
		transform.Translate(movementVector * Time.deltaTime * Speed);

		//transform.Translate(Vector3.right * Time.deltaTime * Speed);
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
