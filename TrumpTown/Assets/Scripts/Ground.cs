using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {
    public GameObject housePrefab;

    public bool canBuild = true;
    public float rotateSpeed = 1f;
    public bool testRotatingTile = false;

    private bool occupied = false;
    private bool rotate = false;
    private Quaternion targetRotation;

    void OnMouseDown()
    {
        if (GameManager.Instance.GetBalance() >= 100 && !occupied && canBuild) {
            // get the tiles position
            Vector3 tilePos = this.transform.position;
            Vector3 housePos;

            if (!testRotatingTile) {
                housePos = new Vector3(tilePos.x, -2.5f, tilePos.z);
            } else {
                housePos = new Vector3(0, 0, 0);
            }
            

            // instantiate a house at the tiles position
            //  GameObject enemy = (GameObject)Instantiate(activeWave.listOfEnemies[0], WayPointManager.Instance.GetSpawnPosition(activeWave.pathIndex), Quaternion.identity);
            GameObject house = (GameObject)Instantiate(housePrefab, housePos, Quaternion.identity);

            if (testRotatingTile) {
                // make the tile the parent object for the house
                //house.transform.parent = this.gameObject.transform;
                StartCoroutine(Rotate());
                
            }

            GameManager.Instance.bank -= 100;
            occupied = true;
        }
    }

    void Update()
    {
        if (rotate) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0 , 180), rotateSpeed * Time.deltaTime);
        }
    }

    IEnumerator Rotate()
    {
        yield return new WaitForSeconds(1.0f);
        rotate = true;
    }

}
