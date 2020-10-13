using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class TreasureHint : MonoBehaviour
{
    [SerializeField]
    private List<string> Hints = new List<string>(); 

    /// <summary>
    /// Add hints to component
    /// </summary>
    /// <param name="hints"> Hints associated with this object</param>
    public void SetHints(string[] hints)
    {
        if(hints.Length == 0) Debug.LogWarning("Set Hints being passes an empty array"); 
        Hints.AddRange(hints); 
    }

    /// <summary>
    /// Get Hints
    /// </summary>
    /// <returns></returns>
    public string[] GetHints()
    {
        return Hints.ToArray(); 
    }

    /// <summary>
    /// Reset Hints to an Empty List
    /// </summary>
    public void ResetHints()
    {
        Hints = new List<string>(); 
    }
    
}
