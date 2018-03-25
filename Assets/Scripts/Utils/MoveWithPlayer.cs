using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlayer : MonoBehaviour {
    Vector3 lastPos;
    public Transform player;

    void Start()
    {
        lastPos = player.position;
    }

	// Update is called once per frame
	void Update () {
        Vector3 offset = player.position - lastPos;
        if(Mathf.Abs(offset.y) > 0.0f)
        {
            lastPos = player.position;
            transform.Translate(0, offset.y, 0);
        }
	}
}
