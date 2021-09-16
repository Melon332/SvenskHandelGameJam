using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVehicle : MonoBehaviour
{
    private VehicleInfo info;

    public void Init(VehicleInfo info)
    {
        this.info = info;
    }

    public void SendVehicle()
    {
        if (!info.Avaliable) return;
        GameManager.instance.SendVehicle(info);
    }

    public void ResetUI()
    {
        
    }
}
