using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSlot : MonoBehaviour
{   
    [SerializeField]
    private GameObject filledSlot;
    
    private bool filled;
    public bool isFilled
    {
        get => filled;
        set
        {
            filled = value;
            filledSlot.SetActive(filled);
        }
    }
}
