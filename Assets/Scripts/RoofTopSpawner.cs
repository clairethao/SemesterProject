using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoofTopSpawner : MonoBehaviour
{
    public GameObject roofTop;
    public GameObject startingRoofTop;
    public int numOfRoofTops;
    public float roofTopMoveSpeed;
    public float randomHeight;
    public int gap;
    public GameObject mathZoneCanvas;

    private List<GameObject> rooftops = new List<GameObject>();
    private int rooftopCount = 0;
    private bool isPaused = false;



    void Start()
    {
        isPaused = true;
        numOfRoofTops = 46;
        roofTopMoveSpeed = Preferences.GetSpeedValue();
        rooftops.Add(startingRoofTop);
        float currentX = startingRoofTop.transform.position.x + 5f;
        
        for (int i = 0; i < numOfRoofTops; i++)
        {
            randomHeight = UnityEngine.Random.Range(4f, 6f);
            int gap = UnityEngine.Random.Range(2, 5);
            float roofTopWidth = 5f;
            Vector3 spawnPosition = new Vector3(currentX + gap, -3f, 0);
            GameObject rooftop = Instantiate(roofTop, spawnPosition, Quaternion.identity);
            RooftopTrigger trigger = rooftop.GetComponent<RooftopTrigger>();
            if (trigger != null)
            {
                trigger.mathZoneCanvas = mathZoneCanvas;
            }
            rooftops.Add( rooftop );
            rooftop.transform.localScale = new Vector3(roofTopWidth, randomHeight, 1f);
            currentX = spawnPosition.x + roofTopWidth;

            rooftopCount++;
            if ((rooftopCount + 1) % 3 == 0)
            {
                rooftop.tag = "mathZone";
            }

        }
        rooftops[rooftops.Count - 1].tag = "winZone";
    }

    public void UnpauseSpawner()
    {
        isPaused = false;
    }

    void Update()
    {
        if (isPaused) return;
        for (int i = rooftops.Count - 1; i >= 0; i--)
        {
            GameObject rooftop = rooftops[i];
            if (rooftop != null)
            {
                rooftop.transform.Translate(Vector2.left * roofTopMoveSpeed * Time.deltaTime);
                if (rooftop.transform.position.x < -20f)
                {
                    Destroy(rooftop);
                    rooftops.RemoveAt(i);
                }
            }
        }

    }
}
