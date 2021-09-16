using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PackageLocation : MonoBehaviour
{
    public Consumer consumer;
    public MeshRenderer[] meshRenderers;
    private DeliveryPosition deliveryPosition;
    public Vector3 position => deliveryPosition.transform.position;
    
    private void Awake()
    {
        deliveryPosition = GetComponentInChildren<DeliveryPosition>();
        if (deliveryPosition == null)
        {
            Debug.LogWarning("This building does not have a delivery position!!!");
            return;
        }
        deliveryPosition.onCarEnter += CheckCollision;
    }

    public void CheckCollision(VehicleScript vs)
    {
        if (vs.vehicleInfo.ShouldLeavePackage(consumer))
        {
            //Tells it to move to the next position and deliver a package
            vs.vehicleInfo.DeliverPackage(consumer);
            vs.CarMove();
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
