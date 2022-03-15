using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TraversableLocation currentlyActiveLocation = TraversableLocation.LOCATION_1;
    [SerializeField] private List<GameObject> locations = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        ChangeLocation(TraversableLocation.LOCATION_1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLocation(TraversableLocation newLocation)
    {
        foreach(GameObject item in locations)
        {
            item.SetActive(false);
        }

        switch (newLocation)
        {
            case TraversableLocation.LOCATION_1:
                locations[0].SetActive(true);
                break;
            case TraversableLocation.LOCATION_2:
                locations[1].SetActive(true);
                break;
            case TraversableLocation.LOCATION_3:
                locations[2].SetActive(true);
                break;
        }

        currentlyActiveLocation = newLocation;
    }
}
