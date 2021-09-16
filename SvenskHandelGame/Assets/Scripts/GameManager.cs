using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private List<Consumer> consumers;
    private List<OrderInfo> orders;
    private List<VehicleInfo> vehicles;
    
    public List<VehicleScriptableObject> vehiclePresets;
    
    public static GameManager instance;

    [SerializeField] private VehicleScript vehiclePrefab;

    [SerializeField] private UIVehicle vehicleUI;

    [SerializeField] private MailOffice office;

    [SerializeField] private GameObject vehicleLayoutGroup;


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

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {

        var locations = FindObjectsOfType<PackageLocation>();

        foreach (var location in locations)
        {
            Consumer consumer = new Consumer(location);
            location.consumer = consumer;
            consumers.Add(consumer);
        }

        vehicles = new List<VehicleInfo>();
        foreach (var v in vehiclePresets)
        {
            var vehicleInfo = new VehicleInfo(v.vehicleData);
            
            var vechileUI = Instantiate(vehicleUI,vehicleLayoutGroup.transform);

            vechileUI.GetComponent<UIVehicle>().Init(vehicleInfo);
            
            vehicleInfo.OnReset += () => {  vechileUI.ResetUI(); };

            vehicles.Add(vehicleInfo);

            for (int i = 0; i < 30;i++)
            {
                CreateNewOrder();
            }
        }
        
    }
    

    public void CreateNewOrder()
    {
        var consumer = consumers[Random.Range(0, consumers.Count)];
        PackageInfo[] package = new PackageInfo[1] {new PackageInfo()};
        OrderInfo order = new OrderInfo(package,consumer);
        
        orders.Add(order);
        
        
        //TODO: CREATE ORDER PREFAB
        
    }


    public void SendVehicle(VehicleInfo vehicleInfo)
    {
        //           TEMPORARY SOLUTION!!! TODO: REMOVE
        //                  ADD ORDERS TO CAR
        // ________________________________________________________
        
        foreach (var order in orders)
        {
            if (order.avaliable)
            {
                vehicleInfo.AddOrder(order);
            }
        }
        // ________________________________________________________
        
        
        
        foreach (var order in vehicleInfo.GetOrders())
        {
            order.avaliable = false;
        }
        vehicleInfo.Avaliable = false;
        var vehicle = Instantiate(vehiclePrefab, office.transform.position,quaternion.identity);
        vehicle.Init(vehicleInfo);
        
    }

    public void VehicleIsBack(VehicleInfo vehicleInfo)
    {
        vehicleInfo.Avaliable = true;
    }

}
public static class GameColors
{
    public static Color IdleBuilding = Color.gray;
    public static Color ActiveBuilding = Color.red;
}

