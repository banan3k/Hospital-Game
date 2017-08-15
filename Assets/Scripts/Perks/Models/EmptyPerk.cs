using UnityEngine;
using System.Collections;

public class EmptyPerk : Perk {

    public EmptyPerk() : base("Empty Perk", "", new PerkCategory("Cannon"))
    {
        image = imageLocation + "Perk";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
