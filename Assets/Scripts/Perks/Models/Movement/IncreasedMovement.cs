using UnityEngine;
using System.Collections;

public class IncreasedMovement : Perk {

    public IncreasedMovement() : base("Increased Movement", "", new PerkCategory("Movement"))
    {
        image = imageLocation + "Perk - Running2";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        player.AddMovementMultiplayer(0.25f);
    }
}
