using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI message;
    public GameObject referenceGuideImage;
    public GameObject canvasImage;
    public GameObject whosWinningImage;

    private int stages;

    private void Awake()
    {
        stages = 1;
        canvasImage.SetActive(false);
        whosWinningImage.SetActive(false);
    }

    public void NextStage()
    {
        switch(stages)
        {
            case 0:
                referenceGuideImage.SetActive(true);
                message.text = "Each player is given a colour reference guide. \n\n" +
                    "Each colour has a different rule assiciated with it.";
                break;
            case 1:
                message.text = "The card on the top of the canvas shows what the current rule is.\n" +
                    "If there is no card ontop of the canvas the rule is RED. \n " +
                    "Put the canvas in the middle of all players.";


                referenceGuideImage.SetActive(false);
                canvasImage.SetActive(true);
                break;
            case 2:
                message.text = "During your turn you can" +
                    "1: Play a card infront of you (to your palette)\n" +
                    "2: To the canvas to change the rule\n" +
                    "3: Both";
                break;
            case 3:
                message.text = "You must play a move that makes you win. \n\nIf you have no moves you must surender.";
                break;
            case 4:
                message.text = "In your deck there are two Who's Winning cards. \n\nThey explain what to do in the event of a tie.";
                canvasImage.SetActive(false);
                whosWinningImage.SetActive(true);
                break;
            case 5:
                message.text = "To start playing hand out one card face up to each player and log them in the companion" +
                    "\n\nThen hand out 7 cards to each player face down, they may look at them";
                break;
            case 6:
                message.text = "The player after the player with the highest card goes first" +
                    "\n\nYou are now playing Red 7. Good Luck!";
                break;
            case 7:
                gameObject.SetActive(false);
                break;
        }
        stages++;
    }

}


