using UnityEngine;
using System.Collections;

public class Helmet : Perk {

    public Helmet() : base("Helmet", "", new PerkCategory("Cannon"))
    {
        image = imageLocation + "Perk - Cannon5";
    }   

    public override void ActivatePerk(PlayerProperties player)
    {
        player.SetHasHelmet(true);
    }
}
