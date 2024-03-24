using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    [SerializeField] public string region; // region name

    [SerializeField] public TypeOfTroops typeOfTroops;
    [SerializeField] public CardType cardType;
    public enum TypeOfTroops // enum for type of troops
    {
        Infatry,
        Artillery,
        cavalry,
        WildCard

    };

    //enum for type of card
    public enum CardType
    {
        Territory,
        WildCard

    };

}
