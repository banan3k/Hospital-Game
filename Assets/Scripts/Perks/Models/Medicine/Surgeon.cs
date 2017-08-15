using UnityEngine;
using System.Collections;

public class Surgeon : Perk {

    public Surgeon() : base("Surgeon", "", new PerkCategory("Medicine"))
    {
        image = imageLocation + "Perk - Medicine2";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
