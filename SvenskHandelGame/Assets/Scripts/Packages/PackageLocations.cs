using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PackageLocations : MonoBehaviour
{
    public Package packageWanted;

    private bool hasBeenPassed;
    private void Awake()
    {
        FindObjectOfType<PackageManager>().locationsList.Add(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenPassed == false)
        {
            //Gets the car that entered the box
            var car = other.GetComponent<VehicleScript>();
            if (car == null) return;
            //Tells it to move to the next position
            car.CarMove(car.positions);
            hasBeenPassed = true;
            //Removes the contained package
            if (car.packages.Contains(packageWanted))
            {
                car.packages.Remove(packageWanted);
                packageWanted = null;
            }
            else
            {
                Debug.Log("Oh no!");
            }
        }
    }
}
