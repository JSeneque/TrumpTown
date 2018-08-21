using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {
    public GameObject housePrefab;
    public GameObject smokePrefab;

    public bool canBuild = true;
    public float rotateSpeed = 1f;
    public bool testRotatingTile = false;
    public Transform target;
    public float speed;
    public float housePosition = 0.24576f;

    private bool occupied = false;
    private bool rotate = false;
    private Quaternion targetRotation;
    private Vector3 tilePos;

    private void Start()
    {
        targetRotation = transform.rotation;
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.GetBalance() >= 100 && !occupied && canBuild) {
            // get the tiles position
            tilePos = this.transform.position;
            Vector3 housePos;

            if (testRotatingTile) {
                // make the tile the parent object for the house
                housePos = new Vector3(tilePos.x, housePosition, tilePos.z);
                GameObject house = (GameObject)Instantiate(housePrefab, tilePos, Quaternion.AngleAxis(180.0f, transform.forward) * transform.rotation);
                house.transform.parent = this.gameObject.transform;

                StartCoroutine(Rotate());
            } else {
                housePos = new Vector3(tilePos.x, -2.5f, tilePos.z);
                GameObject house = (GameObject)Instantiate(housePrefab, housePos, Quaternion.identity);

                CreateBuildingSmoke();

                // construct building
                house.transform.parent = this.gameObject.transform;
                house.GetComponent<House>().setBuild();
            }

            // deduct funds for building
            GameManager.Instance.bank -= 100;

            // set the ground tile as occupied
            occupied = true;
        } else if (occupied && !testRotatingTile) {
            // find the build attach to the tile
            GameObject house = getChildGameObject(this.gameObject, "TealWhiteHouse(Clone)");

            // lower it
            CreateBuildingSmoke();
            house.GetComponent<House>().setBuild();
            Destroy(house, 3.0f);
            occupied = false;
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

    public GameObject getChildGameObject (GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

    private void CreateBuildingSmoke()
    {
        // create building smoke
        GameObject smokePuff = Instantiate(smokePrefab, tilePos, Quaternion.identity) as GameObject;
        ParticleSystem parts = smokePuff.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + 2.0f;
        Destroy(smokePuff, totalDuration);
    }

}
