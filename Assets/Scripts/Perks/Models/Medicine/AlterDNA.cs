using UnityEngine;
using System.Collections;

public class AlterDNA : Perk {

    public AlterDNA() : base("Alter DNA", "", new PerkCategory("Medicine"))
    {
        image = imageLocation + "Perk - Medicine6";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
