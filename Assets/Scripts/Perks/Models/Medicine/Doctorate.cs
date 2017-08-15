using UnityEngine;
using System.Collections;

public class Doctorate : Perk {

    public Doctorate() : base("Doctorate", "", new PerkCategory("Medicine"))
    {
        image = imageLocation + "Perk - Medicine7";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
