using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private List<Consumer> consumers;
    private List<OrderInfo> orders;
    [HideInInspector] public List<VehicleInfo> vehicles;
    
    public List<VehicleScriptableObject> vehiclePresets;
    
    public static GameManager instance;

    [SerializeField] private VehicleScript vehiclePrefab;

    [SerializeField] private UIVehicle vehicleUI;

    [SerializeField] private OrderUI orderUI;

    public MailOffice office;

    [SerializeField] private GameObject vehicleLayoutGroup, orderInfoLayoutGroup;

    public GameColors gameColors;
    
    [Space]
    [Header("Vehicle Meshes :")]
    public GameObject BikeMesh;
    public GameObject CarMesh;
    public GameObject TruckMesh;

    public VehicleInfo currentCarSelected;

    [SerializeField] private Canvas mainCanvas;

    [SerializeField] private Text bikePackages, carPackages, TruckPackages;


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
        AddNewOrderButtons();
        
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
        /*
        foreach (var order in orders)
        {
            if (order.avaliable)
            {
                vehicleInfo.AddOrder(order);
            }
        }
        */
        
        // ________________________________________________________
        
        
        foreach (var order in vehicleInfo.GetOrders())
        {
            order.avaliable = false;
            order.consumer.location.SetActive(true);
        }

        if (vehicleInfo.Avaliable)
        {
            vehicleInfo.Avaliable = false;
            var vehicle = Instantiate(vehiclePrefab, office.position, quaternion.identity);
            vehicle.Init(vehicleInfo);
        }

    }

    public void SelectVehcile(VehicleInfo info)
    {
        currentCarSelected = info;
    }

    public void VehicleIsBack(VehicleInfo vehicleInfo)
    {
        vehicleInfo.Avaliable = true;
        UpdatePackages(vehicleInfo);
    }

    public static bool HasActiveOrders(Consumer consumer)
    {
        return instance.orders.Where(order => !order.avaliable && !order.delivered).Any(order => order.consumer == consumer);
    }

    private void AddNewOrderButtons()
    {
        foreach (var order in orders)
        {
            if (order.avaliable)
            {
               var button = Instantiate(orderUI, orderInfoLayoutGroup.transform);
               button.Init(order,mainCanvas);
            }
        }
    }

    public void UpdatePackages(VehicleInfo info)
    {
        if (info.vehicleData.vehicleType == VehicleType.Bike)
        {
            bikePackages.text = "The bike has: " + info.GetOrders().Count + "/" + info.vehicleData.size + " Packages";
        }
        else if (info.vehicleData.vehicleType == VehicleType.Car)
        {
            carPackages.text = "The car has: " + info.GetOrders().Count + "/" + info.vehicleData.size + " Packages";
        }
        else if (info.vehicleData.vehicleType == VehicleType.Truck)
        {
            TruckPackages.text = "The truck has: " + info.GetOrders().Count+"/"+ info.vehicleData.size + " Packages";
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
[Serializable]
public struct GameColors
{
    public Material IdleBuilding;
    public Material ActiveBuilding;
}

