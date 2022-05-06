using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CardSlot
{
    public Image image;
    public Transform[] cardPositions;

    public string Name;
    public int TurnOrder;

    public void StartTurn()
    {
        gm.tip.text = "Its " + Name + "'s turn, the rule is " + gm.Canvas.GetTopCardColour() + "\n If you have no cards or cannot play a card press the surrender button.";
        isTurn = true;
        image.color = new Color(0.7f, 0,0.1f);
    }

    public override Vector2 AddCard()
    {
        if (IsTurnAndInPlay())
        {
            tempCard = CreateCard.card;
            CreateCard.card = null;
            //get dragger so can set isNew
            dragger = tempCard.GetComponent<CardDragger>();
            return cardPositions[Cards.Count].position;
        }

        return gm.GetCardMakerPosition();
    }

    public void EndTurn()
    {
        image.color = new Color(0, 0, 0);
        isTurn = false;

        if (tempCard != null)
        {
            dragger.isNew = false;
            Cards.Add(tempCard);
            gm.cardsInPlay.Add(tempCard);
            tempCard = null;
        }
    }

    public override bool CanAddCard()
    {
        return InPlay && isTurn && tempCard == null;
    }


    public void Surrender()
    {
        InPlay = false;
        isTurn = false;
        image.color = new Color(0, 0, 0, 0.5f);

        SpriteRenderer cardSprite;
        foreach (Card card in Cards)
        {
            cardSprite = card.GetComponent<SpriteRenderer>();
            cardSprite.material.SetFloat("_GrayscaleAmount", 1);
        }
    }

}
