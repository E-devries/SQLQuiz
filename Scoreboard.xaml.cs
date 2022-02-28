///
/// FILE : Scoreboard.xaml.cs
/// PROJECT : RDA4
/// PROGRAMMER : Elizabeth deVries
/// FIRST VERSION : 2021-12-09
/// DESCRIPTION   : This page is the last page of the rda4 quiz game. It shows the current player's score and total gametime,
///               : As well as a leaderboard showing all other player's scores. It also has a 'play again' button. 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLQuiz
{
    /// <summary>
    /// Interaction logic for Scoreboard.xaml
    /// </summary>
    public partial class Scoreboard : Page
    {
        private Player lastPlayer;

        public Scoreboard(Player currentPlayer)
        {
            InitializeComponent();

            lastPlayer = currentPlayer;
        }

        
        /// <summary>
        /// METHOD NAME : ScorePage_Loaded
        /// DESCRIPTION : This event is launched when the score page is loaded. It fills the page with the player's score information,
        ///             : and displays a scoreboard of other players, organized by total score
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="e"> the event arguments</param>
        private void ScorePage_Loaded(object sender, RoutedEventArgs e)
        {
            pageHeader.Text = "Congratulations, " + lastPlayer.PlayerName + "! Your total score is: " + lastPlayer.TotalScore;

            // create data connector object
            DatabaseConnector finalScores = new DatabaseConnector();


            LeaderBoard.DataContext = finalScores.GetAllPlayers().ToArray();
            ScoreBreakdown.DataContext = finalScores.GetQuestionScores(lastPlayer.PlayerID);
        }
    }
}
