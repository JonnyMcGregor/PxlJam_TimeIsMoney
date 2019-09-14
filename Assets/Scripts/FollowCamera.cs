using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
	public Transform target;
	public float distance;
	public float yaw;
	public float pitch;

	// Update is called once per frame
	void Update () {
		transform.position = target.position + new Vector3(Mathf.Sin(yaw * Mathf.Deg2Rad), Mathf.Sin(pitch * Mathf.Deg2Rad), Mathf.Cos(yaw * Mathf.Deg2Rad)) * distance;
		transform.rotation = Quaternion.LookRotation ((target.position - transform.position).normalized);
	}
}
