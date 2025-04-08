using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IMDB_Project.ViewModels;

namespace IMDB_Project
{

    //SCAFFOLD COMMAND
    // Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=IMDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/Generated -namespace Models -contextdir Data -contextnamespace Data -table Titles, Writers, Ratings, Directors, Episodes, Genres -DataAnnotations 
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;

        }
    }
}