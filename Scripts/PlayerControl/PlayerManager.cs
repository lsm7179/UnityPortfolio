using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    [SerializeField]
    private Image hpBar;
    private int hpInit = 300;
    private int hp = 300;
    private Text hpText = null;
    //[SerializeField]
    public GameObject inventory = null;
    [SerializeField]
    private Animator Ani;

    void Awake()
    {
        hpBar= GameObject.Find("HpBar").GetComponent<Image>();
        hpText = hpBar.GetComponentInChildren<Text>();
        Ani = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("MonsterSword"))
        {
            hp -= 15;
            hpBar.fillAmount = (float)hp / (float)hpInit;
            hpText.text = hp + " / " + hpInit;
        }

        if (other.name.Equals("Body"))
        {
            Debug.Log(other.GetComponentInParent<SlimeCtrl>().thisState);
            if (other.gameObject.GetComponentInParent<SlimeCtrl>().thisState == SlimeCtrl.SlimeState.attack)
            {
            hp -= 5;
            hpBar.fillAmount = (float)hp / (float)hpInit;
            hpText.text = hp + " / " + hpInit;
                Ani.SetTrigger("IsHit");
            }
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    //if(MonsterSword)


    //    if (other.gameObject.name.Equals("MonsterSword"))
    //    {
    //        hp -= 15;
    //        hpBar.fillAmount= (float)hp / (float)hpInit;
    //        hpText.text= hp+" / "+ hpInit;
    //    }
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryOnOff();
        }
    }

    void InventoryOnOff()
    {
        if (inventory.activeInHierarchy)
        {
            inventory.SetActive(false);
        }
        else
        {
            inventory.SetActive(true);
        }
    }
}
