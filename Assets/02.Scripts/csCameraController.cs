﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라를 제어하기 위한 클래스이다.
public class csCameraController : MonoBehaviour {
	private bool isThird;
	private Transform thirdPos;
	private csCharacterController charController;
	private Transform charTransform;
	private Transform transform;
	private float mouseSensitive;
	private Vector3 wallPos;

	public static bool isStop;


	// Use this for initialization
	void Start () {
		isThird = true;
		isStop = false;
		thirdPos = GameObject.Find ("ThirdCameraPos").transform;
		charController = FindObjectOfType<csCharacterController> ();
		charTransform = FindObjectOfType<csCharacterController> ().transform;
		mouseSensitive = charController.GetSensetive;
		wallPos = new Vector3 ();
		transform = gameObject.GetComponent<Transform> ();
		transform.localPosition = thirdPos.localPosition;
		transform.LookAt (charTransform.position + charTransform.up);
		thirdPos.LookAt (charTransform.position + charTransform.up);
	}
	
	// Update is called once per frame
	void Update () {
		if (isStop)
			return;
		CameraRotation ();
		CheckWall ();

		// 키보드 상 C 버튼을 눌렀을 때 카메라의 위치를 3인칭 혹은 1인칭으로 변경한다.
		if (Input.GetButtonDown("C")) {
			ConvertView ();
			charController.setFocus (isThird);
		}
	}

	private void ConvertView(){
		
		// 카메라의 3인칭과 1인칭에 따라 위치를 변경한다.
		if (isThird) {
			// 1인칭인 경우

			charTransform.GetChild (0).gameObject.SetActive (false);
			transform.GetChild (0).gameObject.SetActive (true);
			transform.localPosition = Vector3.zero + Vector3.up * 0.7f;
			transform.localRotation = Quaternion.Euler (Vector3.zero);
			isThird = false;
		} else {
			// 3인칭인 경우

			charTransform.GetChild (0).gameObject.SetActive (true);
			transform.GetChild (0).gameObject.SetActive (false);
			transform.localPosition = thirdPos.localPosition;
			transform.localRotation = thirdPos.localRotation;
			isThird = true;
		}
	}

	private void CameraRotation(){
		float mouseY = Input.GetAxis ("Mouse Y");

		// 3인칭인 경우, 1인칭인 경우 카메라 회전을 다르게 한다.
		if (isThird) {
			// 3인칭의 움직임 범위를 제한한다.
			// 카메라의 최고 위치
			if (transform.localRotation.x >= 0.4f && mouseY < 0f)
				mouseY = 0f;
			
			// 카메라의 최하 위치
			else if (transform.localPosition.y <= 0.5f && transform.localPosition.z > -1.0f && mouseY > 0f)
				mouseY = 0f;

			// 마우스의 Y축 회전에 따른 카메라 회전.
			thirdPos.RotateAround(charTransform.position, charTransform.right, mouseSensitive * -mouseY * Time.deltaTime);
			thirdPos.LookAt (charTransform.position + charTransform.up);
			transform.localPosition = thirdPos.localPosition;
			transform.localRotation = thirdPos.localRotation;
		} else {
			// 1인칭의 회전 범위를 제한한다.
			// 카메라의 최고 회전
			if (transform.localRotation.x < -0.6 && mouseY > 0f)
				mouseY = 0f;
			// 카메라의 최저 회전
			else if (transform.localRotation.x > 0.6 && mouseY < 0f)
				mouseY = 0f;
			
			// 마우스의 Y축 회전에 따른 카메라 회전.
			transform.localRotation *= Quaternion.AngleAxis (mouseSensitive * -mouseY * Time.deltaTime, Vector3.right);
		}
	}

	private void CheckWall(){
		RaycastHit hit;
		Vector3 pos = transform.position;
		if (Physics.Linecast(charTransform.position, transform.position, out hit)) {
			if (hit.collider.tag.Equals ("Player"))
				return;
			if (hit.collider.name.Equals ("Ground")) {
				wallPos.Set (pos.x, hit.point.y, pos.z);
			} else {
				wallPos.Set (hit.point.x, pos.y, hit.point.z);
			}
			transform.position = wallPos;
			transform.LookAt (charTransform.position + charTransform.up);
		}
	}
}
