using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {
    [SerializeField]
    private Transform _player = null;
    private bool traceTrigger = false;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        StartCoroutine(IsTrigger());
    }

    void FixedUpdate()
    {
        if (traceTrigger)
        {
        transform.position = Vector3.Lerp(transform.position, _player.position, Time.deltaTime * 7.0f);
        }
        return;
    }

    IEnumerator IsTrigger()
    {
        yield return new WaitForSeconds(1.5f);
        while (true)
        {
            float dist = Vector3.Distance(_player.position, this.transform.position);
        if (dist <= 0.4f)
        {
            Destroy(gameObject);
        }
        else if (dist < 3.5f)
        {
            traceTrigger = true;
        }
        yield return new WaitForSeconds(0.2f);
        }
    }

    void OnDestroy()
    {
        //체력 마나 회복되게끔
        if (this.gameObject.name.Equals("MpSphere"))
        {
            if (PlayerManager.Instance.mp <= 85)
            {
                PlayerManager.Instance.mp += 15;

            }else if(PlayerManager.Instance.mp > 85&&PlayerManager.Instance.mp <= 100)
            {
                PlayerManager.Instance.mp = 100;
            }
            else
            {
                return;
            }
            PlayerManager.Instance.mpBar.fillAmount = (float)PlayerManager.Instance.mp / (float)PlayerManager.Instance.mpInit;
            PlayerManager.Instance.mpText.text = PlayerManager.Instance.mp + " / " + PlayerManager.Instance.mpInit;
        }
        else if (this.gameObject.name.Equals("HpSphere"))
        {
            if (PlayerManager.Instance.hp <= 285)
            {
                PlayerManager.Instance.hp += 15;
            }
            else if (PlayerManager.Instance.hp > 285&&PlayerManager.Instance.hp <= 300)
            {
                PlayerManager.Instance.hp = 300;
            }
            else
            {
                return;
            }
            PlayerManager.Instance.hpBar.fillAmount = (float)PlayerManager.Instance.hp / (float)PlayerManager.Instance.hpInit;
            PlayerManager.Instance.hpText.text = PlayerManager.Instance.hp + " / " + PlayerManager.Instance.hpInit;
        }

        StopAllCoroutines();
    }

    

}
