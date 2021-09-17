using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private List<Consumer> consumers;
    private List<OrderInfo> orders;
    [HideInInspector] public List<VehicleInfo> vehicles;
    [HideInInspector] public List<UIVehicle> vehicleUIs;
    
    public List<VehicleScriptableObject> vehiclePresets;
    
    public static GameManager instance;

    [SerializeField] private VehicleScript vehiclePrefab;

    [SerializeField] private UIVehicle vehicleUI;

    [SerializeField] private OrderUI orderUI;

    public MailOffice office;

    [SerializeField] private GameObject vehicleLayoutGroup, orderInfoLayoutGroup;

    private OrderInfo cso;

    public OrderInfo currentlySelectedOrder
    {
        get
        {
            return cso;
        }
        set
        {
            PackageLocation previouslocation = null;
            if (cso != null)
            {
                previouslocation = cso.consumer.location;
            }
            cso = value;
            cso?.consumer.location.UpdateMaterial();
            previouslocation?.UpdateMaterial();
        }
        
    }
    
    public GameColors gameColors;
    
    [Space]
    [Header("Vehicle Meshes :")]
    public GameObject BikeMesh;
    public GameObject CarMesh;
    public GameObject TruckMesh;
    [Space]
    [Header("Vehicle Icons :")]
    public Sprite bikesSprite;
    public Sprite carSprite;
    public Sprite truckSprite;

    public VehicleInfo currentCarSelected;

    [SerializeField] private Canvas mainCanvas;

    [SerializeField] private Text bikePackages, carPackages, TruckPackages;

    public float timer = 0f;
    public float timerInterval = 500f;


    


    private void SpawnOrders()
    {
        if ( Time.time > timer + timerInterval)
        {
            timer = Time.time;
            CreateNewOrder();
        }
    }
    
    

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
        InvokeRepeating(nameof(CreateNewOrder),0,5f);
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
            
            var vehicleUI = Instantiate(this.vehicleUI,vehicleLayoutGroup.transform).GetComponent<UIVehicle>();

            Sprite icon = bikesSprite;
            switch (vehicleInfo.vehicleData.vehicleType)
            {
                case VehicleType.Bike:
                    icon = bikesSprite;
                    break;
                case VehicleType.Car:
                    icon = carSprite;
                    break;
                case VehicleType.Truck:
                    icon = truckSprite;
                    break;
            }

            vehicleUI.Init(vehicleInfo,icon);
            
            vehicleInfo.onUpdate += () => {  vehicleUI.ResetUI(); };

            vehicleUIs.Add(vehicleUI);
            vehicles.Add(vehicleInfo);
            
        }
        for (int i = 0; i < 5;i++)
        {
            CreateNewOrder();
        }
        //AddNewOrderButtons();
        
    }
    

    public void CreateNewOrder()
    {
        var consumer = consumers[Random.Range(0, consumers.Count)];
        PackageInformation[] package = new PackageInformation[1] {new PackageInformation()};
        OrderInfo order = new OrderInfo(package,consumer);
        
        orders.Add(order);
        
        //TODO: CREATE ORDER PREFAB
        var button = Instantiate(orderUI, orderInfoLayoutGroup.transform);
        button.Init(order,mainCanvas);
        
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
            order.OnDelivered += () =>
            {
                foreach (var uiVehicle in vehicleUIs)
                {
                    if (uiVehicle.info == vehicleInfo)
                    {
                        uiVehicle.FillSlots();
                    }
                }
            };
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
        foreach (var UIVehicle in vehicleUIs)
        {
            UIVehicle.FillSlots();
        }

        bikePackages.text = "";
        carPackages.text = "";
        TruckPackages.text = "";
        
        /*
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
        */
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
    public Material SelectedBuilding;
}

