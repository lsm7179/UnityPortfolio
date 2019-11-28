using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    //플레이어 제어
    public float moveSpeed = 7.0f;
    public float rotSpeed = 40.0f;
    private Transform playTr;


    private Camera mainCamera;
    private Vector3 targetPos; // 캐릭터의 이동 타겟 위치
    

    void Start () {
        playTr = GetComponent<Transform>();
        mainCamera = GameObject.Find("Camera").GetComponent<Camera>();
        targetPos = playTr.position;

    }
		
	void FixedUpdate () {
        move();

    }

    //플레이어 이동 및 이동 애니메이션 제어
    void move()
    {
        // 마우스 입력을 받았 을 때
        if (Input.GetMouseButtonUp(0))
        {
            // 마우스로 찍은 위치의 좌표 값을 가져온다
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                targetPos = hit.point;
            }
            PlayerAniCtrl.IsWalk = true;
        }
        // 캐릭터가 움직이고 있다면
        if (Run(targetPos))
        {
            // 회전도 같이 시켜준다
            Turn(targetPos);
        }
    }



    public bool Run(Vector3 targetPos)
    {
        // 이동하고자하는 좌표 값과 현재 내 위치의 차이를 구한다.
        float dis = Vector3.Distance(transform.position, targetPos);
       
        if (dis >= 0.01f&&PlayerAniCtrl.IsWalk) // 차이가 아직 있고 상태가 걷는 상태라면
        {
            // 캐릭터를 이동시킨다.
            transform.localPosition = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            return true;
        }
        PlayerAniCtrl.IsWalk = false;
        return false;

    }

    public void Turn(Vector3 targetPos)
    {
        // 캐릭터를 이동하고자 하는 좌표값 방향으로 회전시킨다
        Vector3 dir = targetPos - transform.position;
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
        Quaternion targetRot = Quaternion.LookRotation(dirXZ);
        playTr.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 550.0f * Time.deltaTime);
    }
}

