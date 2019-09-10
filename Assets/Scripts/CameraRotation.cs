using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
	Camera _camera;

	public float _height = 7.2f;
	public float _distance = 13f;
	public Vector3 _lookAt = new Vector3(0f, 4f, 0f);
	public float _speed = 0.1f;

	float _angle = 0f;

  // Start is called before the first frame update
  void Start () {
		_camera = GetComponent<Camera>();
  }

  // Update is called once per frame
  void Update () {
		_angle+= Time.deltaTime * _speed;

		_camera.transform.position = new Vector3(
			Mathf.Cos(_angle) * _distance,
			_height,
			Mathf.Sin(_angle) * _distance
		);

		_camera.transform.LookAt(_lookAt);
  }
}
