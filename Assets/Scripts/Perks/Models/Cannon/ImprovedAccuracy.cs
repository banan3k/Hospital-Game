using UnityEngine;
using System.Collections;

public class ImprovedAccuracy : Perk {

    public ImprovedAccuracy() : base("Improved Accuracy", "", new PerkCategory("Cannon"))
    {
        image = imageLocation + "Perk - Cannon6";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
