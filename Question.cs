///
/// FILE          : Question.cs
/// PROJECT       : SQLQuiz
/// PROGRAMMER    : Elizabeth deVries
/// FIRST VERSION : 2021-12-09
/// DESCRIPTION   : This file contains the class Question, which acts as a simple container for the Question data table


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQuiz
{

    /// <summary>
    /// CLASS NAME  : Question
    /// DESCRIPTION : This class contains data members to represent the data associated with a question. Each Question has an id, and question text.
    /// </summary>
    public class Question
    {
        public const string QUESTION_ERROR = "INVALID_QUESTION";

        private int questionID;
        public int QuestionID { get { return questionID; } }

        private string questionText;
        public string QuestionText { get { return questionText; } }

        public Question(int id, string text)
        {
            questionID = id;
            questionText = text;
        }
    }
}
