using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DeliveryPosition : MonoBehaviour
{
    
    public Action<VehicleScript> onCarEnter;

    private void OnTriggerEnter(Collider other)
    {
        //Gets the car that entered the box
        var car = other.GetComponent<VehicleScript>();
        if (car == null) return;
        onCarEnter?.Invoke(car);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 255, 0, .5f);
        
        Gizmos.DrawCube(transform.position,GetComponent<BoxCollider>().size);
    }
}
