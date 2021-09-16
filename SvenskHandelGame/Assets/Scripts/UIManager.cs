using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerManager _playerManager;

    [SerializeField] private GameObject panel;

    [SerializeField] private Button packageButton;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        AddRandomizePackageButtonToPanel();
    }

    public void SendSelectedCar()
    {
        if (_playerManager.currentSelectedVehicle != null)
        {
            _playerManager.currentSelectedVehicle.CarMove(_playerManager.currentSelectedVehicle.positions);
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
        var button = Instantiate(packageButton, panel.transform);
        button.onClick.AddListener(delegate { AddPackageToCar(button,"test"); });
    }

    private void AddPackageToCar(Button button, string test)
    {
        var package = FindObjectOfType<PackageManager>();
        var location = package.GetPackage();
        if (_playerManager.currentSelectedVehicle != null)
        {
            if (_playerManager.currentSelectedVehicle.packages.Count <
                _playerManager.currentSelectedVehicle.maxAllowedOfPackages)
            {
                if (_playerManager.currentSelectedVehicle.positions.Contains(location.gameObject)) return;
                _playerManager.currentSelectedVehicle.AddLocationForCar(location.gameObject);
                _playerManager.currentSelectedVehicle.AddPackageToCar(package.GetPackage().packageWanted);
                Destroy(button.gameObject);
                AddRandomizePackageButtonToPanel();
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
