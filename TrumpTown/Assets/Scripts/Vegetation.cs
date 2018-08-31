using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetation : MonoBehaviour {
    public bool lower = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (lower) {
            Vector3 targetPos = new Vector3(transform.position.x, -10.0f, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 1 * Time.deltaTime);
            
        }
	}

    public void setLower()
    {
        lower = true;
        StartCoroutine(RemoveVegetationAfterTime(5));
    }

    IEnumerator RemoveVegetationAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject.Destroy(gameObject);
    }
}
