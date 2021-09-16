using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class PackageLocations : MonoBehaviour
{
    public List<Package> packageWanted = new List<Package>();

    public int amountOfPackageWanted;
    
    [HideInInspector] public bool hasBeenUsed = false;

    public Consumer consumer;
    private void Awake()
    {
        amountOfPackageWanted = 1;
        FindObjectOfType<PackageManager>().locationsList.Add(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Gets the car that entered the box
        var car = other.GetComponent<VehicleScript>();
        if (car == null) return;
        if (car.vehicleInfo.ShouldLeavePackage(consumer))
        {
            //Tells it to move to the next position and deliver a package
            car.vehicleInfo.DeliverPackage(consumer);
            car.CarMove();
        }
    }

    public void HasBeenActivated()
    {
        hasBeenUsed = true;
        foreach (var renderer in GetComponents<MeshRenderer>())
        {
            renderer.material.color = Color.red;
        }
    }

    public void HasBeenDeactivated()
    {
        hasBeenUsed = false;
        foreach (var renderer in GetComponents<MeshRenderer>())
        {
            renderer.material.color = Color.grey;
        }
    }
}
