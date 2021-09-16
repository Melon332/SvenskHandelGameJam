using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public abstract class VehicleScript : MonoBehaviour
{
    private NavMeshAgent agent;
    
    public float speed = 18f;

    public List<GameObject> positions = new List<GameObject>();

    public List<Package> packages = new List<Package>();

    public int maxAllowedOfPackages;
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    public void CarMove(List<GameObject> positions)
    {
        if (this.positions.Count > 0)
        {
            agent.SetDestination(positions[0].transform.position);
            this.positions.RemoveAt(0);
        }
        else
        {
            Debug.Log("Hello");
        }
    }

    public void AddPackageToCar(Package package)
    {
        if (packages.Count < maxAllowedOfPackages)
        {
            packages.Add(package);
        }
        else
        {
            Debug.Log("I am full!");
        }
    }

    public void AddLocationForCar(GameObject location)
    {
        if (!positions.Contains(location))
        {
            positions.Add(location);
        }
    }
}
