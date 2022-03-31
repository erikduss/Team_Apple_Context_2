using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColour : MonoBehaviour
{
    RawImage image;
    
    [SerializeField] Color currentLocation;
    [SerializeField] Color notSelected;
    [SerializeField] Color trade;

    private void Start()
    {
        image = GetComponent<RawImage>();
    }
    public void CurrentlySelected()
    {
        image.color = currentLocation;
    }

    public void NotSelected()
    {
        image.color = notSelected;
    }

    public void TradeIcon()
    {
        image.color = trade;
    }
}
