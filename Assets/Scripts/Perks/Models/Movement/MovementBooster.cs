using UnityEngine;
using System.Collections;

public class MovementBooster : Perk {

    public MovementBooster() : base("Movement Boost", "", new PerkCategory("Movement"))
    {
        image = imageLocation + "Perk - Running5";
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ActivatePerk(PlayerProperties player)
    {
        
    }
}
