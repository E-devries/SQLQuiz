///
/// FILE : Answer.cs
/// PROJECT : SQLQuiz
/// PROGRAMMER : Elizabeth deVries
/// FIRST VERSION : 2021-12-09
/// DESCRIPTION   : This file contains the Answer class, which acts as a simple structure to hold information about an answer in the database
/// 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQuiz
{


    /// <summary>
    /// CLASS NAME  : Answer
    /// DESCRIPTION : This class holds information about a quiz answer. It contains an answerID, whether or not it is a correct answer, and answer text.
    /// </summary>
    public class Answer
    {
        public const int ANSWER_PER_QUESTION = 4;
        public const string ANSWER_ERROR = "INVALID ANSWER";

        private int answerID;
        public int AnswerID { get { return answerID; } }

        private bool isCorrect;
        public bool IsCorrect { get { return isCorrect; } }

        private string answerText;
        public string AnswerText {  get { return answerText; } }



        /// <summary>
        /// METHOD NAME : Answer
        /// DESCRIPTION : THis is a constructor that populates the answer object once with values from the database.
        /// </summary>
        /// <param name="id">the id of the answer</param>
        /// <param name="correct">whether or not the answer is correct</param>
        /// <param name="text">the text that makes up the answer</param>
        public Answer(int id, bool correct, string text)
        {
            answerID = id;
            isCorrect = correct;
            answerText = text;
        }
    }
}
