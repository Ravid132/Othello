using OthelloGUI;
using Project18.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OthelloGUI
{

    public partial class FormGame : Form
    {
        private static int m_Turn = 0;

        private Board m_Board;
        private GameManager m_game;
        private Image RedCoin;
        private Image YellowCoin;
        private PictureBox[,] m_BoardButtons;

        public FormGame(int i_BoardSize, bool i_IsComputer)
        {
            RedCoin = Resources.CoinRed;
            YellowCoin = Resources.CoinYellow;
            m_game = new GameManager();
            m_game.IsComputer = i_IsComputer;
            m_Board = new Board(i_BoardSize);
            m_Board.BoardSize = i_BoardSize;
            InitializeComponent();

            initializeBoardForm();
            initializeButtons();
            refreshBoard();
        }

        //refreshes board with green tiles/enables buttons and insert coin
        private void refreshBoard()
        {
            List<Board.Tiles> Moves = m_Board.AvailableMoves((Tile.eTile)(m_Turn % 2));
            m_Turn++;

            Board.Tiles CurrentTile = new Board.Tiles();
            for (int i = 0; i < m_Board.BoardSize; i++)
            {
                for (int j = 0; j < m_Board.BoardSize; j++)
                {
                    if (m_Board.BoardMat[i, j] == Tile.eTile.Empty)
                    {
                        m_BoardButtons[i, j].Image = null;
                    }
                    CurrentTile.Letter = j;
                    CurrentTile.Number = i;
                    if (Moves.Contains(CurrentTile))
                    {
                        m_BoardButtons[i, j].Enabled = true;
                        m_BoardButtons[i, j].BackColor = Color.Green;
                    }
                    else
                    {
                        m_BoardButtons[i, j].Enabled = false;
                        m_BoardButtons[i, j].BackColor = Color.Gray;
                        if (m_Board.BoardMat[i, j] != Tile.eTile.Empty)
                        {
                            if (m_Board.BoardMat[i, j] == Tile.eTile.Player1)
                            {
                                m_BoardButtons[i, j].BackgroundImage = RedCoin;
                            }
                            else
                            {
                                m_BoardButtons[i, j].BackgroundImage = YellowCoin;
                            }
                            m_BoardButtons[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }

                }
            }
            string[] BlackOrWhiteSTR = { "Black", "White" };
            if (m_Turn % 2 == 0)
            {
                this.Text = "Othello - " + string.Format("{0}'s Turn", BlackOrWhiteSTR[0]);
            }
            else
            {
                this.Text = "Othello - " + string.Format("{0}'s Turn", BlackOrWhiteSTR[1]);
            }
        }

        private void initializeBoardForm()
        {
            this.Width = (m_Board.BoardSize * 45) + (15 * 4);
            this.Height = (m_Board.BoardSize * 45) + (15 * 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Text = "Othello - ";
        }

        private void initializeButtons()
        {
            m_BoardButtons = new PictureBox[m_Board.BoardSize, m_Board.BoardSize];
            for(int i = 0; i < m_Board.BoardSize; i++)
            {
                for(int j = 0; j < m_Board.BoardSize; j++)
                {
                    m_BoardButtons[i, j] = new PictureBox();
                    m_BoardButtons[i, j].Size = new Size(45, 45);
                    m_BoardButtons[i, j].BorderStyle = BorderStyle.Fixed3D;
                    m_BoardButtons[i, j].Location = new Point((i * 45) + 15, (j * 45) + 15);
                    m_BoardButtons[i, j].Name = string.Format("{0}{1}", i + 1, j + 1);
                    m_BoardButtons[i, j].Click += new EventHandler(button_Click);
                    this.Controls.Add(m_BoardButtons[i, j]);
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            int row = int.Parse((sender as PictureBox).Name);
            int col = row % 10;
            m_Board.TakeTurn(row / 10 - 1, col - 1);
            refreshBoard();
            this.Refresh();

            if(m_Board.CheckAvailableMoves())
            {
                if(m_game.IsComputer)
                {
                    m_Board.PCTurn();
                    refreshBoard();
                    if(!m_Board.CheckAvailableMoves())
                    {
                        while(!m_Board.CheckAvailableMoves())
                        {
                            if(!m_Board.EndGame)
                            {
                                break;
                            }
                            SkipToNextTurn();
                              
                            if (m_Board.CheckAvailableMoves())
                            {
                                m_Board.PCTurn();
                            }
                            
                        }
                    }
                }
            }
            else
            {
                SkipToNextTurn();
                if (!m_Board.CheckAvailableMoves())
                    GameFinished();
            }
        }

    
        //when game is done
        public void GameFinished()
        {
            m_Board.AddWin();
            string PlayersScore;

            if(m_Board.FirstPlayerWin())
            {
                PlayersScore = string.Format("First Player Won!! ({0}/{1})({2}/{3})\n"
                    , m_Board.FirstPlayerScore, m_Board.SecondPlayerScore,
                    m_Board.FirstPlayerWins, m_Board.SecondPlayerWins);
            }
            else if(m_Board.SecondPlayerWin())
            {
                PlayersScore = string.Format("Second Player Won!! ({0}/{1})({2}/{3})\n"
                    , m_Board.SecondPlayerScore, m_Board.FirstPlayerScore,
                    m_Board.SecondPlayerWins, m_Board.FirstPlayerWins);
            }
            else
            {
                PlayersScore = string.Format("Its a Tie!\n");
            }

            DialogResult AnotherRound = MessageBox.Show(string.Format("{0}\nWould you like another round?", PlayersScore)
                , "Othello", MessageBoxButtons.YesNo);
            if (AnotherRound == DialogResult.Yes)
            {
                ClearBoard();
                this.Hide();
                FormGame Play = new FormGame(m_Board.BoardSize, m_game.IsComputer);
                Play.ShowDialog();
                this.Close();
                //Close();

            }
            else
            {
                Close();
            }

        }

        //skips to next player's turn
        public void SkipToNextTurn()
        {
            m_Board.NoMoves();
            string title = "Othello";
            if(!m_Board.EndGame)
            {
                MessageBox.Show("No available moves! Skipping to next player's turn!", title, MessageBoxButtons.OK);
                refreshBoard();
            }
            m_Board.SwitchTurn();
        }

        //clears board
        public void ClearBoard()
        {
            for(int i = 0; i < m_Board.BoardSize; i++)
            {
                for(int j = 0; j < m_Board.BoardSize; j++)
                {
                    m_BoardButtons[i, j].BackgroundImage = null;
                }
            }
            
        }
        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
