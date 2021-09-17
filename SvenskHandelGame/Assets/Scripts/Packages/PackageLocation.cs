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

    private bool active = false;
    
    
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
            SetActive(GameManager.HasActiveOrders(consumer));
            vs.CarMove();
        }
    }
    
    
    public void SetActive(bool active)
    {
        this.active = active;
        UpdateMaterial();
    }

    public void UpdateMaterial()
    {
        if (GameManager.instance.currentlySelectedOrder.consumer == consumer)
        {
            
        }
        foreach (var renderer in meshRenderers)
                {
                    Material[] mats = new Material[renderer.materials.Length];
                    for (int i = 0; i < renderer.materials.Length; i++)
                    {
                        
                        mats[i] =
                            active
                                ? GameManager.instance.gameColors.ActiveBuilding
                                : GameManager.instance.gameColors.IdleBuilding;
                        if (GameManager.instance.currentlySelectedOrder.consumer == consumer)
                        {
                            mats[i] = GameManager.instance.gameColors.SelectedBuilding;
                        }
                        
                        
                    }
        
                    renderer.materials = mats;
                }
    }

}
