using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloGUI
{
 
    public class GameManager
    {
        private  bool m_IsComputer;
        private int m_FirstPlayerScore;
        private int m_SecondPlayerScore;
        private int m_FirstPlayerWins;
        private int m_SecondPlayerWins;
        
       
        public bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }
            set
            {
                m_IsComputer = value;
            }
        }

        public int FirstPlayerScore
        {
            get
            {
                return m_FirstPlayerScore;
            }
            set
            {
                m_FirstPlayerScore = value;
            }
        }

        public int SecondPlayerScore
        {
            get
            {
                return m_SecondPlayerScore;
            }
            set
            {
                m_SecondPlayerScore = value;
            }
        }

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
    }
}
