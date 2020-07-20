using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using UnityEngine;

// This could be altered at a later date.
using Force = UnityEngine.Vector3;

public class ForceContainer
{
    private readonly List<Force> m_Forces = new List<Force>();

    public ReadOnlyCollection<Force> Forces { get { return m_Forces.AsReadOnly(); } }
    public Force NetForce { get; private set; } // The net force applied to this physics object.

    public int NumOfForces()
    {
        return m_Forces.Count;
    }

    public void AddForce(Force force)
    {
        m_Forces.Add(force);
        NetForce += force;
    }

    public void RemoveForce(Force force)
    {
        bool removed = m_Forces.Remove(force);
        
        if (removed)
        {
            NetForce -= force;
        }
    }

    public void ClearForces()
    {
        m_Forces.Clear();
        NetForce = Vector3.zero;
    }
}
