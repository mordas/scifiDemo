using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
	[SerializeField]
	private float _sensitve = 1.0f;
	void Start () {
		
	}
	
	void Update ()
	{
		float _mouseX = Input.GetAxis("Mouse X");
		Vector3 newRotation = transform.localEulerAngles;
		newRotation.y += _mouseX * _sensitve;
		transform.localEulerAngles = newRotation;
	}
}
