using System.Drawing;
using Unideckbuildduel.Logic;

namespace Unideckbuildduel.View
{
    /// <summary>
    /// A class allowing to display a not-yet-played card.
    /// </summary>
    public class CardView
    {
        private readonly Card card;
        private readonly Color colour;
        private readonly Color colourRect;
        /// <summary>
        /// The location, relative to the window.
        /// </summary>
        public Point Location { get; set; }
        /// <summary>
        /// The number of the card in the player's hand. Read only.
        /// </summary>
        public int CardNum { get; private set; }
        private Rectangle Rect {  get { return new Rectangle(Location, ViewSettings.CardSize); } }

        /// <summary>
        /// Parametered constructor.
        /// </summary>
        /// <param name="card">The card to view</param>
        /// <param name="location">The initial location of the card</param>
        public CardView(Card card, Point location, int cardNum)
        {
            this.card = card;
            Location = location;
            switch (card.CardType.Kind)
            {
                case Kind.Building:
                    colour = Color.RoyalBlue;
                    colourRect = Color.LightBlue;
                    break;
                case Kind.Ressource:
                    colour = Color.ForestGreen;
                    colourRect = Color.LightGreen;
                    break;
                case Kind.Action:
                    colour = Color.Magenta;
                    colourRect = Color.LightPink;
                    break;
                default: colour = Color.Black; break;
            }
            CardNum = cardNum;
        }
        /// <summary>
        /// The draw method.
        /// </summary>
        /// <param name="g">The graphic context to display the building in</param>
        public void Draw(Graphics g)
        {
            SolidBrush blueBrush = new SolidBrush(colourRect);
            g.FillRectangle(blueBrush, Rect);
            g.DrawRectangle(new Pen(colour, ViewSettings.CardWidth), Rect);
            Point baseLine = Location;
            baseLine.Offset(5, 10);
            g.DrawString(card.CardType.Kind.ToString(), ViewSettings.BaseFont, new SolidBrush(colour), baseLine);
            baseLine.Offset(5, 20);
            g.DrawString(card.CardType.Name, ViewSettings.BaseFont, new SolidBrush(colour), baseLine);
            //...
        }

        public bool Contient(Point p)
        {
            return Rect.Contains(p);
        }
    }
}