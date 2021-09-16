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
        float tempPath;
        float nearestPath = 999f;
        Vector3 positionToGoTo = new Vector3();
        if (positions.Count > 0)
        {
            foreach (var position in positions)
            {
                tempPath = Vector3.Distance(transform.position, position.transform.position);
                if (tempPath < nearestPath)
                {
                    nearestPath = tempPath;
                    positionToGoTo = position.transform.position;
                }
            }
            agent.SetDestination(positionToGoTo);
        }
        else
        {
            agent.SetDestination(office.transform.position);
            PackageManager.DeliveriesReset();
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
