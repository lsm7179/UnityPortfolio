using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private Animator ani;
    //플레이어가 공격하는 모션 제어
    private GameObject bladeEffactPrefab;
    public Transform effectPosTr;
    public Collider swordCollider;
    public bool check = true;
    float timer;
    void Start () {
        swordCollider.enabled = false;
        ani = GetComponent<Animator>();
        bladeEffactPrefab = Resources.Load<GameObject>("Effect/BladeStorm");
    }
	
	void FixedUpdate () {
        Attack();

    }

    void Attack()
    {

        if (Input.GetKeyDown(KeyCode.A)&& check)
        {

            Debug.Log("공격");
            check = false;
            swordCollider.enabled = true;
            timer += Time.time;
            PlayerAniCtrl.IsWalk = false;
            ani.SetTrigger("IsAttack");
            AttackEffect();
            StartCoroutine(WaitForIt());

        }
        if(Time.time >= timer + 1.5f) {
            
        }
    }
    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(0.8f);
        swordCollider.enabled = false;
        check = true;
        Debug.Log("시간지연");
    }
    void AttackEffect()
    {
        //Instantiate(bladeEffactPrefab, EffectPosTr.position, EffectPosTr.rotation);
    }
}
