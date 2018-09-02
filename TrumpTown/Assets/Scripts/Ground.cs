using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {
    public GameObject housePrefab;
    public GameObject smokePrefab;

    public bool canBuild = true;
    public float rotateSpeed = 1f;
    public bool isRotatingTile = false;
    public Transform target;
    public float speed;
    public float housePosition = 0.25f;

    private bool isOccupied = false;
    private bool rotate = false;
    private Quaternion targetRotation;
    private Vector3 tilePos;

    private void Start()
    {
        targetRotation = transform.rotation;
        tilePos = this.transform.position;
    }

    void OnMouseDown()
    {
        if (isRotatingTile) {
            RotateTile();
        } else {  
            RisingTile();
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

    void RotateTile()
    {
        if (GameManager.Instance.GetBalance() >= 100 && !isOccupied && canBuild) {
            // get the tiles position
            tilePos = this.transform.position;
            Vector3 housePos;
            
            // set the position of the building
            housePos = new Vector3(tilePos.x, housePosition, tilePos.z);

            // put the building in the scene
            GameObject house = (GameObject)Instantiate(housePrefab, housePos, Quaternion.AngleAxis(180.0f, transform.forward) * transform.rotation);

            // make the tile the parent object for the house
            house.transform.parent = this.gameObject.transform;

            // wait a second then rotate tile with the building
            StartCoroutine(Rotate());

            // deduct funds for building
            // TODO: ground script is doing too much. we need to shift this logic in a better spot
            // TODO: get rid of these hard coded values
            GameManager.Instance.bank -= 100;
            GameManager.Instance.peopleMax += 3;

            // set the ground tile as occupied
            isOccupied = true;

            // determine if there is a object attached to the tile and destroy for now
            StartCoroutine(RemoveVegetationAfterTime(2));

        } else if (isOccupied) {
            // wait a second then rotate tile with the building
            StartCoroutine(Rotate());
            isOccupied = false;
        }
    }

    void RisingTile ()
    {
        if (GameManager.Instance.GetBalance() >= 100 && !isOccupied && canBuild) {
            // start the building smoke
            CreateBuildingSmoke();

            //lower the tree
            foreach (Transform child in transform) {
                if (child.tag == "Vegetation") {
                    child.GetComponent<Vegetation>().setLower();
                }
            }

            StartCoroutine(RaiseHouseAfterTime(3));

            // deduct funds for building
            GameManager.Instance.bank -= 100;

            // set the ground tile as occupied
            isOccupied = true;
        } else if (isOccupied) {
            // find the build attach to the tile
            GameObject house = getChildGameObject(this.gameObject, "TealWhiteHouse(Clone)");

            // lower it
            CreateBuildingSmoke();
            house.GetComponent<House>().setBuild();
            Destroy(house, 3.0f);
            isOccupied = false;
        }
    }

    IEnumerator RemoveVegetationAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        foreach (Transform child in transform) {
            if (child.tag == "Vegetation") {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    IEnumerator RaiseHouseAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // get the tiles position
        tilePos = this.transform.position;
        Vector3 housePos;

        // set the building positon under the tile
        housePos = new Vector3(tilePos.x, -2.5f, tilePos.z);

        // add the building to the scene
        GameObject house = (GameObject)Instantiate(housePrefab, housePos, Quaternion.identity);
        // assign the tile as the parent of this building
        house.transform.parent = this.gameObject.transform;

        // construct building
        house.GetComponent<House>().setBuild();
    }

    




}
