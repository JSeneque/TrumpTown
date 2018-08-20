using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {
    public GameObject housePrefab;

    public bool canBuild = true;
    public float rotateSpeed = 1f;
    public bool testRotatingTile = false;
    public Transform target;
    public float speed;
    public float housePosition = 0.24576f;

    private bool occupied = false;
    private bool rotate = false;
    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = transform.rotation;
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.GetBalance() >= 100 && !occupied && canBuild) {
            // get the tiles position
            Vector3 tilePos = this.transform.position;
            Vector3 housePos;

            

           

            // instantiate a house at the tiles position
            //  GameObject enemy = (GameObject)Instantiate(activeWave.listOfEnemies[0], WayPointManager.Instance.GetSpawnPosition(activeWave.pathIndex), Quaternion.identity);
            

            if (testRotatingTile) {
                // make the tile the parent object for the house
                housePos = new Vector3(tilePos.x, housePosition, tilePos.z);
                GameObject house = (GameObject)Instantiate(housePrefab, tilePos, Quaternion.AngleAxis(180.0f, transform.forward) * transform.rotation);
                house.transform.parent = this.gameObject.transform;
                
                StartCoroutine(Rotate());
            } else {
                housePos = new Vector3(tilePos.x, -2.5f, tilePos.z);
                GameObject house = (GameObject)Instantiate(housePrefab, housePos, Quaternion.identity);
            }

            GameManager.Instance.bank -= 100;
            occupied = true;
        }
    }

    void Update()
    {
        if (rotate) {
            targetRotation = Quaternion.AngleAxis(180.0f, transform.forward) * transform.rotation;
            
            rotate = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);


    }

    IEnumerator Rotate()
    {
        yield return new WaitForSeconds(1.0f);
        rotate = true;
    }

}
