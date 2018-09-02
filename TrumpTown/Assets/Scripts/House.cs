using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public bool built = false;
    public float raiseSpeed = 10;
    public bool occupied = false;               // flag if someone rented the house
    public GameObject chimneySmokePrefab;

    private Vector3 housePos;

    private void Start()
    {
        housePos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (built) {
            Raise();
        }
        else {
            Lower();
        }
    }

    // sets the flag to build or not build building
    public void setBuild()
    {
        if (built) {
            built = false;
        }
        else {
            built = true;
        }
    }

    // raise the building from the ground
    public void Raise()
    {
        Vector3 targetPos = new Vector3(transform.position.x, 0.25f, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, raiseSpeed * Time.deltaTime);
        //StartCoroutine(OccupyBuilding(5));
    }

    // lowers the building
    public void Lower()
    {
        Vector3 targetPos = new Vector3(transform.position.x, -2.5f, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, raiseSpeed * Time.deltaTime);
    }

    // someone rents the building
    IEnumerator OccupyBuilding (float time)
    {
        yield return new WaitForSeconds(time);
        Vector3 chimneySmokePos;

        occupied = true;

        // TODO: Create the chimney smoke
        // -0.675, 2.75, 0.20
        // set the position of the building
        chimneySmokePos = new Vector3((housePos.x + -0.675f), (housePos.y + 2.75f), (housePos.z + 0.20f));
        GameObject chimneySmoke = (GameObject)Instantiate(chimneySmokePrefab, chimneySmokePos, Quaternion.identity);
        //chimneySmoke.transform.parent = this.gameObject.transform;

        // TODO: loop the particle system
        //imneySmoke.GetComponent<ParticleSystem>().enableEmission = true;
        ParticleSystem ps;
        ps = chimneySmoke.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.loop = true;
    }
}