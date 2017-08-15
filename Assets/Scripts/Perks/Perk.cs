using UnityEngine;
using System.Collections;

public abstract class Perk : MonoBehaviour {

    protected string perkName { get; set; }
    protected string image { get; set; }
    protected string imageLocation = "Images/Perks/";
    public bool isActivated { get; set; }

    public PerkCategory category { get; set; }

    public Perk(string name, string image, PerkCategory category)
    {
        this.perkName = name;
        this.image = image;
        this.category = category;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void ActivatePerk(PlayerProperties player);
    
    public override string ToString()
    {
        return perkName;
    }

    public string GetName()
    {
        return perkName;
    }

    public string GetCategory()
    {
        return category.GetName();
    }

    public string GetImage()
    {
        return image;
    }

    public bool IsActivated()
    {
        return isActivated;
    }

    public void SetActivated(bool activated)
    {
        this.isActivated = activated;
    }
}
