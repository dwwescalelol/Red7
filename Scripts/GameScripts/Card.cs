using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colours
{
	VIOLET,
	INDIGO,
	BLUE,
	GREEN,
	YELLOW,
	ORANGE,
	RED
}

public class Card : MonoBehaviour
{

    public void Wiggle()
    {
		Animator anim = GetComponent<Animator>();
		anim.Play("move");
    }

    #region gamestuff
    public Colours Colour;
	public int Number;

	public int CompareTo(object obj)
	{
        Card other = (Card)obj;
		//goes in order of Tie, check number first then colour
		if (Number == other.Number)
			return Colour.CompareTo(other.Colour);

		return Number.CompareTo(other.Number);
	}

	public override string ToString()
	{
		return Colour.ToString() + " " + Number;
	}

    #endregion
}
