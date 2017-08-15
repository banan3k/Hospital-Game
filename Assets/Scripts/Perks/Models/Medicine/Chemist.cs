using UnityEngine;
using System.Collections;

public class Chemist : Perk {

    public Chemist() : base("Chemist", "", new PerkCategory("Medicine"))
    {
        image = imageLocation + "Perk - Medicine5";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
