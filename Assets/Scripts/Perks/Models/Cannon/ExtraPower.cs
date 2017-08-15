using UnityEngine;
using System.Collections;

public class ExtraPower : Perk {

    public ExtraPower() : base("Extra Power", "", new PerkCategory("Cannon"))
    {
        image = imageLocation + "Perk - Cannon2";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        player.AddCannonPowerMultiplayer(0.25f);
    }
}
