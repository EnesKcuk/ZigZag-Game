using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallMaker : MonoBehaviour
{

    public Transform lastWall;
    public GameObject wallsprefab;
    Vector3 lastPos;
    Camera cam;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = lastWall.position;
        cam = Camera.main;
        player = FindObjectOfType<PlayerController>();
        InvokeRepeating("CreateWalls", 1, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateWalls()
    {
        float distance = Vector3.Distance(lastPos, player.transform.position);
        if (distance > cam.orthographicSize * 2) return;

        Vector3 newPos = Vector3.zero;
        int rand = Random.Range(0, 11);
        if(rand <=5)
        {
            newPos = new Vector3(lastPos.x+1, lastPos.y, lastPos.z);
        }
        else
        {
            newPos = new Vector3(lastPos.x, lastPos.y, lastPos.z+1);
        }
        GameObject newBlock = Instantiate(wallsprefab, newPos, Quaternion.Euler(0,90, 0), transform);
        newBlock.transform.GetChild(0).gameObject.SetActive(rand % 3 == 2);
        lastPos = newBlock.transform.position;
    }


}
