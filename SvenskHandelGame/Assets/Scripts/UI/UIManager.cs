using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerManager _playerManager;

    [SerializeField] private GameObject panel;

    [SerializeField] private Button packageButton;

    public PanelPositions[] panelPositions;

    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        AddRandomizePackageButtonToPanel();
        
    }

    private void AddRandomizePackageButtonToPanel()
    {
        foreach (var position in panelPositions)
        {
            if (!position.occupied)
            {
                var button = Instantiate(packageButton, position.transform);
                //button.onClick.AddListener(delegate { AddPackageToCar(button); });
                button.GetComponentInChildren<Text>().text = "Hello there";
            }
        }
    }
/*
    private void AddPackageToCar(Button button)
    {
        if (_playerManager.currentSelectedVehicle != null && _playerManager.currentSelectedVehicle.vehicleInfo.Avaliable)
        {
            Debug.Log(package.GetPackage());
            if (_playerManager.currentSelectedVehicle.packages.Count <
                _playerManager.currentSelectedVehicle.vehicleInfo.vehicleData.size)
            {
                if (_playerManager.currentSelectedVehicle.positions.Contains(package.GetPackage().gameObject))
                    return;
                _playerManager.currentSelectedVehicle.AddLocationToCar(package.GetPackage().gameObject);
                _playerManager.currentSelectedVehicle.AddPackageToCar(package.GetPackage().packageWanted);
                package.GetPackage().HasBeenActivated();
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
    */
}
