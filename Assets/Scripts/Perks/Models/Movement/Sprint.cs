using UnityEngine;
using System.Collections;

public class Sprint : Perk {

    public Sprint() : base("Sprint", "", new PerkCategory("Movement"))
    {
        image = imageLocation + "Perk - Running1";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
