using IMDB_Project.ViewModels;
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

namespace IMDB_Project.Views
{
    public partial class EpisodeView : UserControl
    {
        public EpisodeView(EpisodeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null &&
                scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight &&
                DataContext is EpisodeViewModel viewModel)
            {
                viewModel.LoadMore();
            }
        }
    }


   
    
}


