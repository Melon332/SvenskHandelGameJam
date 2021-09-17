using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVehicle : MonoBehaviour
{
    [HideInInspector] public VehicleInfo info;


    public Transform Layout;
    public PackageSlot slotPrefab;
    public Transform ActivePanel;
    public Image Icon;
    

    public void Init(VehicleInfo info,Sprite icon)
    {
        this.info = info;
        Icon.sprite = icon;
        //GetComponentInChildren<Text>().text = info.vehicleData.vehicleType.ToString();
        ResetUI();
    }

    public void SelectVehicle()
    {
        if (!info.Avaliable) return;
        GameManager.instance.SendVehicle(info);
    }

    public void ResetUI()
    {
        ActivePanel.gameObject.SetActive(!info.Avaliable);
        FillSlots();
    }

    public void  FillSlots()
    {
        foreach (Transform t in Layout)
        {
            Destroy(t.gameObject);
        }
        
        for (int i = 0; i < info.vehicleData.size;i++)
        {
            Instantiate(slotPrefab, Layout).isFilled = i < info.GetOrders().Count;
        }
    }
    
    
    
    
    
}
