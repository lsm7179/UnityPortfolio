using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private Animator ani;
    //플레이어가 공격하는 모션 제어
    private GameObject bladeEffactPrefab;
    public Transform EffectPosTr;

    void Start () {
        ani = GetComponent<Animator>();
        bladeEffactPrefab = Resources.Load<GameObject>("Effect/BladeStorm");
    }
	
	void FixedUpdate () {
        Attack();

    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

            PlayerAniCtrl.IsWalk = false;
            ani.SetTrigger("IsAttack");
            AttackEffect();

        }
    }
    void AttackEffect()
    {
        //Instantiate(bladeEffactPrefab, EffectPosTr.position, EffectPosTr.rotation);
    }
}
