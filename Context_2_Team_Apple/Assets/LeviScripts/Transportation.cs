using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transportation : MonoBehaviour
{
    [SerializeField] Transform floodedCity;
    [SerializeField] Transform forest;

    private void Awake()
    {
        
    
    }

   
    public void GoToCity()
    {
        transform.position = floodedCity.position;
    }

    public void GoToForest()
    {
        transform.position = forest.position;
    }
}
