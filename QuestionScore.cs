///
/// FILE : QuestionScore.cs
/// PROJECT : SQLQuiz
/// PROGRAMMER : Elizabeth deVries
/// FIRST VERSION : 2021-12-09
/// DESCRIPTION   : This file contains the QuestionScore class, which acts as a simple container for questionscore data from the database
/// 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQuiz
{

    /// <summary>
    /// CLASS NAME  : QuestionScore
    /// DESCRIPTION : This class represents the questionScore data. It contains the question id, the time taken to answer the question, 
    ///             : and the score associated with the question. It also contains the text of the correct answer.
    /// </summary>
    public class QuestionScore
    {
        public const string QSCORE_ERROR = "INVALID QUESTIONSCORE";
        public const int MAX_SCORES = 10;


        private int questionID;
        public int QuestionID { get { return questionID; } }

        private int answerTime;
        public int AnswerTime { get { return answerTime; } }

        private int answerScore;
        public int AnswerScore { get { return answerScore; } }

        private string correctAnswer;
        public string CorrectAnswer { get { return correctAnswer; } }

        public QuestionScore(int qID, int time, int score, string answer)
        {
            questionID = qID;
            answerTime = time;
            answerScore = score;
            correctAnswer = answer;
        }
         
    }
}
