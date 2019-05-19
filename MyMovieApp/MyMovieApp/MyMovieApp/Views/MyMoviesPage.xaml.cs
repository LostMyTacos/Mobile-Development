using MyMovieApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMovieApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyMoviesPage : ContentPage
	{
        private ObservableCollection<Movie> _myMovies;

        public MyMoviesPage ()
		{
			InitializeComponent ();

            MyMoviesListView.RefreshCommand = new Command(() => {
                RefreshData();
                MyMoviesListView.IsRefreshing = false;
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var myMovies = await App.Database.GetMoviesAsync();
            _myMovies = new ObservableCollection<Movie>(myMovies);
            MyMoviesListView.ItemsSource = _myMovies;
        }

        async void OnMyMovieSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new MyMovieInfoPage
                {
                    BindingContext = e.SelectedItem as Movie
                });
            }
        }

        public async void RefreshData()
        {
            var myMovies = await App.Database.GetMoviesAsync();
            _myMovies = new ObservableCollection<Movie>(myMovies);
            MyMoviesListView.ItemsSource = _myMovies;
        }
    }
}