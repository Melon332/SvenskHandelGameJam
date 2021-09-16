using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderInfo
{
    public Consumer consumer { get; private set;}
    public PackageInfo[] packages { get; private set;}
    private float timer;
    private OrderState orderState;
    private bool delivered = false;


    public Action OnDelivered;
    

    public OrderInfo(PackageInfo[] packages,Consumer consumer)
    {
        this.packages = packages;
        this.consumer = consumer;
    }


    public void SetDelivered()
    {
        delivered = true;
        OnDelivered?.Invoke();
    }
    
    
    
    
}

[System.Serializable]
public struct PackageInfo
{
    public int size;
    public PackageInfo(int size =1)
    {
        this.size = size;
    }
}

[System.Serializable]
public class Consumer
{
    public string name;
    public Sprite portrait;
    public Vector3 position;
}

public enum OrderState
{
    INVALID = 0,
    InTime,
    Late,
    VeryLate,
    Cancelled
}