/// FILE          : EnterName.xaml.cs
/// PROJECT       : RDA4
/// PROGRAMMER    : Elizabeth deVries
/// FIRST VERSION : 2021-12-09
/// DESCRIPTION   : This is the first screen of the RDA4 quiz game. It takes the user's name and does not start the game
///               : until a name is entered. The name can be any characters as long as it is not blank.
///               


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
    /// Interaction logic for EnterName.xaml
    /// </summary>
    public partial class EnterName : Page
    {
        public EnterName()
        {
            InitializeComponent();
        }

        private void submitName_Click(object sender, RoutedEventArgs e)
        {
            // see if the name is empty after being trimmed of whitespace
            string name = nameField.Text;
            name = name.Trim();

            // if yes, send name to data layer and play game
            if (name != "")
            {
                // insert player
                DatabaseConnector playerInsert = new DatabaseConnector();
                Player newPlayer = playerInsert.InsertPlayer(name);

                // go to play game
                this.NavigationService.Navigate(new PlayGame(newPlayer));
            }
            // if not, warn user
            else
            {
                MessageBox.Show("Sorry, you must enter a user name to continue!");
            }
           
        }
    }
}
