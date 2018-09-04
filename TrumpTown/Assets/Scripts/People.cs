using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour {
    // The target marker.
    public Transform target;

    // Speed in units per sec.
    public float speed =5 ;

	// Update is called once per frame
	void Update () {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<House>().IncreaseDwellers();
        Destroy(gameObject);
    }

    public void SetTargetPosition(Transform pos)
    {
        target = pos;
    }
}
