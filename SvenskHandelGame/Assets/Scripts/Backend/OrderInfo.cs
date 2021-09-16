using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInfo
{
    private Package[] Packages;
    private float timer;
    private Consumer consumer;
    private OrderState orderState;
    private bool delivered = false;

    public OrderInfo(Package[] packages)
    {
        packages = packages;
        
    }
    
    
    
}

public struct Package
{
    private int size;
}

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