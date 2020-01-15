using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	//카메라 기본 속성
	[Header("카메라 기본 속성")]
	[SerializeField]
	private Transform myTransform = null;
	public GameObject Target = null;
	private Transform targetTransform = null;

	public enum CameraViewPointState {FIRST,SENCOND,THIRD}
	public CameraViewPointState CameraState = CameraViewPointState.THIRD;



	[Header("3인칭 카메라")]
	public float distance = 5.0f;//타겟으로부터 떨어진 거리
	public float height = 1.5f; //타겟의 위치보다 더 추가적인 높이
	public float heightDamping = 2.0f; //speed 입니다.
	public float rotationDamping = 3.0f;//speed 입니다.


	void Start () {
		myTransform = GetComponent<Transform>();
		if(Target != null)
		{
			targetTransform = Target.transform;
		}
	}
	/// <summary>
	/// 3인칭 카메라
	/// </summary>
	void ThirdView()
	{
		float wantedRotationAngle = targetTransform.eulerAngles.y;//현재 타겟의 y축 각도값
		float wantedHeight = targetTransform.position.y + height;//현재 타겟의 높이 + 우리가 추가로 올리고 싶은 높이
		float currentRotationAngle = myTransform.eulerAngles.y; //현재 카메라의 y축 각도값
		float currentHeight = myTransform.position.y;//현재 카메라의 높이값

		//현재 각도에서 원하는 각도로 댐핑값을 얻게 됩니다.
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		//현재 높이에서 원하는 높이로 댐핑값을 얻게 됩니다.
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);
		myTransform.position = targetTransform.position;
		myTransform.position -= currentRotation * Vector3.forward * distance;
		myTransform.position = new Vector3(myTransform.position.x, currentHeight, myTransform.position.z);

		myTransform.LookAt(targetTransform);
		
	}
	void LateUpdate () {
		if(Target == null)
		{
			return;
		}
		if(targetTransform == null)
		{
			targetTransform = Target.transform;
		}
		switch (CameraState)
		{
			case CameraViewPointState.THIRD:
				ThirdView();
				break;
		}
	}
}
