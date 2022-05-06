using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using TMPro;
using System.Text;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> cardsInPlay = new List<Card>();

    //for collision between cards and cardslots to be easily detected
    public List<CardSlot> CardSlots;

    //as in base game
    public Canvas Canvas;
    public Player[] Players;

    //palette to make players from
    public Player playerPrefab;
    public Transform[] paletteSpawns;
    
    //UI
    public Image[] playerImages;
    public TextMeshProUGUI tip;
    public TextMeshProUGUI tipTitle;

    //Object that will spawn cards
    public CreateCard cardMaker;

    public static string[] Names;
    public int activePlayers;
    public static int numberPlayers;

    //tutorial
    public static bool withHints;
    public GameObject tutorial;

    //keep track of player turn
    private int currentPlayerTurn;
    private Player currentPlayer;

    //animation
    public Animator camAnim;

    //confetti
    public GameObject[] Confetti;
    public GameObject winScreen;
    public TextMeshProUGUI winText;

    private void Start()
    {
        camAnim = Camera.main.GetComponent<Animator>();
        ShowTutorial();
        MakeNewPlayers();
        SetupPlayers();
    }

    public void ShowTutorial()
    {
        if (withHints)
            tutorial.SetActive(true);
    }

    public void MakeNewPlayers()
    {
        activePlayers = numberPlayers;
        Players = new Player[numberPlayers];
        Player tempPlayer = playerPrefab;
        for (int i = 0; i < numberPlayers; i++)
        {
            tempPlayer = Instantiate(tempPlayer);
            CardSlots.Add(tempPlayer);
            Players[i] = tempPlayer;
            tempPlayer.transform.position = paletteSpawns[i].position;
            tempPlayer.Name = Names[i];
            tempPlayer.name = "Player " + i.ToString();
            tempPlayer.image = playerImages[i];
            playerImages[i].GetComponentInChildren<TextMeshProUGUI>().text = Names[i];
            playerImages[i].gameObject.SetActive(true);
        }
    }

    //all players set so that they can add one card to their decks
    public void SetupPlayers()
    {
        currentPlayerTurn = 0;
        activePlayers = numberPlayers;
        Player tempPlayer = playerPrefab;
        foreach (Player p in Players)
        {
            p.Cards.Clear();
            p.InPlay = true;
            p.isTurn = true;
            Canvas.InPlay = false;
            p.image.color = new Color(0, 0, 0);
        }
        tipTitle.text = "Setup";
        tip.text = "Each player take a card from the deck and place it infront of you. Make the card in the create card and drag it next to your name. After press Finish Start Up";
    }


    public bool PlayersReady()
    {
        //dont continue untill everyone has a card
        foreach (Player player in Players)
            if (player.tempCard == null)
                return false;

        //check if all cards are unique
        foreach (Player player in Players)
        {
            if (!IsCardUnique(player.tempCard))
                return false;
            cardsInPlay.Add(player.tempCard);
        }
        cardsInPlay.Clear();

        foreach (Player player in Players)
        {
            player.EndTurn();
        }

        SetTurnOrder();
        Canvas.InPlay = true;
        currentPlayer = Players[0];
        currentPlayer.StartTurn();

        tipTitle.text = "Tips";

        return true;
    }

    /// <summary>
    /// Finds the <see cref="Player"/> in <see cref="Players"/> that is currently winning,
    /// and sets the next <see cref="Player"/> to take the first turn as specified in the rules
    /// by sorting the <see cref="Players"/> by the TurnOrder assigned to each <see cref="Player"/>. 
    /// </summary>
    private void SetTurnOrder()
    {
        Player startPlayer = Rules.WhoIsWinning(this);
        int turnOrder = 0;
        bool isFound = false;

        for (int i = 0; turnOrder < Players.Length; i++)
        {
            //makes array circular
            if (i == Players.Length)
                i = 0;
            //player after current winning player goes first
            if (isFound)
            {
                Players[i].TurnOrder = turnOrder;
                turnOrder++;
            }
            if (Players[i].Equals(startPlayer))
            {
                isFound = true;
            }
        }
        SortPlayersByTurnOrder();
    }

    /// <summary>
    /// Insert sort to sort the <see cref="Players"/> in order of lowest <see cref="Player.TurnOrder"/>
    /// to highest.
    /// </summary>
    private void SortPlayersByTurnOrder()
    {
        for (int i = 0; i < Players.Length - 1; i++)
        {
            for (int j = i + 1; j > 0; j--)
            {
                if (Players[j - 1].TurnOrder > Players[j].TurnOrder)
                {
                    Player temp = Players[j - 1];
                    Players[j - 1] = Players[j];
                    Players[j] = temp;
                }
            }
        }
    }

    public void NextTurn()
    {
        //add temp cards to list and check if the move is legal
        //after remove the card

        if (IsLegalMove(currentPlayer))
        {
            currentPlayer.EndTurn();
            Canvas.EndTurn();

            currentPlayer = FindNextPlayer();
            currentPlayer.StartTurn();
        }
        else
        {
            StringBuilder winningCards = new StringBuilder();
            foreach(Card c in Rules.winningCards)
            {
                c.Wiggle();
                winningCards.Append(c.ToString() + " ");
            }
            tip.text = "Invalid move, the current winning cards are: " + winningCards; 
        }
    }

    private Player FindNextPlayer()
    {
        do
        {
            currentPlayerTurn++;

            if (currentPlayerTurn == numberPlayers)
                currentPlayerTurn = 0;
        }
        while (!Players[currentPlayerTurn].InPlay);

        return Players[currentPlayerTurn];
    }    

    public bool IsLegalMove(Player player)
    {
        currentPlayer.AddTempCard();
        Canvas.AddTempCard();
        bool result = player.Equals(Rules.WhoIsWinning(this));
        currentPlayer.RemoveTempCard();
        Canvas.RemoveTempCard();
        return result;
    }

    public void Surender()
    {
        currentPlayer.Surrender();
        activePlayers--;
        if (activePlayers == 1)
        {
            EndGame();
            return;
        }
        FindObjectOfType<AudioManager>().Play("Surrender");
        currentPlayer = FindNextPlayer();
        currentPlayer.StartTurn();
    }

    public void EndGame()
    {
        if (CreateCard.card != null)
            Destroy(CreateCard.card.gameObject);

        foreach(Card c in cardsInPlay)
            Destroy(c.gameObject);

        cardsInPlay.Clear();
        winScreen.SetActive(true);
        winText.text = FindNextPlayer().Name + "\nWins!";

        if(Canvas.tempCard != null)
            Destroy(Canvas.tempCard.gameObject);

        Canvas.gameObject.SetActive(false);

        foreach (GameObject g in Confetti)
            g.SetActive(true);

        FindObjectOfType<AudioManager>().Play("Victory");

    }

    public void RestartGame()
    {
        Canvas.gameObject.SetActive(true);
        winScreen.SetActive(false);

        foreach (GameObject g in Confetti)
            g.SetActive(false);

        SetupPlayers();
    }

    public Vector2 GetCardMakerPosition()
    {
        return cardMaker.transform.position;
    }

    public bool IsCardInPlay(Card card)
    {
        //check if exist as finalised card
        foreach (Card c in cardsInPlay)
            if (c.CompareTo(card) == 0)
                return true;

        //check if temp card
        Card playerTemp = Players[currentPlayerTurn].tempCard;
        Card canvasTemp = Canvas.tempCard;
        if (playerTemp != null && card.CompareTo(playerTemp) == 0)
            return true;
        if (canvasTemp != null && card.CompareTo(canvasTemp) == 0)
            return true;

        return false;
    }

    private bool IsCardUnique(Card card)
    {
        foreach (Card c in cardsInPlay)
        {
            if (c.CompareTo(card) == 0)
                return false;

        }
        return true;
    }
}
