
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unideckbuildduel.Logic;
using Unideckbuildduel.View;

namespace Unideckbuildduel.View
{
    /// <summary>
    /// A class for the main window for gameplay. One single instance.
    /// </summary>
    public partial class Window : Form
    {
        private readonly List<CardView> cardViews;
        private readonly List<List<BuildingView>> buildingViews;
        private Point playerOneCardStart;
        private Point playerTwoCardStart;
        private Point playerOneBuildingStart;
        private Point playerTwoBuildingStart;
        private Point playerOneBuildingCurrent;
        private Point playerTwoBuildingCurrent;
        private CardView selection;
        public Button btnDeck { get { return deck; } set { } }

        public Button btnDrawFromDiscard { get { return drawFromDiscard; } set { } }

        /// <summary>
        /// A reference to the single instance of this class
        /// </summary>
        public static Window GetWindow { get; } = new Window();
        private Window()
        {
            InitializeComponent();
            cardViews = new List<CardView>();
            buildingViews = new List<List<BuildingView>>
            {
                new List<BuildingView>(),
                new List<BuildingView>()
            };
            ViewSettings.Rightmost = outputListBox.Left;
            InitParam();
        }

        private void InitParam()
        {
            buttonRestart.Enabled = false;
            playerOneCardStart = new Point(10, 10);
            playerTwoCardStart = new Point(10, 500);
            playerOneBuildingStart = new Point(25, 190);
            playerTwoBuildingStart = new Point(25, 370);
            playerOneBuildingCurrent = playerOneBuildingStart;
            playerTwoBuildingCurrent = playerTwoBuildingStart;
        }
        /// <summary>
        /// Method called by the controler whenever some text should be displayed
        /// </summary>
        /// <param name="s"></param>
        public void WriteLine(string s)
        {
            List<string> strs = s.Split('\n').ToList();
            strs.ForEach(str => outputListBox.Items.Add(str));
            if (outputListBox.Items.Count > 0)
            {
                outputListBox.SelectedIndex = outputListBox.Items.Count - 1;
            }
            outputListBox.Refresh();
        }
        /// <summary>
        /// Method called to display a new building
        /// </summary>
        /// <param name="playerNumber">The number of the player</param>
        /// <param name="card">The card to base the building on</param>
        public void AddNewBuilding(int playerNumber, Card card)
        {
            Point point = playerNumber == 0 ? playerOneBuildingCurrent : playerTwoBuildingCurrent;
            Point start = playerNumber == 0 ? playerOneBuildingStart : playerTwoBuildingStart;
            BuildingView building = BuildingView.MakeBuildingOrNull(card.CardType, point);
            BuildingView build = BuildingPresentInBuildings(building, playerNumber);
            if (build == null)
            {
                buildingViews[playerNumber].Add(building);
                point.Offset(ViewSettings.BuildSize.Width, 0);
                point.Offset(ViewSettings.Margin.Width, 0);
                if (point.X + ViewSettings.BuildSize.Width + ViewSettings.Margin.Width >= ViewSettings.Rightmost)
                {
                    point.X = start.X;
                    point.Y = point.Y + ViewSettings.BuildSize.Height + ViewSettings.Margin.Height;
                }

                if (playerNumber == 0)
                {
                    playerOneBuildingCurrent = point;
                }
                else
                {
                    playerTwoBuildingCurrent = point;
                }
            }
            else
            {
                build.NbBuildingsInThisType += 1;
            }


            Refresh();
        }

        public BuildingView BuildingPresentInBuildings(BuildingView building, int playerNumber)
        {
            foreach (BuildingView b in buildingViews[playerNumber])
            {
                if (b.GetCardType().Name.Equals(building.GetCardType().Name))
                {
                    return b;
                }
            }
            return null;
        }
        /// <summary>
        /// Method called whenever the cards should be displayed
        /// </summary>
        /// <param name="playerNumber">The number of the player</param>
        /// <param name="cards">The cards to display for the player</param>
        public void CardsForPlayer(int playerNumber, List<Card> cards)
        {
            cardViews.Clear();
            Point point = playerNumber == 0 ? playerOneCardStart : playerTwoCardStart;
            int i = 0;
            foreach (Card c in cards)
            {
                CardView cardView = new CardView(c, point, i++);
                cardViews.Add(cardView);
                point.Offset(ViewSettings.CardSize.Width, 0);
                point.Offset(ViewSettings.Margin.Width, 0);
            }

            foreach (Effect effect in Game.GetGame.effectsAvailables)
            {
                if (!effect.Equals(Effect.noEffect))
                {
                    effectListBox.Items.Add(effect.ToString());
                }
            }
            Refresh();
        }
        #region Event handling
        private void Window_Paint(object sender, PaintEventArgs e)
        {
            foreach (CardView cv in cardViews) { cv.Draw(e.Graphics); }
            foreach (List<BuildingView> lbv in buildingViews)
            {
                foreach (BuildingView bv in lbv) { bv.Draw(e.Graphics); }
            }
            playerOneScoreLabel.Text = Controller.GetControler.PlayerOneScore;
            playerTwoScoreLabel.Text = Controller.GetControler.PlayerTwoScore;
            if (Controller.GetControler.NumbersOfTurnsToGo == Game.GetGame.Turn)
            {
                turnLabel.Text = "Turn# " + Game.GetGame.Turn;
            }
            else
            {
                turnLabel.Text = Controller.GetControler.NumberOfTurns;
            }
            commonDeckLenght.Text = Game.GetGame.nbCommonDeck.ToString();
            discardDeckLenght.Text = Game.GetGame.nbDiscardDeck.ToString();
        }

        private void NextTurnButton_Click(object sender, EventArgs e)
        {
            effectListBox.Items.Clear();

            if (Game.GetGame.canPlayAgain)
            {
                Controller.GetControler.DrawStart(Game.GetGame.CurrentPlayer);
                Game.GetGame.RestartPlaying();
                Game.GetGame.Play();
                Game.GetGame.canPlayAgain = false;
            }
            else
            {
                Controller.GetControler.EndTurn();
                if (Game.GetGame.currentPlayerCanDraw)
                {
                    deck.Enabled = true;
                }
                else
                {
                    deck.Enabled = false;
                }
                if (Game.GetGame.currentPlayerCanDrawFromDiscard)
                {
                    drawFromDiscard.Enabled = true;
                }
                else
                {
                    drawFromDiscard.Enabled = false;
                }
            }

            Game.GetGame.substitutes.Clear();
            Game.GetGame.effectsAvailables.Clear();
        }

        private void PlaceAllButton_Click(object sender, EventArgs e)
        {
            Controller.GetControler.PlaceAllCards();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        public void DisableButton()
        {
            buttonRestart.Enabled = true;
            nextTurnButton.Enabled = false;
            placeAllButton.Enabled = false;
            Refresh();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            Controller.GetControler.StartEverything();
            foreach (var item in buildingViews)
            {
                item.Clear();
            }
            buttonRestart.Enabled = false;
            nextTurnButton.Enabled = true;
            placeAllButton.Enabled = true;
            InitParam();
            Refresh();
        }

        private CardView Selection(Point p)
        {
            CardView card = null;
            int i = 0;
            while (card == null && i < cardViews.Count)
            {
                if (cardViews[i].Contient(p))
                {
                    card = cardViews[i];
                }
                i++;
            }
            return card;
        }
        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            selection = Selection(e.Location);
            if (selection != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (Game.GetGame.GameStatus == GameStatus.Discarding)
                        {
                            Game.GetGame.DiscardCard(Game.GetGame.CurrentPlayer, selection.CardNum);
                            Controller.GetControler.DisplayHand(Game.GetGame.CurrentPlayer, Game.GetGame.CardsForPlayer(Game.GetGame.CurrentPlayer));
                        }
                        else
                        {
                            Controller.GetControler.PlayCard(Game.GetGame.CurrentPlayer, selection.CardNum);
                            discardDeckLenght.Text = Game.GetGame.nbDiscardDeck.ToString();

                        }
                        break;
                    case MouseButtons.Right:
                        if (Game.GetGame.effectCard != null)
                        {
                            Card card = Game.GetGame.CardsForPlayer(Game.GetGame.CurrentPlayer)[selection.CardNum];

                            if (card.CardType.Name.Equals(Game.GetGame.effectCard.Name))
                            {
                                if (Game.GetGame.GameStatus == GameStatus.Exchanging)
                                {
                                    if (Game.GetGame.NeedValidation())
                                    {
                                        Game.GetGame.DiscardCard(Game.GetGame.CurrentPlayer, selection.CardNum);
                                        Game.GetGame.DrawOneCard(Game.GetGame.CurrentPlayer);
                                        Controller.GetControler.DisplayHand(Game.GetGame.CurrentPlayer, Game.GetGame.CardsForPlayer(Game.GetGame.CurrentPlayer));
                                        discardDeckLenght.Text = Game.GetGame.nbDiscardDeck.ToString();
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            Refresh();
        }

        private void discardPhase_Click(object sender, EventArgs e)
        {
            Game.GetGame.StartDiscardPhase();
            Game.GetGame.Play();
        }

        private void deck_Click(object sender, EventArgs e)
        {
            if (Game.GetGame.PlayerCardCount(Game.GetGame.CurrentPlayer) >= Game.GetGame.PlayerHandSize(Game.GetGame.CurrentPlayer))
            {

                btnDeck.Enabled = false;
            }
            else
            {
                Game.GetGame.DrawACard();
                Controller.GetControler.DisplayHand(Game.GetGame.CurrentPlayer, Game.GetGame.CardsForPlayer(Game.GetGame.CurrentPlayer));
                commonDeckLenght.Text = Game.GetGame.nbCommonDeck.ToString();
                btnDeck.Enabled = false;
            }

        }

        private void drawFromDiscard_Click(object sender, EventArgs e)
        {
            if (Game.GetGame.PlayerCardCount(Game.GetGame.CurrentPlayer) >= Game.GetGame.PlayerHandSize(Game.GetGame.CurrentPlayer))
            {

                btnDrawFromDiscard.Enabled = false;
            }
            else
            {
                Game.GetGame.DrawACardFromDiscard();
                Controller.GetControler.DisplayHand(Game.GetGame.CurrentPlayer, Game.GetGame.CardsForPlayer(Game.GetGame.CurrentPlayer));
                discardDeckLenght.Text = Game.GetGame.nbDiscardDeck.ToString();
                btnDrawFromDiscard.Enabled = false;
            }
        }
    }
}
