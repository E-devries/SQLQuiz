///
/// FILE : DatabaseConnector.cs
/// PROJECT : SQLQuiz
/// PROGRAMMER : Elizabeth deVries
/// FIRST VERSION : 2021-12-09
/// DESCRIPTION   : This class interacts with the mysql to insert, update, or select. 
///               : It hides the database logic from the rest of the program


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLQuiz
{

    /// <summary>
    /// CLASS NAME  : DatabaseConnector
    /// DESCRIPTION : This class is responsible for signing in to the database. It collects its login information from app.config, 
    ///               and is capable of inserting, updating, or selecting information from the rdA4Data database.
    /// </summary>
    public class DatabaseConnector
    {
        // Data Members //
        private MySqlConnection connection;


        /// <summary>
        /// METHOD NAME : DatabaseConnector
        /// DESCRIPTION : This is a constructor that sets up a connection string for a database connection using information from app.config
        /// </summary>
        public DatabaseConnector()
        {
            string hostServer = ConfigurationManager.AppSettings.Get("DatabaseServer");
            string databaseName = ConfigurationManager.AppSettings.Get("DatabaseName");
            string uid = ConfigurationManager.AppSettings.Get("DatabaseUID");
            string password = ConfigurationManager.AppSettings.Get("DatabasePW");

            string connectionString = "SERVER=" + hostServer + ";DATABASE=" + databaseName + ";UID=" + uid + ";PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            
        }



        /// <summary>
        /// METHOD NAME : GetQuestion
        /// DESCRIPTION : This method queries the database for the question of the given id
        /// </summary>
        /// <param name="id">the integer id of the question to select</param>
        /// <returns> the requested question</returns>
        public Question GetQuestion(int id)
        {
            Question question = null;

            try
            {
                connection.Open();

                // get the data
                string selectString = "SELECT * FROM `Question` WHERE `questionID` = " + id + ";";
                MySqlCommand selectQuestion = new MySqlCommand(selectString, connection);
                MySqlDataReader questionReader = selectQuestion.ExecuteReader();

                // instantiate the question with the data
                questionReader.Read();
                question = new Question(questionReader.GetInt32("questionID"), questionReader.GetString("questionText"));
                questionReader.Close();


                connection.Close();
            }
            // if exception is encountered, assign an error Question and return
            catch (Exception)
            {
                question = new Question(0, Question.QUESTION_ERROR);
            }

            return question;
        }



        /// <summary>
        /// METHOD NAME : GetAnswers
        /// DESCRIPTION : This method gets the 4 questions associated with the given question id
        /// </summary>
        /// <param name="questionID"> the integer id of the question to get answers from</param>
        /// <returns> an array containing 4 answers where the first answer is the correct one</returns>
        public Answer[] GetAnswers(int questionID)
        {
            Answer[] answers = new Answer[4];

            try
            {
                connection.Open();

                // get the correct answer
                string selectString = "SELECT * FROM `Answer` WHERE `questionID` = " + questionID + " AND `isCorrect` = true;";
                MySqlCommand selectRightAnswer = new MySqlCommand(selectString, connection);
                MySqlDataReader correctAnswerReader = selectRightAnswer.ExecuteReader();

                correctAnswerReader.Read();
                answers[0] = new Answer(correctAnswerReader.GetInt32("answerID"), correctAnswerReader.GetBoolean("isCorrect"), correctAnswerReader.GetString("answerText"));
                correctAnswerReader.Close();


                // get the incorrect answers
                selectString = "SELECT * FROM `Answer` WHERE `questionID` = " + questionID + " AND `isCorrect` = false;";
                MySqlCommand selectAnswers = new MySqlCommand(selectString, connection);
                MySqlDataReader answerReader = selectAnswers.ExecuteReader();

                // fill in, starting at index 1 because we already have the correct answer there
                for (int i = 1; i < Answer.ANSWER_PER_QUESTION; i++)
                {
                    answerReader.Read();

                    answers[i] = new Answer(answerReader.GetInt32("answerID"), answerReader.GetBoolean("isCorrect"), answerReader.GetString("answerText"));
                }
                answerReader.Close();
                connection.Close();
            }
            // if exception, assign the first answer to an error and return
            catch (Exception)
            {
                answers[0] = new Answer(0, false, Answer.ANSWER_ERROR);
            }

            return answers;
        }



        /// <summary>
        /// METHOD NAME : InsertPlayer
        /// DESCRIPTION : This method inserts a player to the database, and then selects them back in order to get the new ID.
        ///             : It then populates a player object with the id, given name, total score, and total time. 
        /// </summary>
        /// <param name="playerName"> the name of the player to insert</param>
        /// <returns>a player object representing the player being inserted</returns>
        public Player InsertPlayer(string playerName)
        {
            Player player = null;

            try
            {
                connection.Open();

                // insert player into database
                string commandString = "INSERT INTO `Player` (`playerName`) VALUES ('" + playerName + "');";
                MySqlCommand playerInsert = new MySqlCommand(commandString, connection);
                playerInsert.ExecuteNonQuery();

                // reselect player to populate player object with id and score/time
                string queryString = "SELECT * FROM `Player` WHERE `playerID` = LAST_INSERT_ID();";
                MySqlCommand playerRetrieve = new MySqlCommand(queryString, connection);
                MySqlDataReader playerReader = playerRetrieve.ExecuteReader();

                // populate object and close reader
                playerReader.Read();
                player = new Player(playerReader.GetInt32("playerID"), playerReader.GetString("playerName"), playerReader.GetInt32("totalScore"), playerReader.GetInt32("totalGameTime"));
                playerReader.Close();

                connection.Close();
            }
            // assign an error player if exception is found
            catch(MySqlException)
            {
                player = new Player(0, Player.PLAYER_ERROR, 0,0);
            }

            return player;
        }



        /// <summary>
        /// METHOD NAME : UpdatePlayer
        /// DESCRIPTION : This method takes a player and updates that entry in the database.
        ///             : It then inserts an associative entity for the current question
        /// </summary>
        /// <param name="player">the player to update</param>
        /// <param name="question"> the integer id of the question</param>
        /// <param name="questionScore"> the score for the specific question</param>
        /// <param name="questionTime"> the time in ms for the specific quetion</param>
        /// <returns>true if successful, false if not</returns>
        public bool UpdatePlayer(Player player, int question, int questionScore, int questionTime)
        {
            bool success = true;

            try
            {
                connection.Open();

                // perform update on player
                string commandString = "UPDATE `Player` SET `totalScore` = " + player.TotalScore + ", `totalGameTime` = " + player.TotalTime + " WHERE `playerID` = " + player.PlayerID + ";";
                MySqlCommand updatePlayer = new MySqlCommand(commandString, connection);
                updatePlayer.ExecuteNonQuery();

                // insert questionscore associative entity
                commandString = "INSERT INTO `QuestionScore` (`playerID`, `questionID`, `answerTime`, `score`) VALUES (";
                commandString += player.PlayerID + ", " + question + ", " + questionTime + ", " + questionScore + ");";
                MySqlCommand insertQuestionScore = new MySqlCommand(commandString, connection);
                insertQuestionScore.ExecuteNonQuery();

                connection.Close();
            }
            catch(MySqlException)
            {
                success = false;
            }

            return success;
        }



        /// <summary>
        /// METHOD NAME : GetQuestionScores
        /// DESCRIPTION : This method gets all of the question scores belonging to the given player and returns them
        ///               as an array of up to 10 strings.
        /// </summary>
        /// <param name="playerID"></param>
        /// <returns></returns>
        public QuestionScore[] GetQuestionScores(int playerID)
        {
            QuestionScore[] questionScores = new QuestionScore[QuestionScore.MAX_SCORES];

            try
            {
                connection.Open();
                string qScoreQuery = "SELECT Q.`questionID`, Q.`answerTime`, Q.`score`, A.`answerText` FROM `QuestionScore` Q, `Answer` A WHERE Q.playerID = " + playerID + " AND A.isCorrect = true AND A.questionID = Q.questionID;";
                MySqlCommand getQScores = new MySqlCommand(qScoreQuery, connection);
                MySqlDataReader qScoreReader = getQScores.ExecuteReader();

                for(int i = 0; i < QuestionScore.MAX_SCORES; i++)
                {
                    qScoreReader.Read();

                    questionScores[i] = new QuestionScore(qScoreReader.GetInt32("questionID"), qScoreReader.GetInt32("answerTime"), qScoreReader.GetInt32("score"), qScoreReader.GetString("answerText"));
                }
                qScoreReader.Close();
                connection.Close();
            }
            // if an exception is found, return an error in the first element
            catch(MySqlException)
            {
                questionScores[0] = new QuestionScore(0,0,0, QuestionScore.QSCORE_ERROR);
            }

            return questionScores;
        }



        /// <summary>
        /// METHOD NAME : GetAllPlayers
        /// DESCRIPTION : This method selects all players from the database, and returns them as a list of Players.
        /// </summary>
        /// <returns></returns>
        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();

            try
            {
                connection.Open();
                
                // get all players
                string selectQuery = "SELECT * FROM `Player`;";
                MySqlCommand getPlayers = new MySqlCommand(selectQuery, connection);
                MySqlDataReader playerReader = getPlayers.ExecuteReader();

                // read until no more players are left
                while(playerReader.Read() == true)
                {
                    players.Add(new Player(playerReader.GetInt32("playerID"), playerReader.GetString("playerName"), playerReader.GetInt32("totalScore"), playerReader.GetInt32("totalGameTime")));
                }
                playerReader.Close();
                connection.Close();

                players = players.OrderByDescending(x => x.TotalScore).ToList();
            }
            // if an exception is encountered, return the list with an error player in the first spot.
            catch
            {
                players[0] = new Player(0, Player.PLAYER_ERROR, 0, 0);
            }


            return players;
        }
    }
}
