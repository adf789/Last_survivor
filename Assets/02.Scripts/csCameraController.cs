﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCameraController : MonoBehaviour {
	private bool isThird;
	private Transform thirdPos;
	private csCharacterController charController;
	private Transform charTransform;
	private float mouseSensitive;

	// Use this for initialization
	void Start () {
		isThird = true;
		thirdPos = GameObject.Find ("ThirdCameraPos").transform;
		charController = FindObjectOfType<csCharacterController> ();
		charTransform = FindObjectOfType<csCharacterController> ().transform;
		mouseSensitive = charController.GetSensetive;
	}
	
	// Update is called once per frame
	void Update () {
		CameraRotation ();
		// 키보드 상 C 버튼을 눌렀을 때 카메라의 위치를 3인칭 혹은 1인칭으로 변경한다.
		if (Input.GetButtonDown("C")) {
			ConvertView ();
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
			transform.position = thirdPos.position;
			transform.rotation = thirdPos.rotation;
			isThird = true;
		}
	}

	private void CameraRotation(){
		float mouseY = Input.GetAxis ("Mouse Y");

		// 3인칭인 경우, 1인칭인 경우 카메라 회전을 다르게 한다.
		if (isThird) {
			// 3인칭의 움직임 범위를 제한한다.
			// 카메라의 최고 위치
			if (transform.localPosition.y <= 0.25f && mouseY > 0f)
				mouseY = 0f;
			// 카메라의 최하 위치
			else if (transform.localRotation.x >= 0.4f && mouseY < 0f)
				mouseY = 0f;

			// 마우스의 Y축 회전에 따른 카메라 회전.
			transform.RotateAround (charTransform.position, charTransform.right, mouseSensitive * -mouseY * Time.deltaTime);
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
}
