using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class TreasureHint : MonoBehaviour
{
    [SerializeField]
    private List<string> Hints = new List<string>(); 


    public void SetHints(string[] hints)
    {
        Hints.AddRange(hints); 
    }


    public string[] GetHints()
    {
        return Hints.ToArray(); 
    }
}
