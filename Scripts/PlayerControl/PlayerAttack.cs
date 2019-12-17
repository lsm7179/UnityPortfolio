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

            StartCoroutine(WaitForAttack(0.16f));
            swordCollider.enabled = true;
            timer += Time.time;
            PlayerAniCtrl.IsWalk = false;
            PlayerAniCtrl.IsAttack = true;
            StartCoroutine(WaitForIt(0.4f));

        }
    }
    IEnumerator WaitForAttack(float time)
    {
        yield return new WaitForSeconds(time);
        check = false;
    }
        IEnumerator WaitForIt(float time)
    {
        yield return new WaitForSeconds(time);
        swordCollider.enabled = false;
        check = true;
        PlayerAniCtrl.IsAttack = false;
    }
    void AttackEffect()
    {
        //Instantiate(bladeEffactPrefab, EffectPosTr.position, EffectPosTr.rotation);
    }
}
