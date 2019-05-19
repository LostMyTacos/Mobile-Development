using MyMovieApp.Models;
using MyMovieApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMovieApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyMovieInfoPage : ContentPage
	{
		public MyMovieInfoPage ()
		{
			InitializeComponent ();
		}

        async void RemoveButton_Pressed(object sender, EventArgs e)
        {
            var movie = (Movie)BindingContext;
            await App.Database.DeleteMovieAsync(movie);
            await Navigation.PopAsync();
        }       
    }
}