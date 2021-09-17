using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public MailOffice office;

    [SerializeField] private GameObject vehicleLayoutGroup;

    public GameColors gameColors;
    
    [Space]
    [Header("Vehicle Meshes :")]
    public GameObject BikeMesh;
    public GameObject CarMesh;
    public GameObject TruckMesh;


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

        consumers = new List<Consumer>();

        orders = new List<OrderInfo>();

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
        PackageInformation[] package = new PackageInformation[1] {new PackageInformation()};
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
            order.consumer.location.SetActive(true);
        }
        vehicleInfo.Avaliable = false;
        var vehicle = Instantiate(vehiclePrefab, office.position,quaternion.identity);
        vehicle.Init(vehicleInfo);
        
    }

    public void VehicleIsBack(VehicleInfo vehicleInfo)
    {
        vehicleInfo.Avaliable = true;
    }

    public static bool HasActiveOrders(Consumer consumer)
    {
        return instance.orders.Where(order => !order.avaliable && !order.delivered).Any(order => order.consumer == consumer);
    }
    
    
    
}
[Serializable]
public struct GameColors
{
    public Material IdleBuilding;
    public Material ActiveBuilding;
}

