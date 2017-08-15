using UnityEngine;
using System.Collections;

public class ImprovedBatteries : Perk {

    public ImprovedBatteries() : base("Improved Batteries", "", new PerkCategory("Cannon"))
    {
        image = imageLocation + "Perk - Cannon1";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
