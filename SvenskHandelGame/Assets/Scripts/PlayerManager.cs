using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public VehicleScript currentSelectedVehicle;
    public MailOffice officeToReturnTo;
    public List<VehicleScript> vehicleScripts = new List<VehicleScript>();

    private void Awake()
    {
        foreach (var vehicle in FindObjectsOfType<VehicleScript>())
        {
            vehicleScripts.Add(vehicle);
            vehicle.office = officeToReturnTo;
        }
        
    }
}
