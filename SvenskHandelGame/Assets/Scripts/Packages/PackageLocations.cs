using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class PackageLocations : MonoBehaviour
{
    public List<Package> packageWanted = new List<Package>();

    private bool hasBeenPassed;

    public int amountOfPackageWanted;
    
    [HideInInspector] public bool hasBeenUsed = false;
    private void Awake()
    {
        amountOfPackageWanted = 1;
        FindObjectOfType<PackageManager>().locationsList.Add(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenPassed == false)
        {
            //Gets the car that entered the box
            var car = other.GetComponent<VehicleScript>();
            if (car == null) return;
            if (car.positions.Contains(gameObject))
            {
                //Tells it to move to the next position
                car.positions.Remove(gameObject);
                car.CarMove();
                hasBeenPassed = true;
                //Removes the contained package
                foreach (var package in packageWanted)
                {
                    if (car.packages.Contains(package))
                    {
                        car.packages.Remove(package);
                    }
                    else
                    {
                        Debug.Log("Oh no!");
                    }
                }
                packageWanted.Clear();
                HasBeenDeactivated();
            }
        }
    }

    public void HasBeenActivated()
    {
        hasBeenUsed = true;
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void HasBeenDeactivated()
    {
        hasBeenUsed = false;
        GetComponent<MeshRenderer>().material.color = Color.grey;
    }
}
