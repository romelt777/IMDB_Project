﻿using System;
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
using IMDB_Project.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace IMDB_Project.Views
{
    /// <summary>
    /// Interaction logic for MovieRatingView.xaml
    /// </summary>
    public partial class MovieRatingView : UserControl
    {
        public MovieRatingView()
        {
            InitializeComponent();
            DataContext = ((App)Application.Current).ServiceProvider.GetService<MovieRatingViewModel>();

        }
    }
}
