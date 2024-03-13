using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    [SerializeField] List<GameObject> cards = new List<GameObject>(); //list containing all of the possible cards set in unity inspector
    [SerializeField] Transform cardSpawnPoint; //point where cards appears. set through inspector

    // will get random card from the list and remove it
    GameObject GetRandomCard()
    {
        if(cards.Count == 0)
        {
            return null;
        }else{
            int randomCard = Random.Range(0,cards.Count-1);
            RemoveCard(randomCard);
            return cards[randomCard];
        }
    }



   // removes card from the list
    void RemoveCard(int cardIndex)
    {
        cards.RemoveAt(cardIndex);
    }

    // makes card appear on canvas. for now done by clicking the button for testing
    public void InstatiateCard()
    {
        GameObject card = Instantiate(GetRandomCard(), cardSpawnPoint.position, Quaternion.identity);
        card.transform.SetParent(cardSpawnPoint); // to not have a lot of different variables will instatiate as child of cardSpawnPoint (might change later)
    }
}
