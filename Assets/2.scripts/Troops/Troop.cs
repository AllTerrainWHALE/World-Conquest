using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

class Troop : MonoBehaviour
{
    [Header("Infantry")]
    [SerializeField] GameObject blueInfantry;
    [SerializeField] GameObject cyanInfantry;
    [SerializeField] GameObject greenInfantry;
    [SerializeField] GameObject orangeInfantry;
    [SerializeField] GameObject pinkInfantry;
    [SerializeField] GameObject redInfantry;
    public GameObject Infantry(TroopColour troopColour) => troopColour switch
    {
        TroopColour.Blue => blueInfantry,
        TroopColour.Cyan => cyanInfantry,
        TroopColour.Green => greenInfantry,
        TroopColour.Orange => orangeInfantry,
        TroopColour.Pink => pinkInfantry,
        TroopColour.Red => redInfantry,
        _ => throw new InvalidEnumArgumentException(),
    };

    [Header("Cavalry")]
    [SerializeField] GameObject blueCavalry;
    [SerializeField] GameObject cyanCavalry;
    [SerializeField] GameObject greenCavalry;
    [SerializeField] GameObject orangeCavalry;
    [SerializeField] GameObject pinkCavalry;
    [SerializeField] GameObject redCavalry;
    public GameObject Cavalry(TroopColour troopColour) => troopColour switch
    {
        TroopColour.Blue => blueCavalry,
        TroopColour.Cyan => cyanCavalry,
        TroopColour.Green => greenCavalry,
        TroopColour.Orange => orangeCavalry,
        TroopColour.Pink => pinkCavalry,
        TroopColour.Red => redCavalry,
        _ => throw new InvalidEnumArgumentException(),
    };

    [Header("Artillery")]
    [SerializeField] GameObject blueArtillery;
    [SerializeField] GameObject cyanArtillery;
    [SerializeField] GameObject greenArtillery;
    [SerializeField] GameObject orangeArtillery;
    [SerializeField] GameObject pinkArtillery;
    [SerializeField] GameObject redArtillery;
    public GameObject Artillery(TroopColour troopColour) => troopColour switch
    {
        TroopColour.Blue => blueArtillery,
        TroopColour.Cyan => cyanArtillery,
        TroopColour.Green => greenArtillery,
        TroopColour.Orange => orangeArtillery,
        TroopColour.Pink => pinkArtillery,
        TroopColour.Red => redArtillery,
        _ => throw new InvalidEnumArgumentException(),
    };

    //public TroopColour GetTroopColour(GameObject troop) => troopName switch
    //{
    //    blueInfantry.name => TroopColour.Blue,
    //    blueCavalry.name => TroopColour.Blue,
    //    blueArtillery.name => TroopColour.Blue,
    //
    //    cyanInfantry.name => TroopColour.Cyan,
    //    cyanCavalry.name => TroopColour.Cyan,
    //    cyanArtillery.name => TroopColour.Cyan,
    //
    //    greenInfantry.name => TroopColour.Green,
    //    greenCavalry.name => TroopColour.Green,
    //    greenArtillery.name => TroopColour.Green,
    //
    //    orangeInfantry.name => TroopColour.Orange,
    //    orangeCavalry.name => TroopColour.Orange,
    //    orangeArtillery.name => TroopColour.Orange,
    //
    //    pinkInfantry.name => TroopColour.Pink,
    //    pinkCavalry.name => TroopColour.Pink,
    //    pinkArtillery.name => TroopColour.Pink,
    //
    //    redInfantry.name => TroopColour.Red,
    //    redCavalry.name => TroopColour.Red,
    //    redArtillery.name => TroopColour.Red,
    //};

    public TroopColour GetTroopColour(GameObject troop)
    {
        string troopName = troop.name.Replace("(Clone)", "");
        if (troopName == blueInfantry.name || troopName == blueCavalry.name || troopName == blueArtillery.name) return TroopColour.Blue;
        else if (troopName == cyanInfantry.name || troopName == cyanCavalry.name || troopName == cyanArtillery.name) return TroopColour.Cyan;
        else if (troopName == greenInfantry.name || troopName == greenCavalry.name || troopName == greenArtillery.name) return TroopColour.Green;
        else if (troopName == orangeInfantry.name || troopName == orangeCavalry.name || troopName == orangeArtillery.name) return TroopColour.Orange;
        else if (troopName == pinkInfantry.name || troopName == pinkCavalry.name || troopName == pinkArtillery.name) return TroopColour.Pink;
        else if (troopName == redInfantry.name || troopName == redCavalry.name || troopName == redArtillery.name) return TroopColour.Red;
        else throw new System.Exception($"Colour of troop cannot be identified: {troopName}");
    }
}

enum TroopTypes
{
    Infantry = 1,
    Cavalry = 5,
    Artillery = 10,
}

public enum TroopColour
{
    Blue,
    Cyan,
    Green,
    Orange,
    Pink,
    Red
}