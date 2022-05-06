using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDragger : MonoBehaviour
{
	private Vector3 offset;
	private Vector3 origional;
	public bool isNew;

	public CardSlot slot;
	private GameManager gm;
	private BoxCollider2D cardCollider;
	private Animator animator;

	private void Awake()
	{
		gm = FindObjectOfType<GameManager>();
		cardCollider = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
		isNew = true;

		origional = gm.GetCardMakerPosition();
	}

	private bool CanMove()
    {
		return slot == null;
    }

	private void OnMouseDrag()
    {
		if (CanMove())
		{
			transform.position = GetMousePos() - offset;
			animator.Play("move");
		}
	}

	private void OnMouseDown()
	{
		if (CanMove())
			offset = GetMousePos() - transform.position;
	}

	//make canvas check indipendant, then can look through players
    private void OnMouseUp()
    {
		//check if it can be removed or if it can move
		if (!isNew)
			return;
		if (isNew && !CanMove())
		{
			slot.RemoveTopCard();
			return;
		}
		//if card is already in play return to the start position
		//this is ignored if the card is not the 
		if (gm.IsCardInPlay(CreateCard.card))
		{
			gm.tip.text = CreateCard.card.ToString() + " already in play";
			transform.position = origional;
			gm.camAnim.SetTrigger("shake");
			return;
		}

		foreach (CardSlot cardSlot in gm.CardSlots)
		{
			BoxCollider2D boxCollider = cardSlot.GetComponent<BoxCollider2D>();
			if (cardSlot.IsTurnAndInPlay() && cardCollider.Distance(boxCollider).distance <= 0)
			{
				//do not add card to stack
				if (!cardSlot.CanAddCard())
				{
					transform.position = origional;
					return;
				}

				origional = cardSlot.AddCard();
				slot = cardSlot;
				FindObjectOfType<AudioManager>().Play("PlaceCard");
				transform.position = origional;
				return;
			}
		}
		transform.position = origional;

	}

	private Vector3 GetMousePos()
	{
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
