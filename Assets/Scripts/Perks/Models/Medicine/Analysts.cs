using UnityEngine;
using System.Collections;

public class Analysts : Perk {

    public Analysts() : base("Analyst", "", new PerkCategory("Medicine"))
    {
        image = imageLocation + "Perk - Medicine4";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        player.AddExtraChancesMultiplier(2);
    }
}
