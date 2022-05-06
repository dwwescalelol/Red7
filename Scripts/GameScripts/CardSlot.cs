using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardSlot : MonoBehaviour
{
    protected GameManager gm;

    public Card tempCard;
    public CardDragger dragger;

    public bool InPlay;
    public bool isTurn;

    public List<Card> Cards;

    private void Start()
    {
        tempCard = null;
        gm = FindObjectOfType<GameManager>();
    }

    public virtual bool CanAddCard()
    {
        return tempCard == null;
    }

    public bool IsTurnAndInPlay()
    {
        return InPlay && isTurn;
    }

    public void AddTempCard()
    {
        if (tempCard != null)
            Cards.Add(tempCard);
    }

    public void RemoveTempCard()
    {
        Cards.Remove(tempCard);
    }

    public void RemoveTopCard()
    {
        tempCard.GetComponent<Animator>().Play("fadeAway");
        Destroy(tempCard.gameObject, 0.5f);
        tempCard = null;
    }

    public abstract Vector2 AddCard();
}
