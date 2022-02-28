///
/// FILE          : MainWindow.xaml.cs
/// PROJECT       : RDA4
/// PROGRAMMER    : Elizabeth deVries
/// FIRST VERSION : 2021-12-09
/// DESCRIPTION   : This is the main window of the RDA4 Quiz game. It contains a frame that will load the different pages and allow navigation between them.
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        /// <summary>
        /// METHOD NAME : MainWindow
        /// DESCRIPTION : This is a constructor that launches the main window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

        }



        /// <summary>
        /// METHOD NAME : RDA4Quiz_Loaded
        /// DESCRIPTION : This event is fired when the main window is completely loaded. 
        ///             : It navigates the user to the first page, 'EnterName'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RDA4Quiz_Loaded(object sender, RoutedEventArgs e)
        {
            screenFrame.NavigationService.Navigate(new EnterName());
        }
    }
}
