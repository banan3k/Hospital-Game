using UnityEngine;
using System.Collections;

public class FieldTraining : Perk {

    public FieldTraining() : base("Field Training", "", new PerkCategory("Medicine"))
    {
        image = imageLocation + "Perk - Medicine3";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
