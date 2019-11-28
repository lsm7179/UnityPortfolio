using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniCtrl : MonoBehaviour {

    public static bool IsWalk = false;
    [SerializeField]
    private Animator Ani;

    void Start () {
        Ani = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
        Ani.SetBool("IsWalk", IsWalk);
    }
}
