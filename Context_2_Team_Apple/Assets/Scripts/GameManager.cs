using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TraversableLocation currentlyActiveLocation = TraversableLocation.LOCATION_1;
    [SerializeField] private List<GameObject> locations = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI metalAmountText;
    [SerializeField] private TextMeshProUGUI foodAmountText;
    [SerializeField] private TextMeshProUGUI woodAmountText;

    private int lastKnownFoodAmount = 0;
    private int lastKnownWoodAmount = 0;
    private int lastKnownMetalAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //ChangeLocation(TraversableLocation.LOCATION_1);
        ResourceAmountUpdateChecker();
    }

    // Update is called once per frame
    void Update()
    {
        ResourceAmountUpdateChecker();
    }

    private void ResourceAmountUpdateChecker()
    {
        if(lastKnownWoodAmount != ResourceManager.wood || lastKnownMetalAmount != ResourceManager.metal || lastKnownFoodAmount != ResourceManager.food)
        {
            metalAmountText.text = ResourceManager.metal.ToString();
            foodAmountText.text = ResourceManager.food.ToString();
            woodAmountText.text = ResourceManager.wood.ToString();

            lastKnownFoodAmount = ResourceManager.food;
            lastKnownMetalAmount = ResourceManager.metal;
            lastKnownWoodAmount = ResourceManager.wood;
        }
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
