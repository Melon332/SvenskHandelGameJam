using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    [HideInInspector] public OrderInfo orderInfo;

    public void Init(OrderInfo info, Canvas group)
    {
        orderInfo = info;
        GetComponentInChildren<Text>().text = info.consumer.ToString();
        GetComponent<DragDrop>().canvas = group;
    }

    public void AddOrder(VehicleInfo info)
    {
        info.AddOrder(orderInfo);
        Destroy(gameObject);
        Debug.Log("Added order");
    }
}
