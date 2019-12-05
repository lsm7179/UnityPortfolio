using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniCtrl : MonoBehaviour {

    public static bool IsWalk = false;
    public static bool IsAttack = false;
    [SerializeField]
    private Animator Ani;

    void Start () {
        Ani = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
        Ani.SetBool("IsWalk", IsWalk);
        Ani.SetBool("IsAttack", IsAttack);
    }
}
