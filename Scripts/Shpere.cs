using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shpere : MonoBehaviour {
    private Transform _player = null;
    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        StartCoroutine(Trace());
    }

    void FixedUpdate()
    {

    }

    IEnumerator Trace()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
        float dist = Vector3.Distance(_player.position, this.transform.position);

        if(dist < 3.0f)
        {
                transform.position = Vector3.Lerp(transform.position, _player.position, Time.deltaTime * 3.0f);
        }
        else if (dist < 0.01f)
        {
                Destroy(this.gameObject);
        }
        yield return new WaitForSeconds(0.2f);
        }
        
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

}
