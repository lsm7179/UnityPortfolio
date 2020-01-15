using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCtrl : MonoBehaviour {

    private float DieTime = 1.5f;
    void Start () {
        Destroy(gameObject, DieTime);
    }
	
}
