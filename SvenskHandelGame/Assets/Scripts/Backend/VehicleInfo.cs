using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInfo
{
    private List<OrderInfo> orders = new List<OrderInfo>();
    private bool avaliable;
    
    public bool Avaliable
    {
        get => avaliable;
        set
        {
            if (avaliable == value) return;
            avaliable = value;
            InitiateVehicle();
        }
    }
    
    public VehicleData vehicleData;
    
    public VehicleInfo(VehicleData vehicleData)
    {
        this.vehicleData = vehicleData;
    }

    void InitiateVehicle()
    {
        orders = new List<OrderInfo>();
    }
    
}

public struct VehicleData
{
    private int   size;
    private int   speed;
    private float carbonFootprint;
    
    public VehicleData(int size,int speed,int carbonFootprint)
    {
        this.size =  size;
        this.speed = speed;
        this.carbonFootprint = carbonFootprint;
    }
    
}
