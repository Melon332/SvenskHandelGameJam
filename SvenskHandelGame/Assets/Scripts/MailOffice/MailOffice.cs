using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MailOffice : MonoBehaviour
{
    private PlayerManager _playerManager;

    private DeliveryPosition deliveryPosition;

    public Vector3 position => deliveryPosition.transform.position;
    private void Awake()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _playerManager.officeToReturnTo = this;

        deliveryPosition = GetComponentInChildren<DeliveryPosition>();
        
        if (deliveryPosition != null)
        {
            deliveryPosition.onCarEnter += CheckCollision;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var deliveryTruck = other.GetComponent<VehicleScript>();
        if (deliveryTruck == null) return;
        if (!deliveryTruck.isDoneDelivering) return;
        GameManager.instance.VehicleIsBack(deliveryTruck.vehicleInfo);
        Destroy(deliveryTruck.gameObject);
    }

    public void CheckCollision(VehicleScript vs)
    {
        if (!vs.isDoneDelivering) return;
        GameManager.instance.VehicleIsBack(vs.vehicleInfo);
        Destroy(vs.gameObject);
    }
    
    
}
