using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderInfo
{
    public Consumer consumer { get; private set;}
    public PackageInformation[] packages { get; private set;}
    private float timer;
    private OrderState orderState;
    public bool delivered { get; private set; }
    public bool avaliable = true;

    public Action OnDelivered;
    

    public OrderInfo(PackageInformation[] packages,Consumer consumer)
    {
        this.packages = packages;
        this.consumer = consumer;
        delivered = false;
        avaliable = true;
    }

    public int GetSize()
    {
        int size = 0;
        foreach (var package in packages)
        {
            size += package.size;
        }
        return size;
    }


    public void SetDelivered()
    {
        delivered = true;
        OnDelivered?.Invoke();
    }

}

[System.Serializable]
public struct PackageInformation
{
    public int size;
    public PackageInformation(int size =1)
    {
        this.size = size;
    }
}

[System.Serializable]
public class Consumer
{
    public string name;
    public Sprite portrait;
    public PackageLocation location;
    public Vector3 position => location.position;

    public Consumer(PackageLocation location)
    {
        this.location = location;
    }
    
}

public enum OrderState
{
    INVALID = 0,
    InTime,
    Late,
    VeryLate,
    Cancelled
}