using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendCars : MonoBehaviour
{
    public void Send()
    {
        foreach (var vechiles in GameManager.instance.vehicles)
        {
            GameManager.instance.SendVehicle(vechiles);
        }
        Debug.Log("Sent");
    }
}
