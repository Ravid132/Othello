using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace OthelloGUI
{
    public partial class FormGameSettings : Form
    {
        private int m_BoardSize;
        private bool m_IsComputer;
        private const int MAX_SIZE = 12;
        private const int MIN_SIZE = 6;
        private const int ADD_SIZE = 2;
        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        public bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }
        }

        public FormGameSettings()
        {
            m_BoardSize = 6;
            InitializeComponent();
        }

        
        private void GameSettings_Load(object sender, EventArgs e)
        {
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        //board size button
        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            if (m_BoardSize == MAX_SIZE)
            {
                m_BoardSize = MIN_SIZE;
            }
            else
            {
                m_BoardSize += ADD_SIZE;
            }

            this.ButtonBoardSize.Text = string.Format("BoardSize: {0}x{0} (click to increase)", m_BoardSize);
        }

        //player against computer
        private void buttonPlayAgainstComputer_Click(object sender, EventArgs e)
        {
            m_IsComputer = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //play against friend
        private void buttonPlayAgainstFriend_Click(object sender, EventArgs e)///?
        {
            m_IsComputer = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
