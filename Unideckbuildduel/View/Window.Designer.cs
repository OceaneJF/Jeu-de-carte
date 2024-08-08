namespace Unideckbuildduel.View
{
    partial class Window
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.quitButton = new System.Windows.Forms.Button();
            this.outputListBox = new System.Windows.Forms.ListBox();
            this.placeAllButton = new System.Windows.Forms.Button();
            this.nextTurnButton = new System.Windows.Forms.Button();
            this.turnLabel = new System.Windows.Forms.Label();
            this.playerTwoScoreLabel = new System.Windows.Forms.Label();
            this.playerOneScoreLabel = new System.Windows.Forms.Label();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.commonDeckLenght = new System.Windows.Forms.Label();
            this.discardDeckLenght = new System.Windows.Forms.Label();
            this.discardPhase = new System.Windows.Forms.Button();
            this.deck = new System.Windows.Forms.Button();
            this.drawFromDiscard = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.effectListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // quitButton
            // 
            this.quitButton.BackColor = System.Drawing.Color.Red;
            this.quitButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.quitButton.FlatAppearance.BorderSize = 2;
            this.quitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitButton.Location = new System.Drawing.Point(1317, 567);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(78, 32);
            this.quitButton.TabIndex = 0;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = false;
            this.quitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // outputListBox
            // 
            this.outputListBox.FormattingEnabled = true;
            this.outputListBox.Location = new System.Drawing.Point(1213, 73);
            this.outputListBox.Name = "outputListBox";
            this.outputListBox.Size = new System.Drawing.Size(205, 121);
            this.outputListBox.TabIndex = 1;
            // 
            // placeAllButton
            // 
            this.placeAllButton.Location = new System.Drawing.Point(1317, 538);
            this.placeAllButton.Name = "placeAllButton";
            this.placeAllButton.Size = new System.Drawing.Size(75, 23);
            this.placeAllButton.TabIndex = 2;
            this.placeAllButton.Text = "Play All";
            this.placeAllButton.UseVisualStyleBackColor = true;
            this.placeAllButton.Visible = false;
            this.placeAllButton.Click += new System.EventHandler(this.PlaceAllButton_Click);
            // 
            // nextTurnButton
            // 
            this.nextTurnButton.BackColor = System.Drawing.Color.SkyBlue;
            this.nextTurnButton.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.nextTurnButton.FlatAppearance.BorderSize = 2;
            this.nextTurnButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextTurnButton.Location = new System.Drawing.Point(1318, 500);
            this.nextTurnButton.Name = "nextTurnButton";
            this.nextTurnButton.Size = new System.Drawing.Size(77, 32);
            this.nextTurnButton.TabIndex = 3;
            this.nextTurnButton.Text = "Next Turn";
            this.nextTurnButton.UseVisualStyleBackColor = false;
            this.nextTurnButton.Click += new System.EventHandler(this.NextTurnButton_Click);
            // 
            // turnLabel
            // 
            this.turnLabel.AutoSize = true;
            this.turnLabel.Location = new System.Drawing.Point(1210, 567);
            this.turnLabel.Name = "turnLabel";
            this.turnLabel.Size = new System.Drawing.Size(35, 13);
            this.turnLabel.TabIndex = 4;
            this.turnLabel.Text = "label1";
            // 
            // playerTwoScoreLabel
            // 
            this.playerTwoScoreLabel.AutoSize = true;
            this.playerTwoScoreLabel.Location = new System.Drawing.Point(1210, 534);
            this.playerTwoScoreLabel.Name = "playerTwoScoreLabel";
            this.playerTwoScoreLabel.Size = new System.Drawing.Size(35, 13);
            this.playerTwoScoreLabel.TabIndex = 5;
            this.playerTwoScoreLabel.Text = "label1";
            // 
            // playerOneScoreLabel
            // 
            this.playerOneScoreLabel.AutoSize = true;
            this.playerOneScoreLabel.Location = new System.Drawing.Point(1210, 510);
            this.playerOneScoreLabel.Name = "playerOneScoreLabel";
            this.playerOneScoreLabel.Size = new System.Drawing.Size(35, 13);
            this.playerOneScoreLabel.TabIndex = 6;
            this.playerOneScoreLabel.Text = "label1";
            // 
            // buttonRestart
            // 
            this.buttonRestart.BackColor = System.Drawing.Color.SkyBlue;
            this.buttonRestart.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.buttonRestart.FlatAppearance.BorderSize = 2;
            this.buttonRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRestart.ForeColor = System.Drawing.Color.Black;
            this.buttonRestart.Location = new System.Drawing.Point(1318, 462);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(77, 32);
            this.buttonRestart.TabIndex = 7;
            this.buttonRestart.Text = "Restart";
            this.buttonRestart.UseVisualStyleBackColor = false;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1210, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Taille de la pioche :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1210, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Taille de la défausse :";
            // 
            // commonDeckLenght
            // 
            this.commonDeckLenght.AutoSize = true;
            this.commonDeckLenght.Location = new System.Drawing.Point(1315, 15);
            this.commonDeckLenght.Name = "commonDeckLenght";
            this.commonDeckLenght.Size = new System.Drawing.Size(25, 13);
            this.commonDeckLenght.TabIndex = 10;
            this.commonDeckLenght.Text = "      ";
            // 
            // discardDeckLenght
            // 
            this.discardDeckLenght.AutoSize = true;
            this.discardDeckLenght.Location = new System.Drawing.Point(1327, 44);
            this.discardDeckLenght.Name = "discardDeckLenght";
            this.discardDeckLenght.Size = new System.Drawing.Size(37, 13);
            this.discardDeckLenght.TabIndex = 11;
            this.discardDeckLenght.Text = "          ";
            // 
            // discardPhase
            // 
            this.discardPhase.BackColor = System.Drawing.Color.LimeGreen;
            this.discardPhase.FlatAppearance.BorderColor = System.Drawing.Color.DarkGreen;
            this.discardPhase.FlatAppearance.BorderSize = 2;
            this.discardPhase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.discardPhase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.discardPhase.ForeColor = System.Drawing.Color.Black;
            this.discardPhase.Location = new System.Drawing.Point(1231, 349);
            this.discardPhase.Name = "discardPhase";
            this.discardPhase.Size = new System.Drawing.Size(77, 42);
            this.discardPhase.TabIndex = 12;
            this.discardPhase.Text = "discard";
            this.discardPhase.UseVisualStyleBackColor = false;
            this.discardPhase.Click += new System.EventHandler(this.discardPhase_Click);
            // 
            // deck
            // 
            this.deck.BackColor = System.Drawing.Color.LimeGreen;
            this.deck.Enabled = false;
            this.deck.FlatAppearance.BorderColor = System.Drawing.Color.DarkGreen;
            this.deck.FlatAppearance.BorderSize = 2;
            this.deck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deck.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deck.ForeColor = System.Drawing.Color.Black;
            this.deck.Location = new System.Drawing.Point(1318, 349);
            this.deck.Name = "deck";
            this.deck.Size = new System.Drawing.Size(77, 43);
            this.deck.TabIndex = 14;
            this.deck.Text = "Deck";
            this.deck.UseVisualStyleBackColor = false;
            this.deck.Click += new System.EventHandler(this.deck_Click);
            // 
            // drawFromDiscard
            // 
            this.drawFromDiscard.BackColor = System.Drawing.Color.LimeGreen;
            this.drawFromDiscard.Enabled = false;
            this.drawFromDiscard.FlatAppearance.BorderColor = System.Drawing.Color.DarkGreen;
            this.drawFromDiscard.FlatAppearance.BorderSize = 2;
            this.drawFromDiscard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drawFromDiscard.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawFromDiscard.ForeColor = System.Drawing.Color.Black;
            this.drawFromDiscard.Location = new System.Drawing.Point(1318, 398);
            this.drawFromDiscard.Name = "drawFromDiscard";
            this.drawFromDiscard.Size = new System.Drawing.Size(77, 43);
            this.drawFromDiscard.TabIndex = 15;
            this.drawFromDiscard.Text = "Draw from discard";
            this.drawFromDiscard.UseVisualStyleBackColor = false;
            this.drawFromDiscard.Click += new System.EventHandler(this.drawFromDiscard_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1210, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Effet disponible a ce tour :";
            // 
            // effectListBox
            // 
            this.effectListBox.FormattingEnabled = true;
            this.effectListBox.Location = new System.Drawing.Point(1213, 223);
            this.effectListBox.Name = "effectListBox";
            this.effectListBox.Size = new System.Drawing.Size(205, 108);
            this.effectListBox.TabIndex = 17;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1430, 617);
            this.Controls.Add(this.effectListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.drawFromDiscard);
            this.Controls.Add(this.deck);
            this.Controls.Add(this.discardPhase);
            this.Controls.Add(this.discardDeckLenght);
            this.Controls.Add(this.commonDeckLenght);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.playerOneScoreLabel);
            this.Controls.Add(this.playerTwoScoreLabel);
            this.Controls.Add(this.turnLabel);
            this.Controls.Add(this.nextTurnButton);
            this.Controls.Add(this.placeAllButton);
            this.Controls.Add(this.outputListBox);
            this.Controls.Add(this.quitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Window";
            this.Text = "Insert title here";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Window_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Window_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.ListBox outputListBox;
        private System.Windows.Forms.Button placeAllButton;
        private System.Windows.Forms.Button nextTurnButton;
        private System.Windows.Forms.Label turnLabel;
        private System.Windows.Forms.Label playerTwoScoreLabel;
        private System.Windows.Forms.Label playerOneScoreLabel;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label commonDeckLenght;
        private System.Windows.Forms.Label discardDeckLenght;
        private System.Windows.Forms.Button discardPhase;
        private System.Windows.Forms.Button deck;
        private System.Windows.Forms.Button drawFromDiscard;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox effectListBox;
    }
}

