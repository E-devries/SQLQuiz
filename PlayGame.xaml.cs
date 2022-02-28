///
/// FILE : PlayGame.xaml.cs
/// PROJECT : RDA4
/// PROGRAMMER : Elizabeth deVries
/// FIRST VERSION : 2021-12-09
/// DESCRIPTION   : This file contains the code behind logic for the RDA4 quiz game's main game page.
///               : It displays questions and answers, tracks the time, the current question, and the user's selection, if any. It then uses that information 
///               : to grade the user, for a total of 10 questions with four answers each.


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
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;

namespace SQLQuiz
{
    /// <summary>
    /// Interaction logic for PlayGame.xaml
    /// </summary>
    public partial class PlayGame : Page
    {

        // Constants //
        const int START_SCORE = 20;
        const int QUESTION_TIME = 20000;   // start time in ms

        const int QUESTION_MAX = 10;
        const int ANSWER_MAX = 4;

        // Data Members //
        volatile bool gameOn;
        int currentQuestionNumber;  // current question id
        int currentMSeconds;        // current time in ms
        int currentScore;           // player score for current question
        int currentCorrectChoice;   // radio button position for current question's correct answer
        Player currentPlayer;



        public PlayGame(Player newPlayer)
        {
            InitializeComponent();

            currentPlayer = newPlayer;
            currentMSeconds = 0;
            currentScore = START_SCORE;
            currentQuestionNumber = 0;
            currentCorrectChoice = 0;
            gameOn = true;
        }



        /// <summary>
        /// METHOD NAME : GamePage_Loaded
        /// DESCRIPTION : This is an event that fires when the game page is loaded. It initializes the screen with the first question
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="e">the event arguments</param>
        private void GamePage_Loaded(object sender, RoutedEventArgs e)
        {
            Thread gameThread = new Thread(new ThreadStart(ManageGame));
            gameThread.Start();

            
        }



        /// <summary>
        /// METHOD NAME : ManageGame
        /// DESCRIPTION : This method runs in a separate thread from ui and manages displaying the questions/answers and running the game timer.
        /// </summary>
        private void ManageGame()
        {
            while(currentQuestionNumber < 10)
            {
                // update page for the first question
                this.Dispatcher.Invoke(UpdateGamePage);

                // create timer for the game
                TimeQuiz();
                
            }

            this.Dispatcher.Invoke(NextPage);
        }



        /// <summary>
        /// METHOD NAME : UpdateGamePage
        /// DESCRIPTION : This method updates the page by incrementing the current question number, and retrieving the question and answers associated
        ///             : with that number for display by querying the database.
        /// </summary>
        /// <returns>true if the current question number is 10 or less, and false if the number becomes greater</returns>
        private void UpdateGamePage()
        {
            // first, update rectangle
            switch(currentQuestionNumber)
            {
                case 1:
                    Q1.Fill = new SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
                    break;

                case 2:
                    Q2.Fill = new SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
                    break;

                case 3:
                    Q3.Fill = new SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
                    break;

                case 4:
                    Q4.Fill = new SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
                    break;

                case 5:
                    Q5.Fill = new SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
                    break;

                case 6:
                    Q6.Fill = new SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
                    break;

                case 7:
                    Q7.Fill = new SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
                    break;

                case 8:
                    Q8.Fill = new SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
                    break;

                case 9:
                    Q9.Fill = new SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
                    break;

                case 10:
                    Q10.Fill = new SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
                    break;
            }

            // then, set the game variables
            gameOn = true;
            currentQuestionNumber++;
            currentMSeconds = 0;
            currentScore = START_SCORE;

            // make sure all the radio buttons are unchecked for the new question
            ChoiceA.IsChecked = false;
            ChoiceB.IsChecked = false;
            ChoiceC.IsChecked = false;
            ChoiceD.IsChecked = false;

            if (currentQuestionNumber <= QUESTION_MAX)
            {
                DatabaseConnector connector = new DatabaseConnector();
                // get question
                Question newQuestion = connector.GetQuestion(currentQuestionNumber);

                // display error message if it didn't work, display question if it did
                if (newQuestion.QuestionText == Question.QUESTION_ERROR)
                {
                    QuestionBlock.Text = "Woops, something went wrong getting the question.";
                }
                else
                {
                    QuestionBlock.Text = newQuestion.QuestionText;
                }

                // get answers
                Answer[] newAnswers = connector.GetAnswers(currentQuestionNumber);

                // check for error
                if (newAnswers[0].AnswerText == Answer.ANSWER_ERROR)
                {
                    string errorAnswerText = "Woops, something went wrong getting the answers.";
                    ChoiceA.Content = errorAnswerText;
                    ChoiceB.Content = errorAnswerText;
                    ChoiceC.Content = errorAnswerText;
                    ChoiceD.Content = errorAnswerText;
                }
                else
                {
                    // randomize correct answer's position 
                    Random random = new Random();
                    currentCorrectChoice = random.Next(1, ANSWER_MAX + 1);

                    // assign correct answer to randomized positon, and wrong answers to the other positions
                    switch (currentCorrectChoice)
                    {
                        // if positon = 1, then correct goes into answerA and rest go in B-D
                        case 1:

                            ChoiceA.Content = newAnswers[0].AnswerText;
                            ChoiceB.Content = newAnswers[1].AnswerText;
                            ChoiceC.Content = newAnswers[2].AnswerText;
                            ChoiceD.Content = newAnswers[3].AnswerText;
                            break;

                        // if position = 2, then correct goes into answerB and rest go in A, C, D
                        case 2:

                            ChoiceB.Content = newAnswers[0].AnswerText;
                            ChoiceA.Content = newAnswers[1].AnswerText;
                            ChoiceC.Content = newAnswers[2].AnswerText;
                            ChoiceD.Content = newAnswers[3].AnswerText;
                            break;

                        // if position = 3, then correct goes into answer C and rest go in A, B, D
                        case 3:

                            ChoiceC.Content = newAnswers[0].AnswerText;
                            ChoiceA.Content = newAnswers[1].AnswerText;
                            ChoiceB.Content = newAnswers[2].AnswerText;
                            ChoiceD.Content = newAnswers[3].AnswerText;
                            break;

                        // if position = 4, then correct goes into answer D and rest go in A-C
                        case 4:

                            ChoiceD.Content = newAnswers[0].AnswerText;
                            ChoiceA.Content = newAnswers[1].AnswerText;
                            ChoiceB.Content = newAnswers[2].AnswerText;
                            ChoiceC.Content = newAnswers[3].AnswerText;
                            break;
                    }
                }
            }
            // if game somehow surpassed question count, show error
            else
            {
                MessageBox.Show("Something happened and the question counter surpassed the number of questions");
            }

            return;
        }



        /// <summary>
        /// METHOD NAME : TimeQuiz
        /// DESCRIPTION : This method is used by a thread to keep track of the time taken to answer the question,
        ///             : and will refresh the quiz when the game needs to stop. It will also refresh the game if 20 seconds has passed.
        /// </summary>
        private void TimeQuiz()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            const int SLEEP_TIME = 10; // the time to wait in ms

            // while the game is on, count down in 10ms increments
            while(gameOn == true)
            {
                // wait 10 ms
                Thread.Sleep(SLEEP_TIME);

                // set new time by converting sleep time increment from ms to seconds
                currentMSeconds = (int)timer.ElapsedMilliseconds;

                // if current seconds reaches 0, stop game and assign 0 points
                if (currentMSeconds >= QUESTION_TIME)
                {
                    currentScore = 0;
                    gameOn = false;
                    timer.Stop();
                    SaveQuestionResults();
                }

            }
        }



        /// <summary>
        /// METHOD NAME : SaveQuestionResults
        /// DESCRIPTION : This method is called when the current round of the quiz is over. 
        ///             : It saves the player's current score and time in the database and updates the progress bar on the game screen
        ///             : based on whether or not the question has been answered.
        /// </summary>
        private void SaveQuestionResults()
        {
            // create database class
            DatabaseConnector saveData = new DatabaseConnector();

            // save current data to player
            currentPlayer.TotalTime += currentMSeconds;
            currentPlayer.TotalScore += currentScore;

            // update player entry
            saveData.UpdatePlayer(currentPlayer, currentQuestionNumber, currentScore, currentMSeconds);

        }



        /// <summary>
        /// METHOD NAME : NextPage
        /// DESCRIPTION : This method moves the navigation to the next page. It is in its own method because it may be called outside of the ui thread,
        ///             : and therefore needs to be invoked.
        /// </summary>
        private void NextPage()
        {
            // after the questions are all done, move to scoreboard
            this.NavigationService.Navigate(new Scoreboard(currentPlayer));
        }



        /// <summary>
        /// METHOD NAME : ChoiceA_Checked
        /// DESCRIPTION : This event is fired when radio button ChoiceA is checked.
        ///               It ends the game, records the current time, and assigns the score based on
        ///               whether or not this was the correct choice.
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="e">the event arguments</param>
        private void ChoiceA_Checked(object sender, RoutedEventArgs e)
        {
            // stop game
            gameOn = false;

            // if a win, assign score and time
            if (currentCorrectChoice == 1)
            {
                currentScore -= currentMSeconds / 1000;
            }
            else
            {
                currentScore = 0;
                
            }

            // save results
            SaveQuestionResults();
        }



        /// <summary>
        /// METHOD NAME : ChoiceB_Checked
        /// DESCRIPTION : This event is fired when radio button ChoiceB is checked.
        ///               It ends the game, records the current time, and assigns the score based on
        ///               whether or not this was the correct choice.
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="e">the event arguments</param>
        private void ChoiceB_Checked(object sender, RoutedEventArgs e)
        {
            // stop game
            gameOn = false;

            // if a win, assign score and time
            if (currentCorrectChoice == 2)
            {
                currentScore -= currentMSeconds / 1000;
            }
            // set score to 0 if not
            else
            {
                currentScore = 0;
            }

            // save results
            SaveQuestionResults();
        }



        /// <summary>
        /// METHOD NAME : ChoiceC_Checked
        /// DESCRIPTION : This event is fired when radio button ChoiceC is checked.
        ///               It ends the game, records the current time, and assigns the score based on
        ///               whether or not this was the correct choice.
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="e">the event arguments</param>
        private void ChoiceC_Checked(object sender, RoutedEventArgs e)
        {
            // stop game
            gameOn = false;

            // if won, assign score
            if (currentCorrectChoice == 3)
            {
                currentScore -= currentMSeconds / 1000;
            }
            // if not, assign score to 0
            else
            {
                currentScore = 0;
            }

            // save results
            SaveQuestionResults();
        }



        /// <summary>
        /// METHOD NAME : ChoiceD_Checked
        /// DESCRIPTION : This event is fired when radio button ChoiceD is checked.
        ///               It ends the game, records the current time, and assigns the score based on
        ///               whether or not this was the correct choice.
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="e">the event arguments</param>
        private void ChoiceD_Checked(object sender, RoutedEventArgs e)
        {
            // stop game
            gameOn = false;

            // assign score if correct
            if (currentCorrectChoice == 4)
            {
                currentScore -= currentMSeconds / 1000;
            }
            // if not, score = 0
            else
            {
                currentScore = 0;
            }

            // save results
            SaveQuestionResults();
        }
    }
}
