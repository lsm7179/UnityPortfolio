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
    void Awake()
    {
        hpBar= GameObject.Find("HpBar").GetComponent<Image>();
        hpText = hpBar.GetComponentInChildren<Text>();

    }

    
	void OnTriggerEnter(Collider other)
    {
        //if(MonsterSword)
        if (other.gameObject.name.Equals("MonsterSword"))
        {
            hp -= 15;
            hpBar.fillAmount= (float)hp / (float)hpInit;
            hpText.text= hp+" / "+ hpInit;
        }
    }
}
