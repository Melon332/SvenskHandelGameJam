using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoBehaviour
{
    public List<PackageLocations> locationsList = new List<PackageLocations>();

    public List<Package> avaliablePackagesToRandomize = new List<Package>();

    public static bool hasNoDeliveriesLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AddRandomPackageToCitizen),0,15);
    }

    private void AddRandomPackageToCitizen()
    {
        if (!hasNoDeliveriesLeft)
        {
            foreach (var packageLocation in locationsList)
            {
                for (int i = 0; i < packageLocation.amountOfPackageWanted; i++)
                {
                    packageLocation.packageWanted.Add(
                        avaliablePackagesToRandomize[Random.Range(0, avaliablePackagesToRandomize.Count)]);
                }
            }
            DeliveriesReset();
        }
    }

    public PackageLocations GetPackage()
    {
        foreach (var location in locationsList)
        {
            if (location.packageWanted.Count > 0 && !location.hasBeenUsed)
            {
                location.HasBeenActivated();
                return location;
            }
        }
        return null;
    }

    public static void DeliveriesReset()
    {
        hasNoDeliveriesLeft = true;
    }

    public static void NoDeliveriesLeft()
    {
        hasNoDeliveriesLeft = false;
    }
}
