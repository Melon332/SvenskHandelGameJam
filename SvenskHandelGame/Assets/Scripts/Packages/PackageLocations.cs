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
        amountOfPackageWanted = Random.Range(2, 5);
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
    }
}
