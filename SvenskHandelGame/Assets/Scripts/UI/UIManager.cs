using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerManager _playerManager;

    [SerializeField] private GameObject panel;

    [SerializeField] private Button packageButton;

    private PackageManager package;

    public PanelPositions[] panelPositions;
    
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        AddRandomizePackageButtonToPanel();
        package = FindObjectOfType<PackageManager>();
    }

    public void SendSelectedCar()
    {
        if (_playerManager.currentSelectedVehicle != null && _playerManager.currentSelectedVehicle.positions.Count > 0)
        {
            if (_playerManager.currentSelectedVehicle.isAtMailOffice)
            {
                _playerManager.currentSelectedVehicle.CarMove(_playerManager.currentSelectedVehicle.positions);
                _playerManager.currentSelectedVehicle.isAtMailOffice = false;
            }
            else
            {
                Debug.Log("This car is currently patrolling!");
            }
        }
        else
        {
            Debug.Log("You don't have a car selected!");
        }
    }

    public void SelectCar(VehicleScript vehicle)
    {
        _playerManager.currentSelectedVehicle = vehicle;
    }

    private void AddRandomizePackageButtonToPanel()
    {
        foreach (var position in panelPositions)
        {
            if (!position.occupied)
            {
                var button = Instantiate(packageButton, position.transform);
                button.onClick.AddListener(delegate { AddPackageToCar(button); });
                button.GetComponentInChildren<Text>().text = "Hello there";
            }
        }
    }

    private void AddPackageToCar(Button button)
    {
        if (_playerManager.currentSelectedVehicle != null && _playerManager.currentSelectedVehicle.isAtMailOffice)
        {
            Debug.Log(package.GetPackage());
            if (_playerManager.currentSelectedVehicle.packages.Count <
                _playerManager.currentSelectedVehicle.maxAllowedOfPackages)
            {
                if (_playerManager.currentSelectedVehicle.positions.Contains(package.GetPackage().gameObject)) return;
                _playerManager.currentSelectedVehicle.AddLocationToCar(package.GetPackage().gameObject);
                _playerManager.currentSelectedVehicle.AddPackageToCar(package.GetPackage().packageWanted);
                Destroy(button.gameObject);
            }
            else
            {
                Debug.Log("I am full!");
            }
        }
        else
        {
            Debug.Log("Select a vehicle first!");
        }
    }
}
