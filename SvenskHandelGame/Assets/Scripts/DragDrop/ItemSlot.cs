using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var info = GetComponent<UIVehicle>().info;
        if (eventData.pointerDrag == null || !eventData.pointerDrag.GetComponent<OrderUI>() || info == null) return;
        if (info.GetOrders().Count < info.vehicleData.size && info.Avaliable)
        {
            eventData.pointerDrag.transform.parent = this.transform;
            eventData.pointerDrag.gameObject.GetComponent<OrderUI>().AddOrder(GetComponent<UIVehicle>().info);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
            GameManager.instance.UpdatePackages(info);
            Destroy(eventData.pointerDrag.gameObject);
        }
    }
}
