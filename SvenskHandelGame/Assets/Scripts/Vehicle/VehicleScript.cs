using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public abstract class VehicleScript : MonoBehaviour
{
    [HideInInspector] public MailOffice office;
    private NavMeshAgent agent;
    
    public float speed = 18f;

    public List<GameObject> positions = new List<GameObject>();

    public List<Package> packages = new List<Package>();

    public int maxAllowedOfPackages;

    [HideInInspector] public bool isAtMailOffice = true;

    public float carbonFootPrint = 5;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    public void CarMove()
    {
        if (positions.Count > 0)
        {
            agent.SetDestination(positions[0].transform.position);
        }
        else
        {
            agent.SetDestination(office.transform.position);
            PackageManager.DeliveriesReset();
            Debug.Log("Hello");
        }
    }

    public void AddPackageToCar(List<Package> package)
    {
        foreach (var packagesToAdd in package)
        {
            if (packages.Count < maxAllowedOfPackages)
            {
                packages.Add(packagesToAdd);
            }
            else
            {
                Debug.Log("I am full!");
            }   
        }
    }

    public void AddLocationToCar(GameObject location)
    {
        if (!positions.Contains(location))
        {
            positions.Add(location);
        }
    }
}
