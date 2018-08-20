using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {
    public bool construction = false;
    public float raiseSpeed = 10;
		
	// Update is called once per frame
	void Update () {
		if (!construction) {
            StartCoroutine(Raise());
        }
	}




    IEnumerator Raise()
    {
        Vector3 targetPos = new Vector3(transform.position.x, 0.25f, transform.position.z);
        yield return new WaitForSeconds(1.0f);

        // y = -2.82 go to y = -0.46
        transform.position = Vector3.MoveTowards(transform.position, targetPos, raiseSpeed * Time.deltaTime);

        construction = true;
    }
}
