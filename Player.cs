///
/// FILE : Player.cs
/// PROJECT : SqlQuiz
/// PROGRAMMER : Elizabeth deVries
/// FIRST VERSION : 2021-12-09
/// DESCRIPTION   : This file contains the player class, which acts as a simple structure to hold information about a player


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQuiz
{

    /// <summary>
    /// CLASS NAME  : Player
    /// DESCRIPTION : This class holds information about a player to be inserted in the database. It contains the player's id, 
    ///               player name, total game score, and total game time
    /// </summary>
    public class Player
    {
        public const string PLAYER_ERROR = "INVALID_PLAYER";

        private int playerID;
        public int PlayerID { get { return playerID; } }

        private string playerName;
        public string PlayerName { get { return playerName; } }

        private int totalScore;
        public int TotalScore { get { return totalScore; } set { totalScore = value; } }

        private int totalTime;  // time in milliseconds
        public int TotalTime { get { return totalTime; } set { totalTime = value; } }



        /// <summary>
        /// METHOD NAME : Player
        /// DESCRIPTION : This is a constructor that sets the attributes of the player based on the database information
        /// </summary>
        /// <param name="name"> player name</param>
        /// <param name="id"> the id of the player</param>
        /// <param name="score"> the total score of the player</param>
        /// <param name="time"> the total game time in ms</param>
        public Player(int id, string name, int score, int time)
        {
            playerID = id;
            playerName = name;
            totalScore = score;
            totalTime = time;
        }
    }
}
