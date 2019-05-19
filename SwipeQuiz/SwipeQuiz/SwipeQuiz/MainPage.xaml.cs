using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwipeQuiz
{
    public partial class MainPage : ContentPage
    {
        public class Card
        {
            public string Question { get; set; }
            public string ImageURL { get; set; }
            public string Answer1 { get; set; }
            public string Answer2 { get; set; }
            public string CorrectAnswer { get; set; }
        }

        public int CorrectAnswers;
        public int IncorrectAnswers;
        public bool gameStarted = false;

        public int cardNumber = 0;
        public List<Card> Cards = new List<Card>();
        public Card currentCard;

        public MainPage()
        {
            InitializeComponent();

            Cards.Add(new Card()
            {
                Question = "Moose?",
                ImageURL = "https://cdn0.wideopenspaces.com/wp-content/uploads/2018/10/Alaska-Moose-Wallpaper__yvt2-630x339.jpg",
                Answer1 = "Yes", Answer2 = "No",
                CorrectAnswer = "Yes"                
            });

            Cards.Add(new Card()
            {
                Question = "Duck?",
                ImageURL = "https://cdn.newsapi.com.au/image/v1/5f450a3c1733038414d489d47d0babef?width=650",
                Answer1 = "Yes", Answer2 = "No",
                CorrectAnswer = "No"
            });

            Cards.Add(new Card()
            {
                Question = "Taco?",
                ImageURL = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/burrito-1554147258.jpg?crop=0.668xw:1.00xh;0.264xw,0&resize=640:*",
                Answer1 = "Yes", Answer2 = "No",
                CorrectAnswer = "No"
            });

            Cards.Add(new Card()
            {
                Question = "Banana?",
                ImageURL = "http://static1.squarespace.com/static/5956a474ccf2106856898d23/5abbb431758d462671c21b71/5abbb881758d462671c321f1/1522251977504/Cucumber.jpg",
                Answer1 = "Yes", Answer2 = "No",
                CorrectAnswer = "No"
            });

            Cards.Add(new Card()
            {
                Question = "Orlando Bloom?",
                ImageURL = "http://tribzap2it.files.wordpress.com/2011/03/bloom-wig.jpg?w=1400",
                Answer1 = "Yes", Answer2 = "No",
                CorrectAnswer = "Yes"
            });         

        } 

        async void OnSwiped(object sender, SwipedEventArgs e)
        {
            // _Label.Text = $"You swiped: {e.Direction.ToString()}";
            
            if (gameStarted == true)
            {
                switch (e.Direction)
                {
                    case SwipeDirection.Up:
                        // Handle the swipe
                        CheckAnswer(currentCard.Answer1);
                        break;
                    case SwipeDirection.Down:
                        // Handle the swipe
                        CheckAnswer(currentCard.Answer2);
                        break;
                    case SwipeDirection.Right:
                        // Handle the swipe                   
                        break;
                    case SwipeDirection.Left:
                        // Handle the swipe                       
                        break;
                }                
            }
            else if (gameStarted == false && (IncorrectAnswers > 1 || CorrectAnswers > 1))
            {
                var response = await DisplayAlert("Play Again?", "Reset The Game?", "Yes", "No");
                if (response == true)
                {
                    await DisplayAlert("Resetting Game", "Thanks for Playing!", "Ok");
                    StartGame();
                }
                else
                {
                    await DisplayAlert("Game Over", "Thanks for Playing!", "Ok");
                }
            }
            else
            {
                StartGame();
            }

        }

        void StartGame()
        {
            gameStarted = true;
            cardNumber = 0;
            IncorrectAnswers = 0;
            CorrectAnswers = 0;
            currentCard = Cards[cardNumber];
            _Start.Text = "";
            _Correct.Text = "Swipe Up to answer: Yes";
            _Incorrect.Text = "Swipe Down to answer: No";
            _Question.Text = currentCard.Question;
            _Image.Source = currentCard.ImageURL;
        }

        void CheckAnswer(string input)
        {
            var answer = currentCard.CorrectAnswer;
            if (input == answer)
            {
                CorrectAnswers++;
                NextQuestion();
            }
            else
            {
                IncorrectAnswers++;
                WrongAnswer();
            }
        }

        void NextQuestion()
        {
            cardNumber++;
            if(cardNumber == Cards.Count())
            {
                GameOver();
            }
            else
            {
                // _Start.Text = cardNumber.ToString() + " " +  Cards.Count.ToString();
                currentCard = Cards[cardNumber];
                _Question.Text = currentCard.Question;
                _Image.Source = currentCard.ImageURL;
            }
            
        }

        void WrongAnswer()
        {
            DisplayAlert("Incorrect...", "Nice Try!", "Continue");
            NextQuestion();
        }

        void GameOver()
        {
            gameStarted = false;
            if(CorrectAnswers > IncorrectAnswers)
            {
                _Question.Text = "You Won!";
                _Image.Source = "https://www.theonlinecitizen.com/wp-content/uploads/2019/02/thumbs-up-660x330.jpg";
            }
            else
            {
                _Question.Text = "You Lose! Better Luck Next Time!";              
                _Image.Source = "http://sandymoore.files.wordpress.com/2012/09/sad-boo.jpg";
            }
            _Incorrect.Text = "Incorrect Ansers: " + IncorrectAnswers.ToString();
            _Correct.Text = "Correct Answers: " + CorrectAnswers.ToString();
            _Start.Text = "Swipe In Any Direction to see Menu!";
        }


    }
}

