using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopulationCollection : MonoBehaviour
{
    public static UnityAction<Citizen> AddCitizen;
    public static UnityAction<Citizen> DeleteCitizen;

    public List<Citizen> citizens = new List<Citizen>();

    private void Start()
    {
        Subscribe();
    }

    void AddCit(Citizen citizen)
    {
        citizens.Add(citizen);
    }

    void RemoveCit(Citizen citizen)
    {
        citizens.Remove(citizen);
    }

    void Subscribe()
    {
        AddCitizen += AddCit;
        DeleteCitizen += RemoveCit;
    }

    void Unsubscribe()
    {
        AddCitizen -= AddCit;
        DeleteCitizen -= RemoveCit;
    }
}