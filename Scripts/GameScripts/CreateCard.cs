using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCard : MonoBehaviour
{
    public static Card card;
    public GameManager gm;
    public static int sortOrder;

    public Colours cardColour = Colours.VIOLET;
    public int cardNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        sortOrder = 100;
    }

    private void Update()
    {
        if(card == null && gm.activePlayers > 1)
            DrawCard();
    }

    public void DrawCard()
    {
        if (gm.deck.Count >= 1)
        {
            if(card != null && !gm.deck.Contains(card))
                Destroy(card.gameObject);

            card = FindCardInDeck(cardColour, cardNumber);

            if(card == null)
                return;

            FindObjectOfType<AudioManager>().Play("DrawCard");
            card.GetComponent<SpriteRenderer>().sortingOrder = sortOrder++;
            card = Instantiate(card);
            card.gameObject.SetActive(true);
            card.transform.position = transform.position;
        }
    }

    private Card FindCardInDeck(Colours colour, int number)
    {
        foreach (Card c in gm.deck)
            if (c.Colour == colour && c.Number == number)
                return c;

        return null;
    }

    public void SetCreateColour(string colour)
    {
        cardColour = (Colours)Enum.Parse(typeof(Colours), colour);
        DrawCard();
    }

    public void SetCreateNumber(int num)
    {
        cardNumber = num;
        DrawCard();
    }
}