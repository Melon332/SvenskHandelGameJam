using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private List<Consumer> consumers;
    private List<OrderInfo> orders;
    private List<VehicleInfo> vehicles;
    
    public List<VehicleScriptableObject> vehiclePresets;
    
    public static GameManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Initialize() 
    {
        vehicles = new List<VehicleInfo>();
        foreach (var v in vehiclePresets)
        {
            var vehicleInfo = new VehicleInfo(v.vehicleData);
            
            // TODO: CREATE VEHICLE UI PREFABS
            // create the UI vehicles and assign them the vehicle info.
            //  EXAMPLE:    VehicleUI ui = Instantiate(uiprefab);

            vehicleInfo.OnReset += () => {   /* UI vehicle Reset method */ };

            vehicles.Add(vehicleInfo);
            
        }
        
    }

    public void CreateNewOrder()
    {
        var consumer = consumers[Random.Range(0, consumers.Count)];
        PackageInfo[] package = new PackageInfo[1] {new PackageInfo()};
        OrderInfo order = new OrderInfo(package,consumer);
        
        //TODO: CREATE ORDER PREFAB
        
    }


    public void SendVehicle(VehicleInfo vehicleInfo)
    {
        vehicleInfo.Avaliable = false;
        
        //TODO: CREATE ACTUAL VEHICLE WITH VEHICLE INFO
        
    }

    public void VehicleIsBack(VehicleInfo vehicleInfo)
    {
        vehicleInfo.Avaliable = true;
    }
    
    
    
    






}
