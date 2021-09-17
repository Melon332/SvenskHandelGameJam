using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVehicle : MonoBehaviour
{
    [HideInInspector] public VehicleInfo info;

    public void Init(VehicleInfo info)
    {
        this.info = info;
        GetComponentInChildren<Text>().text = info.vehicleData.vehicleType.ToString();
    }

    public void SelectVehicle()
    {
        if (!info.Avaliable) return;
        GameManager.instance.SelectVehcile(info);
    }

    public void ResetUI()
    {
        
    }
}
