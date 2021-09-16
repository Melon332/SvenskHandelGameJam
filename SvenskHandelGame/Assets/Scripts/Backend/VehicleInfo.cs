using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VehicleInfo
{
    private List<OrderInfo> orders = new List<OrderInfo>();
    private bool avaliable;
    public Action OnReset;
    
    
    public bool Avaliable
    {
        get => avaliable;
        set
        {
            if (avaliable == value) return;
            avaliable = value;
            ResetVehicle();
        }
    }
    
    public VehicleData vehicleData;
    
    public VehicleInfo(VehicleData vehicleData)
    {
        this.vehicleData = vehicleData;
        avaliable = true;
        ResetVehicle();
    }

    void ResetVehicle()
    {
        orders = new List<OrderInfo>();
        OnReset?.Invoke();
    }
    
}

[System.Serializable]
public struct VehicleData
{
    public int   size;
    public float   speed;
    public float carbonFootprint;
    
    public VehicleData(int size,float speed,int carbonFootprint)
    {
        this.size =  size;
        this.speed = speed;
        this.carbonFootprint = carbonFootprint;
    }
    
}
