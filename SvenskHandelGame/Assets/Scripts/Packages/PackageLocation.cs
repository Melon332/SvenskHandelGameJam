using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class PackageLocation : MonoBehaviour
{
    public Consumer consumer;
    public MeshRenderer[] meshRenderers;
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
    public void SetActive(bool active)
    {
        foreach (var renderer in meshRenderers)
        {
            
            renderer.material.color = active ? GameColors.ActiveBuilding : GameColors.IdleBuilding ;
        }
    }

}
