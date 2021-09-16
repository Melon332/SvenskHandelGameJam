using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public List<OrderInfo> GetOrders()
    {
        return orders.Where(order => !order.delivered).ToList();
    }

    public bool ShouldLeavePackage(Consumer consumer)
    {
        return orders.Any(order => !order.delivered && consumer == order.consumer);
    }
    

    public List<Vector3> GetOrderPositions()
    {
        return (from order in orders where !order.delivered select order.consumer.position).ToList();
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
