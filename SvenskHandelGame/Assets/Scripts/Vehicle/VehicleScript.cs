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

    public VehicleInfo vehicleInfo;

    public bool isDoneDelivering = false;
    
    public void Init(VehicleInfo info)
    {
        agent = GetComponent<NavMeshAgent>();
        vehicleInfo = info;
        agent.speed = info.vehicleData.speed;
        GameObject mesh = null;
        switch (info.vehicleData.vehicleType)
        {
            case VehicleType.Bike:
                mesh = GameManager.instance.BikeMesh;
                break;
            case VehicleType.Car:
                mesh = GameManager.instance.CarMesh;
                break;
            case VehicleType.Truck:
                mesh = GameManager.instance.TruckMesh;
                break;
        }
        Instantiate(mesh, transform);
        CarMove();
    }
    public void CarMove()
    {
        float nearestPath = -1f;
        Vector3 positionToGoTo = new Vector3();
        var positions = this.vehicleInfo.GetOrderPositions();
        if (positions.Count > 0)
        {
            foreach (var position in positions)
            {
                float tempPath = Vector3.Distance(transform.position, position);
                if (tempPath < nearestPath || nearestPath == -1f)
                {
                    nearestPath = tempPath;
                    positionToGoTo = position;
                }
            }
            agent.SetDestination(positionToGoTo);
        }
        else
        {
            agent.SetDestination(GameManager.instance.office.position);
            isDoneDelivering = true;
        }
    }
}
