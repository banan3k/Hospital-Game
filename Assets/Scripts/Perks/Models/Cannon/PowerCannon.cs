using UnityEngine;
using System.Collections;

public class PowerCannon : Perk {

    public PowerCannon() : base("Power Cannon", "", new PerkCategory("Cannon"))
    {
        image = imageLocation + "Perk - Cannon4";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
