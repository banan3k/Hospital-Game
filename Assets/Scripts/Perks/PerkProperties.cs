using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

static public class PerkProperties {
    static private Dictionary<int, List<string>> PlayersPerksList;
    static private List<Perk> PerksList;
    static private List<PerkCategory> PerkCategories;

    static public List<string> GetActivePerks(int number)
    {
        if (PlayersPerksList == null)
            PlayersPerksList = new Dictionary<int, List<string>>();

        if (!PlayersPerksList.ContainsKey(number))
            PlayersPerksList[number] = new List<string>();

        return PlayersPerksList[number];
    }

    static public void SetActivePerk(int number, Perk perk)
    {
        PlayersPerksList[number].Add(perk.GetName());
    }

    static public List<Perk> GetAllPerks()
    {
        if (PerksList == null)
        {
            PerksList = new List<Perk>{
                new Sprint(),
                new IncreasedMovement(),
                new PowerBoots(),
                new IncreasedRange(),
                new MovementBooster(),
                new IncreasedHealing(),
                new Surgeon(),
                new FieldTraining(),
                new Analysts(),
                new Chemist(),
                new AlterDNA(),
                new Doctorate(),
                new ImprovedBatteries(),
                new ExtraPower(),
                new Wings(),
                new PowerCannon(),
                new Helmet(),
                new ImprovedAccuracy()
            };
        }

        return PerksList;
    }

    static public List<PerkCategory> GetAllPerkCategories()
    {
        if (PerkCategories == null)
        {
            PerkCategories = new List<PerkCategory>{
                new PerkCategory("Movement"),
                new PerkCategory("Medicine"),
                new PerkCategory("Cannon")
            };
        }

        return PerkCategories;
    }

    static public List<Perk> GetThreeRandomPerks(int number)
    {
        List<Perk> list = new List<Perk>();
        if (PerksList == null)
            PerkProperties.GetAllPerks();

        if (PerkCategories == null)
            PerkProperties.GetAllPerkCategories();

        foreach(PerkCategory cat in PerkCategories)
        {
            bool perkFound = false;
            List<Perk> perks = PerksList.FindAll(p => p.category.name == cat.name);

            do
            {
                int index = new System.Random().Next(0, perks.Count);
                if (!HasPerkActivated(number, perks[index]))
                {
                    list.Add(perks[index]);
                    perkFound = true;
                }
            } while (!perkFound);
        }
        return list;
    }

    static public bool HasPerkActivated(int number, Perk perk)
    {
        List<string> activatedPerks = PerkProperties.GetActivePerks(number);
        foreach (string activatedPerk in activatedPerks)
            if (activatedPerk.Equals(perk.GetName()))
                return true;

        return false;
    }
}
