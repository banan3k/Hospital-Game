using UnityEngine;
using System.Collections;

public class PerkCategory{

    public string name { get; set; }

    public PerkCategory(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
    }
}
