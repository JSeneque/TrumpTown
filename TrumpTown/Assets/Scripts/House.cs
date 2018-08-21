using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {
    public bool built = false;
    public float raiseSpeed = 10;
		
	// Update is called once per frame
	void Update () {
        //if (!built) {
        //    StartCoroutine(Raise());
        //}
        //else {
        //    StartCoroutine(Lower());
        //}
        if (built) {
            Raise();
        }
        else {
            Lower();
        }
    }

    //IEnumerator Raise()
    //{
    //    Vector3 targetPos = new Vector3(transform.position.x, 0.25f, transform.position.z);
    //    yield return new WaitForSeconds(1.0f);

    //    transform.position = Vector3.MoveTowards(transform.position, targetPos, raiseSpeed * Time.deltaTime);

    //    built = true;
    //}

    public void setBuild()
    {
        if (built) {
            built = false;
        }else {
            built = true;
        }
    }

    public void Raise()
    {
        Vector3 targetPos = new Vector3(transform.position.x, 0.25f, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, raiseSpeed * Time.deltaTime);
    }

    public void Lower()
    {
        Vector3 targetPos = new Vector3(transform.position.x, -2.5f, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, raiseSpeed * Time.deltaTime);
    }

    //IEnumerator Lower()
    //{
    //    Vector3 targetPos = new Vector3(transform.position.x, -2.5f, transform.position.z);
    //    yield return new WaitForSeconds(1.0f);

    //    transform.position = Vector3.MoveTowards(transform.position, targetPos, raiseSpeed * Time.deltaTime);

    //    //built = false;
    //}
}
