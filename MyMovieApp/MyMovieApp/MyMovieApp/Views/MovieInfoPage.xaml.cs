using MyMovieApp.Models;
using MyMovieApp.Services;

using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMovieApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieInfoPage : ContentPage
	{   
        public MovieInfoPage()
        {
            InitializeComponent();
        }

        async void AddButton_Pressed(object sender, EventArgs e)
        {
            var movie = (Movie)BindingContext;
            await App.Database.AddMovieAsync(movie);
            await Navigation.PopAsync();
        } 
    }
}