using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MailOffice : MonoBehaviour
{
    private PlayerManager _playerManager;
    private void Awake()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _playerManager.officeToReturnTo = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        var deliveryTruck = other.GetComponent<VehicleScript>();
        if (deliveryTruck)
        {
            other.GetComponent<VehicleScript>().isAtMailOffice = true;
        }
    }
}
