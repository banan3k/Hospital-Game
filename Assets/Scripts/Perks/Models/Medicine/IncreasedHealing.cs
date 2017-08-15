using UnityEngine;
using System.Collections;

public class IncreasedHealing : Perk {

    public IncreasedHealing() : base("Increased Healing", "", new PerkCategory("Medicine"))
    {
        image = imageLocation + "Perk - Medicine1";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
