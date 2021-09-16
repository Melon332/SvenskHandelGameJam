using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoBehaviour
{
    public List<PackageLocations> locationsList = new List<PackageLocations>();

    public List<Package> avaliablePackageToRandomize = new List<Package>();

    private PlayerManager _playerManager;
    // Start is called before the first frame update
    void Start()
    {
        AddRandomPackageToCitizen();

        _playerManager = FindObjectOfType<PlayerManager>();

        foreach (var teststuff in locationsList)
        {
            Debug.Log(teststuff.packageWanted.nameOfPackage);
        }
    }

    private void AddRandomPackageToCitizen()
    {
        foreach (var packageLocation in locationsList)
        {
            packageLocation.packageWanted = avaliablePackageToRandomize[Random.Range(0,avaliablePackageToRandomize.Count)];
        }
    }

    public PackageLocations GetPackage()
    {
        var package = locationsList[Random.Range(0, locationsList.Count)];
        if (package)
        {
            return locationsList[Random.Range(0, locationsList.Count)];
        }

        return null;
    }
}
