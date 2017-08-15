using UnityEngine;
using System.Collections;

public class IncreasedRange : Perk {

    public IncreasedRange() : base("Increased Range", "", new PerkCategory("Movement"))
    {
        image = imageLocation + "Perk - Running4";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
