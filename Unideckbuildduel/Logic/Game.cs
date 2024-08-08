using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unideckbuildduel.Logic.GameData;
using Unideckbuildduel.View;

namespace Unideckbuildduel.Logic
{
    /// <summary>
    /// A class used for the game's logic. One single instance at a time.
    /// </summary>
    public class Game
    {
        private Stack<Card> commonDeck;
        private Stack<Card> discardDeck;
        private List<Player> players;
        private Dictionary<Player, List<Card>> cards;
        private Dictionary<Player, List<Card>> buildings;
        public int nbCommonDeck { get { return commonDeck != null ? commonDeck.Count : 0; } set { } }
        public int nbDiscardDeck { get { return discardDeck != null ? discardDeck.Count : 0; } set { } }

        public CardType effectCard { get; set; }
        public CardType drawEffectCard { get; set; }

        public CardType drawDiscardEffectCard { get; set; }
        public bool currentPlayerCanDraw { get { return players[CurrentPlayer].canDraw; } }

        public bool currentPlayerCanDrawFromDiscard { get { return players[CurrentPlayer].canDrawFromDiscard; } }

        public bool canPlayAgain { get; set; } = false;
        public List<CardType> substitutes { get; set; } = new List<CardType>();

        public List<Effect> effectsAvailables { get; set; } = new List<Effect>();

        /// <summary>
        /// A reference to the single instance of this class
        /// </summary>
        public static Game GetGame { get; } = new Game();
        /// <summary>
        /// Turn number (from 0)
        /// </summary>
        public int Turn { get; set; }
        /// <summary>
        /// The current phase
        /// </summary>
        public GameStatus GameStatus { get; private set; }
        /// <summary>
        /// The current player (0-1)
        /// </summary>
        public int CurrentPlayer { get; private set; }
        private Game() { }
        /// <summary>
        /// Method used to launch a new game (at startup or after)
        /// </summary>
        public void NewGame(string playerOneName, string playerTwoName)
        {
            discardDeck = new Stack<Card>();
            commonDeck = LoadData.GenStack();
            nbCommonDeck = commonDeck.Count;
            players = new List<Player> { new Player { Name = playerOneName }, new Player { Name = playerTwoName } };
            cards = new Dictionary<Player, List<Card>>();
            buildings = new Dictionary<Player, List<Card>>();
            foreach (Player p in players)
            {
                p.Number = players.IndexOf(p);
                cards.Add(p, new List<Card>());
                buildings.Add(p, new List<Card>());
            }
            GameStatus = GameStatus.TurnStart;
            CurrentPlayer = 0;
            Turn = 0;
            nbCommonDeck = commonDeck.Count;
            nbDiscardDeck = discardDeck.Count;
        }
        /// <summary>
        /// Method called to end the discard phase
        /// </summary>
        public void DiscardPhaseEnded()
        {
            GameStatus = GameStatus.Ended;
        }

        public void StartDiscardPhase()
        {
            GameStatus = GameStatus.Discarding;
        }

        public void StartExchangingPhase()
        {
            GameStatus = GameStatus.Exchanging;
        }

        public void RestartPlaying()
        {
            GameStatus = GameStatus.Playing;
        }

        /// <summary>
        /// Try to play a specific card.
        /// </summary>
        /// <param name="playerNum">The number of the player</param>
        /// <param name="cardNum">The number of the card</param>
        /// <returns>msg: a string containing a message, ok: true iff the card could be played</returns>
        public (string msg, bool ok) PlayCard(int playerNum, int cardNum)
        {
            Card card = cards[players[playerNum]][cardNum];
            return PlayCard(playerNum, card);
        }
        private (string msg, bool ok) PlayCard(int playerNum, Card card)
        {
            if (card == null) { return ("Card playing error", false); }
            switch (card.CardType.Kind)
            {
                case Kind.Building:
                    bool needValidation = false;
                    var reqBs = card.CardType.RequiredBuildings;
                    if (reqBs != null && reqBs.Count > 0)
                    {
                        bool reqBok = true;
                        foreach (CardType b in reqBs.Keys)
                        {
                            int presB = NumberOfCardsPresent(buildings[players[playerNum]], b);
                            if (presB < reqBs[b])
                            {
                                reqBok = false;
                            }
                        }
                        if (!reqBok)
                        {
                            return ("Not enough required buildings", false);
                        }
                    }

                    var reqRs = card.CardType.RequiredRessources;
                    foreach (var substitute in substitutes)
                    {
                        if (reqRs != null)
                        {
                            if (reqRs.ContainsKey(substitute))
                            {
                                reqRs.Remove(substitute);
                            }
                        }
                    }

                    if (reqRs != null && reqRs.Count > 0)
                    {
                        bool reqRok = true;
                        foreach (CardType b in reqRs.Keys)
                        {
                            int presR = NumberOfCardsPresent(cards[players[playerNum]], b);
                            if (presR < reqRs[b])
                            {
                                reqRok = false;
                            }
                        }
                        if (!reqRok)
                        {
                            return ("Not enough required ressources", false);
                        }
                        else
                        {
                            needValidation = true;
                            if (NeedValidation())
                            {
                                needValidation=false;
                                foreach (CardType b in reqRs.Keys)
                                {
                                    int nbRs = 0;
                                    for (int i = 0; i < cards[players[playerNum]].Count; i++)
                                    {
                                        if (cards[players[playerNum]][i].CardType == b && nbRs < reqRs[b])
                                        {

                                            DiscardCard(playerNum, i);
                                            nbRs++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!needValidation)
                    {
                        playEffect(card);
                        nbDiscardDeck = discardDeck.Count;
                        buildings[players[playerNum]].Add(card);
                        cards[players[playerNum]].Remove(card);
                        players[playerNum].Points += card.CardType.Points;
                        Controller.GetControler.NewBuilding(playerNum, card);
                        Controller.GetControler.DisplayHand(CurrentPlayer, cards[players[CurrentPlayer]]);
                    }
                    break;
                case Kind.Action:
                    if (NeedValidation())
                    {
                        if (playAction(card))
                        {
                            DiscardCard(playerNum, card);
                            nbDiscardDeck = discardDeck.Count;
                            Controller.GetControler.DisplayHand(CurrentPlayer, cards[players[CurrentPlayer]]);
                        }
                        else
                        {
                            return ("Not enough required ressources", false);
                        }
                    }
                    break;
                default:
                    return ("Card type not handled yet", false);
            }
            return (null, true);
        }

        public bool NeedValidation()
        {
            bool validation = false;
            var message = "Are you sure to play this card ?";
            var title = "Hey!";
            var result = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    validation = true;
                    break;
                case DialogResult.No:
                    validation = false;
                    break;
                default:
                    validation = false;
                    break;
            }
            return validation;
        }

        private void playEffect(Card card)
        {

            switch (card.CardType.Effect)
            {
                case Effect.CanExchange:
                    effectCard = card.CardType.EffectCard;
                    Window.GetWindow.WriteLine("You can Exchange any :"+ effectCard.Name+ " for this turn.");
                    StartExchangingPhase();
                    Play();
                    break;
                case Effect.OneMoreCard:
                    UpdatePlayerHandSize(CurrentPlayer, 6);
                    Window.GetWindow.WriteLine("+1 card in your hand, forever.");
                    break;
                case Effect.DrawOncePerTurn:
                    Controller.GetControler.enabledDeck();
                    drawEffectCard = card.CardType.EffectCard;
                    players[CurrentPlayer].canDraw = true;
                    Window.GetWindow.WriteLine("You can draw one card from deck per turn now.");
                    break;
                case Effect.DrawFromDiscardOncePerTurn:
                    Controller.GetControler.enabledDrawFromDiscard();
                    drawDiscardEffectCard = card.CardType.EffectCard;
                    players[CurrentPlayer].canDrawFromDiscard = true;
                    Window.GetWindow.WriteLine("You can draw one card from discard per turn now.");
                    break;
                case Effect.ProducesOne:
                    cards[players[CurrentPlayer]].Add(new Card { CardType = card.CardType.EffectCard });
                    Window.GetWindow.WriteLine(card.CardType.Name + " will produce for you one " +card.CardType.EffectCard.Name + " per turn.");
                    break;
                case Effect.PlayAgain:
                    canPlayAgain = true;
                    Window.GetWindow.WriteLine("You got a new free turn.");
                    break;
                case Effect.Substitute:
                    substitutes.Add(card.CardType.EffectCard);
                    Window.GetWindow.WriteLine("You don't need " + card.CardType.EffectCard.Name + " for this turn for build or play action.");
                    break;
                default:
                    break;
            }
        }

        private bool playAction(Card card)
        {
            bool isPlayable = false;
            switch (card.CardType.Name)
            {
                case "Official Visit":
                    var EfcReqCd = card.CardType.EffectRequiredCard;
                    List<Card> playerHand = cards[players[CurrentPlayer]];
                    if (NumberOfCardsPresent(playerHand, EfcReqCd) > 0)
                    {
                        isPlayable = true;
                        int i = 0;
                        bool found = false;

                        if (!substitutes.Contains(EfcReqCd))
                        {
                            while (i < playerHand.Count && !found)
                            {
                                if (playerHand[i].CardType.Equals(EfcReqCd))
                                {
                                    DiscardCard(CurrentPlayer, playerHand[i]);
                                    found = true;
                                }
                                i++;
                            }
                        }

                        playEffect(card);
                    }

                    if (substitutes.Contains(EfcReqCd) && EfcReqCd != null)
                    {
                        isPlayable = true;
                        playEffect(card);
                    }

                    break;
                case "Over-hyped Keyword":
                    isPlayable = true;
                    playEffect(card);

                    break;
                case "Prestigious Award":
                    isPlayable = true;
                    playEffect(card);
                    break;
            }
            return isPlayable;

        }
        private static int NumberOfCardsPresent(List<Card> cards, CardType type)
        {
            if (cards == null || cards.Count == 0 || type == null)
            {
                return 0;
            }
            int res = 0;
            foreach (Card card in cards)
            {
                if (card.CardType.Equals(type))
                {
                    res++;
                }
            }
            return res;
        }
        /// <summary>
        /// Method called when the game should advance; sometimes recursive, sometimes not.
        /// </summary>
        public void Play()
        {
            switch (GameStatus)
            {
                case GameStatus.TurnStart:
                    GameStatus = GameStatus.Drawing;
                    Play();
                    break;
                case GameStatus.Drawing:
                    Controller.GetControler.DrawStart(CurrentPlayer);
                    break;
                case GameStatus.Playing:
                    Controller.GetControler.DisplayHand(CurrentPlayer, cards[players[CurrentPlayer]]);
                    Controller.GetControler.PlayPhaseStart(CurrentPlayer);
                    break;
                case GameStatus.Discarding:
                    Controller.GetControler.DiscardStart(CurrentPlayer);
                    break;
                case GameStatus.Exchanging:
                    Controller.GetControler.ExchangeStart(CurrentPlayer);
                    break;
                case GameStatus.Ended:

                    CurrentPlayer = (CurrentPlayer + 1) % players.Count;
                    if (CurrentPlayer == 0)
                    {
                        if (Controller.GetControler.NumbersOfTurnsToGo > Turn)
                        {
                            Turn++;
                        }
                    }
                    Controller.GetControler.TurnEnded(CurrentPlayer, Turn);

                    if (Controller.GetControler.NumbersOfTurnsToGo > Turn)
                    {
                        GameStatus = GameStatus.TurnStart;
                        Play();
                    }
                    break;
            }
            //nbCommonDeck = commonDeck.Count;
        }
        /// <summary>
        /// Method called to discard a specific card.
        /// </summary>
        /// <param name="playerNum">The number of the player</param>
        /// <param name="cardNum">The number of the card</param>
        /// <returns>True iff the card was actually discarded</returns>
        public bool DiscardCard(int playerNum, int cardNum)
        {
            Card card = cards[players[playerNum]][cardNum];
            return DiscardCard(playerNum, card);
        }
        private bool DiscardCard(int playerNum, Card card)
        {
            if (card == null) { return false; }
            discardDeck.Push(card);
            return cards[players[playerNum]].Remove(card);
        }
        /// <summary>
        /// Draw one card for a player from the deck
        /// </summary>
        /// <param name="num">The number of the player</param>
        /// <returns>A reference to the drawn card, also added to the player's hand</returns>
        public Card DrawOneCard(int num)
        {
            if (PlayerHandSize(num) <= PlayerCardCount(num)) { return null; }
            if (commonDeck.Count <= 0)
            {
                if (discardDeck.Count <= 0)
                {
                    commonDeck = LoadData.GenStack(); // Deck reload!
                }
                else
                {
                    discardDeck = Mix(discardDeck);

                    foreach (Card item in discardDeck)
                    {
                        commonDeck.Push(item);
                    }
                    discardDeck.Clear();
                    nbDiscardDeck = 0;
                }
            }
            Card c = commonDeck.Pop();
            cards[players[num]].Add(c);

            return c;
        }

        public List<Card> CardsForPlayer(int playerNum)
        {
            return cards[players[playerNum]];
        }

        public List<Card> BuildingsForPlayer(int playerNum)
        {
            return buildings[players[playerNum]];
        }

        public Stack<Card> Mix(Stack<Card> discardDeck)
        {
            List<Card> list = new List<Card>();
            foreach (Card card in discardDeck)
            {
                list.Add(card);
            }

            Random random = new Random();
            for (int i = discardDeck.Count - 1; i > 0; i--)
            {
                int r = random.Next(i + 1);
                (list[i], list[r]) = (list[r], list[i]);
            }
            Stack<Card> stack = new Stack<Card>();
            foreach (Card card in list)
            {
                stack.Push(card);
            }
            return stack;
        }


        public void DrawACard()
        {
            List<Card> commonDeckList = new List<Card>();
            foreach (Card card in commonDeck)
            {
                commonDeckList.Add(card);
            }
            commonDeck.Clear();

            int i = 0;
            bool found = false;

            while (i < commonDeckList.Count && !found)
            {
                if (commonDeckList[i].CardType.Name.Equals(drawEffectCard.Name))
                {
                    cards[players[CurrentPlayer]].Add(commonDeckList[i]);
                    commonDeckList.Remove(commonDeckList[i]);
                    found = true;
                }
                i++;
            }
            foreach (Card card in commonDeckList)
            {
                commonDeck.Push(card);
            }

        }
        public void DrawACardFromDiscard()
        {
            List<Card> discardDeckList = new List<Card>();
            foreach (Card card in discardDeck)
            {
                discardDeckList.Add(card);
            }
            discardDeck.Clear();

            int i = 0;
            bool found = false;

            while (i < discardDeckList.Count && !found)
            {
                if (discardDeckList[i].CardType.Name.Equals(drawDiscardEffectCard.Name))
                {
                    cards[players[CurrentPlayer]].Add(discardDeckList[i]);
                    discardDeckList.Remove(discardDeckList[i]);
                    found = true;
                }
                i++;
            }
            foreach (Card card in discardDeckList)
            {
                discardDeck.Push(card);
            }

        }

        /// <summary>
        /// Method called to end the draw phase
        /// </summary>
        public void DrawPhaseEnded()
        {
            GameStatus = GameStatus.Playing;
        }
        /// <summary>
        /// Read-only access to the players' names
        /// </summary>
        /// <param name="num">The number of the player</param>
        /// <returns>The player's name</returns>
        public string PlayerName(int num) => players[num].Name;
        /// <summary>
        /// Read-only access to the players' scores
        /// </summary>
        /// <param name="num">The number of the player</param>
        /// <returns>The player's score</returns>
        public int PlayerScore(int num) => players[num].Points;
        /// <summary>
        /// Read-only access to the players' handsizes
        /// </summary>
        /// <param name="num">The number of the player</param>
        /// <returns>The player's hand's size</returns>
        public int PlayerHandSize(int num) => players[num].HandSize;

        public int UpdatePlayerHandSize(int num, int handSize) => players[num].HandSize = handSize;
        /// <summary>
        /// Read-only access to the players' number of cards
        /// </summary>
        /// <param name="num">The number of the player</param>
        /// <returns>The player's number of cards</returns>
        public int PlayerCardCount(int num) => cards[players[num]].Count;

    }
}
