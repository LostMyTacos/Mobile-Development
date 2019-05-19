using System;
using System.IO;
using Xamarin.Forms;

namespace TipCalculator
{
    public partial class MainPage : ContentPage
    {
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tip.txt");

        public MainPage()
        {
            InitializeComponent();

            if (File.Exists(_fileName))
            {
                editor.Text = File.ReadAllText(_fileName);
            }
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            double bill = double.Parse(editor.Text);
            double tipAmount = double.Parse(editor1.Text) / 100;
            double tipTotal = bill * tipAmount;
            double billTotal = bill + tipTotal;
            tip.Text = "Orginal Bill: $" + bill.ToString() + "\nTip: $" + tipTotal.ToString() + "\nTotal Bill: $" + billTotal.ToString();
        }

        void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            editor.Text = string.Empty;
            editor1.Text = string.Empty;
        }
    }
}
