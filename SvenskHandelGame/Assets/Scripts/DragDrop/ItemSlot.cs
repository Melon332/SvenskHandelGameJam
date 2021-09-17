using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<OrderUI>())
        {
            if (GetComponent<UIVehicle>().info.GetOrders().Count < GetComponent<UIVehicle>().info.vehicleData.size)
            {
                eventData.pointerDrag.transform.parent = this.transform;
                eventData.pointerDrag.gameObject.GetComponent<OrderUI>().AddOrder(GetComponent<UIVehicle>().info);
                GameManager.instance.UpdatePackages(GetComponent<UIVehicle>().info);
                if (GetComponent<UIVehicle>().info.Avaliable)
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                    eventData.pointerDrag.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
                    Destroy(eventData.pointerDrag.gameObject);
                }
            }
        }
    }
}
