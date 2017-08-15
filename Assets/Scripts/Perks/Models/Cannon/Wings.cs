using UnityEngine;
using System.Collections;

public class Wings : Perk {

    public Wings() : base("Wings", "", new PerkCategory("Cannon"))
    {
        image = imageLocation + "Perk - Cannon3";
    }

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
