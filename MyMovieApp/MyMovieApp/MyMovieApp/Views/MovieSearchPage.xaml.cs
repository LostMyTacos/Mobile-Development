using MyMovieApp.Models;
using MyMovieApp.Services;

using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMovieApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieSearchPage : ContentPage
	{
		public MovieSearchPage ()
		{
            BindingContext = this;
            InitializeComponent();
		}

        private MovieService _service = new MovieService();
        
        private BindableProperty IsSearchingProperty =
            BindableProperty.Create("IsSearching", typeof(bool), typeof(MovieSearchPage), false);
        public bool IsSearching
        {
            get { return (bool)GetValue(IsSearchingProperty); }
            set { SetValue(IsSearchingProperty, value); }
        }

        async void OnTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (e.NewTextValue == null || e.NewTextValue.Length < MovieService.MinSearchLength)
                return;

            await FindMovies(name: e.NewTextValue);
        }

        async Task FindMovies(string name)
        {
            try
            {
                IsSearching = true;

                var movies = await _service.FindMoviesByName(name);
                moviesListView.ItemsSource = movies;
                moviesListView.IsVisible = movies.Any();
                notFound.IsVisible = !moviesListView.IsVisible;
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Could not retrieve the list of movies.", "OK");
            }
            finally
            {
                IsSearching = false;
            }
        }

        async void OnMovieSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new MovieInfoPage
                {
                    BindingContext = e.SelectedItem as Movie
                });
            }
        }

    }
}