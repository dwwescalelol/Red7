using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : CardSlot
{ 
    public override Vector2 AddCard()
    {
        tempCard = CreateCard.card;
        CreateCard.card = null;
        dragger = tempCard.GetComponent<CardDragger>();
        return transform.position;
    }

    public Colours GetTopCardColour()
    {
        if (Cards.Count == 0)
            return Colours.RED;
        return Cards[Cards.Count - 1].Colour;
    }

    public void EndTurn()
    {
        if (tempCard == null)
            return;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        dragger.isNew = false;
        Cards.Add(tempCard);
        gm.cardsInPlay.Add(tempCard);
        tempCard = null;
    }
}
