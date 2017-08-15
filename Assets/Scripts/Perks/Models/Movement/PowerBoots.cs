using UnityEngine;
using System.Collections;

public class PowerBoots : Perk {

    public PowerBoots() : base("Power Boots", "", new PerkCategory("Movement"))
    {
        image = imageLocation + "Perk - Running3";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
