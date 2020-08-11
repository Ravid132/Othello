using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static OthelloGUI.Tile;

namespace OthelloGUI
{
    public class Board
    {
        private int m_BoardSize;
        private eTile[,] m_BoardMat;
        private List<Tiles> m_LegalMoves = new List<Tiles>();
        private bool m_EndGame;
        private int m_Turn;
        private GameManager m_Game;
        private bool m_FirstPlayerMoves;
        private bool m_SecondPlayerMoves;
        private int m_FirstPlayerScore;
        private int m_SecondPlayerScore;
        private int m_FirstPlayerWins;
        private int m_SecondPlayerWins;

        public int FirstPlayerWins
        {
            get
            {
                return m_FirstPlayerWins;
            }
            set
            {
                m_FirstPlayerWins = value;
            }
        }
        
        public int SecondPlayerWins
        {
            get
            {
                return m_SecondPlayerWins;
            }
            set
            {
                m_SecondPlayerWins = value;
            }
        }

        public int FirstPlayerScore
        {
            get
            {
                return m_FirstPlayerScore;
            }
        }

        public int SecondPlayerScore
        {
            get
            {
                return m_SecondPlayerScore;
            }
        }

        public bool FirstPlayerMoves
        {
            get
            {
                return m_FirstPlayerMoves;
            }
        }

        public bool SecondPlayerMoves
        {
            get
            {
                return m_SecondPlayerMoves;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
            set
            {
                m_BoardSize = value;
            }
        }

        public eTile[,] BoardMat
        {
            get
            {
                return m_BoardMat;
            }
        }

        public List<Tiles> LegalMoves
        {
            get
            {
                return m_LegalMoves;
            }
        }

        public bool EndGame
        {
            get
            {
                return m_EndGame;
            }
            set
            {
                m_EndGame = value;
            }
        }

        public int Turn
        {
            get
            {
                return m_Turn;
            }
        }

        public Board(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_BoardMat = new eTile[m_BoardSize, m_BoardSize];

            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_BoardMat[i, j] = eTile.Empty;
                }
            }

            m_BoardMat[(m_BoardSize / 2) - 1, (m_BoardSize / 2) - 1] = eTile.Player2;
            m_BoardMat[(m_BoardSize / 2) - 1, (m_BoardSize / 2)] = eTile.Player1;
            m_BoardMat[(m_BoardSize / 2), (m_BoardSize / 2) - 1] = eTile.Player1;
            m_BoardMat[(m_BoardSize / 2), (m_BoardSize / 2)] = eTile.Player2;


            m_FirstPlayerScore = 0;
            m_SecondPlayerScore = 0;
            
            m_Turn = 0;
            m_Game = new GameManager();
        }

        public struct Tiles
        {
            public int Letter;
            public int Number;

            public Tiles(int x, int y)
            {
                Letter = y;
                Number = x;
            }
            public int Letters
            {
                get { return Letter; }
                set { Letter = value; }
            }
            public int Numbers
            {
                get { return Number; }
                set { Number = value; }
            }

        }

        //checks if there are available moves
        public bool CheckAvailableMoves()
        {
            bool isAvailable = true;
            
            if(m_LegalMoves.Count == 0)
            {
                isAvailable = false;
            }

            return isAvailable;
        }

        //pc makes a move
        public void PCTurn()
        {
            Thread.Sleep(1000);
            Tiles Move;
            Random rnd = new Random();
            Move = LegalMoves[rnd.Next(LegalMoves.Count)];
            m_BoardMat[Move.Number, Move.Letter] = eTile.Player2;
            SwitchTurn();
        }

        //switch turn to next player
        public void SwitchTurn()
        {
            m_Turn = (++m_Turn) % 2;
            AvailableMoves((eTile)m_Turn);
        }

        //player's turn
        public void TakeTurn(int i_Row,int i_Col)
        {
            
            m_BoardMat[i_Row, i_Col] = (eTile)(m_Turn%2);
            FlipTile(i_Row,i_Col,(eTile)m_Turn);
            m_FirstPlayerMoves = true;
            m_SecondPlayerMoves = true;
            SwitchTurn();//netx player's turn
        }
        
        //check if there's no more available moves
        public void NoMoves()
        {
            m_EndGame = true;

            for (int i = 0; i < m_BoardSize; i++)
            {
                for(int j = 0; j < m_BoardSize; j++)
                {
                    if(m_BoardMat[i, j] == eTile.Empty)
                    {
                        m_EndGame = false;
                        break;
                    }
                }
            }

            if(m_EndGame)
            {
                if ((m_Turn % 2) == 0)
                {
                    m_FirstPlayerMoves = false;
                }
                else
                {
                    m_SecondPlayerMoves = false;
                }
                
                if(!m_SecondPlayerMoves && !m_FirstPlayerMoves)
                {
                    m_EndGame = true;
                }
                        
            }
        }

        //flipping tiles
        public bool FlipTile(int i_Row, int i_Col, eTile PlayerTurn)
        {
            Tile OppositeColor = new Tile();
            bool LegalMove = false;

            if (m_LegalMoves.Contains(new Tiles(i_Row, i_Col)))
                LegalMove = true;

            int row = i_Row;
            int col = i_Col;
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                  
                    row = row + x;
                    col = col + y;
                    if (!legalPath(row,col, PlayerTurn, x, y))
                    {
                        row = i_Row;
                        col = i_Col;
                        continue;
                    }
                    while (m_BoardMat[row, col] != PlayerTurn && m_BoardMat[row, col] != eTile.Empty)
                    {
                        m_BoardMat[row, col] = PlayerTurn;
                        row = row + x;
                        col = col + y;
                        if (row <= 0 || m_BoardSize <= row || col <= 0 || m_BoardSize <= col)
                        {
                            break;
                        }

                    }
                    row = i_Row;
                    col = i_Col;
                }
              

            }

            return LegalMove;
        }

        private bool legalPath(int i_Row, int i_Col, eTile PlayerTurn,int x,int y)
        {
            bool isLegalPath = false;
            if (i_Row <= 0 || m_BoardSize <= i_Row || i_Col <= 0 || m_BoardSize <= i_Col)
            {
                return isLegalPath;
            }

            while (m_BoardMat[i_Row,i_Col] !=PlayerTurn && m_BoardMat[i_Row, i_Col] != eTile.Empty)
            {
                i_Row += x;
                i_Col += y;
                if (i_Row <= 0 || m_BoardSize <= i_Row || i_Col <= 0 || m_BoardSize <= i_Col)
                {
                    break;
                }

                if (m_BoardMat[i_Row, i_Col] == PlayerTurn)
                {
                    isLegalPath = true;
                }
            }

      

            return isLegalPath;
        }
        
        //find available moves


        eTile m_PlayerColor;
        public List<Tiles> AvailableMoves(eTile i_PlayerColor)
        {
            int TempCol;
            int TempRow;
            m_LegalMoves.Clear();

            m_PlayerColor = i_PlayerColor;
            
            for (int row = 0; row < m_BoardSize; row++)
            {
                for (int col = 0; col < m_BoardSize; col++)
                {
                    if (m_BoardMat[row, col] == eTile.Empty)
                    {
                        if (findAvailableMoves(row, col))
                        {
                            m_LegalMoves.Add(new Tiles(row, col));
                        }
                    }
                }
            }
            return m_LegalMoves;
        }

        Tile m_OppositeColor = new Tile();

        private bool findAvailableMoves(int i_CurrentX,int i_CurrentY)
        {
            bool isValid = false;
   

            int tempX = i_CurrentX;
            int tempY = i_CurrentY;

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    i_CurrentX = i_CurrentX + x;
                    i_CurrentY = i_CurrentY + y;

                    if (!isOutOfBounds(i_CurrentX, i_CurrentY) && m_BoardMat[i_CurrentX, i_CurrentY] == m_OppositeColor.GetOppositeColor(m_PlayerColor))
                    {
                        while (!isOutOfBounds(i_CurrentX, i_CurrentY) && m_BoardMat[i_CurrentX, i_CurrentY] == m_OppositeColor.GetOppositeColor(m_PlayerColor))
                        {
                            i_CurrentX = i_CurrentX + x;
                            i_CurrentY = i_CurrentY + y;
                        }

                        if (!isOutOfBounds(i_CurrentX, i_CurrentY) && m_BoardMat[i_CurrentX, i_CurrentY] == m_PlayerColor)
                        {
                            isValid = true;
                        }

                    }
                    i_CurrentX = tempX;
                    i_CurrentY = tempY;
                }
            }
            return isValid;
        }

        private bool isOutOfBounds(int i_X, int i_Y)
        {
            bool isOut = false;

            if(i_X < 0 || m_BoardSize <= i_X || i_Y < 0 || m_BoardSize <= i_Y)
            {
                isOut = true;
            }

            return isOut;
        }

        //add win to player
        public void AddWin()
        {
            m_FirstPlayerScore = 0;
            m_SecondPlayerScore = 0;
            for(int i = 0 ; i < m_BoardSize; i++)
            {
                for(int j = 0; j < m_BoardSize; j++)
                {
                    if (BoardMat[i, j] == eTile.Player1)
                    {
                        m_FirstPlayerScore++;
                    }
                    else if (BoardMat[i, j] == eTile.Player2)
                    {
                        m_SecondPlayerScore++;
                    }
                }
            }

            if (m_FirstPlayerScore > m_SecondPlayerScore)
                m_FirstPlayerWins++;
            else
                m_SecondPlayerWins++;
        }
        
        //returns true when first player wins
        public bool FirstPlayerWin()
        {
            return m_FirstPlayerScore > m_SecondPlayerScore;
        }

        //returns true when second player wins
        public bool SecondPlayerWin()
        {
            return m_FirstPlayerScore < m_SecondPlayerScore;
        }

        //returns true when its a tie
        public bool IsTie()
        {
            return m_FirstPlayerScore == m_SecondPlayerScore;
        }
    }
}
    

