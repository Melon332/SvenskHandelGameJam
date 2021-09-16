using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderInfo
{
    private PackageInfo[] packages;
    private float timer;
    private Consumer consumer;
    private OrderState orderState;
    private bool delivered = false;

    public OrderInfo(PackageInfo[] packages,Consumer consumer)
    {
        this.packages = packages;
        this.consumer = consumer;
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
    private string name;
    private Sprite portrait;
}

public enum OrderState
{
    INVALID = 0,
    InTime,
    Late,
    VeryLate,
    Cancelled
}