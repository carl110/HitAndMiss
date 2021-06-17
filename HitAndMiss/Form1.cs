using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HitAndMiss
{
    public partial class Form1 : Form
    {
        int circle1X = 395;
        int circleyAxis = 70;
        int circle2X = 455;
        int circle3X = 515;
        int circleNumber = 1;
        int circleLine = 0;
        int[] circleArray = { 70, 120, 170, 220, 270, 320 };
        string[] guess = new string[3];
        string[] answer = { "Red", "Green", "Yellow", "Blue", "Black" };
        public Form1()
        {
            InitializeComponent();
        }
        public class Randomizer
        {
            public static void Randomize<T>(T[] items)
            {
                Random rand = new Random();
                // For each spot in the array, pick
                // a random item to swap into that spot.
                for (int i = 0; i < items.Length - 1; i++)
                {
                    int j = rand.Next(i, items.Length);
                    T temp = items[i];
                    items[i] = items[j];
                    items[j] = temp;
                }
            }
        }
        private void DrawCircleOutline(int xAxis, int yAxis)
        {
            Graphics myGraphics = base.CreateGraphics();
            Pen myPen = new Pen(Color.Black);
            SolidBrush mySolidBrush = new SolidBrush(Color.Red);
            myGraphics.DrawEllipse(myPen, xAxis, yAxis, 40, 40);
        }
        private void DrawCircle(int xAxis, int yAxis, Brush colour)
        {
            Graphics myGraphics = base.CreateGraphics();
            Pen myPen = new Pen(Color.Black);
            SolidBrush mySolidBrush = new SolidBrush(Color.Black);
            myGraphics.DrawEllipse(myPen, xAxis, yAxis, 40, 40);
            myGraphics.FillEllipse(colour, xAxis, yAxis, 40, 40);
        }
        private void ButtonPush(Brush colour, string colourGuess)
        {
            if (circleLine < circleArray.Length)
            {
                switch (circleNumber)
                {
                    case 1:
                        DrawCircle(circle1X, circleArray[circleLine], colour);
                        guess[0] = colourGuess;
                        circleNumber = 2;
                        break;
                    case 2:
                        DrawCircle(circle2X, circleArray[circleLine], colour);
                        guess[1] = colourGuess;
                        circleNumber = 3;
                        break;
                    case 3:
                        DrawCircle(circle3X, circleArray[circleLine], colour);
                        guess[2] = colourGuess;
                        circleNumber = 1;
                        circleLine += 1;
                        if (guess.SequenceEqual(answer))
                        {
                            MessageBox.Show("Congrate on guessing the correct sequence", "Correct", MessageBoxButtons.OK);
                        } else if (circleLine < circleArray.Length)
                        {
                            DrawCircleOutline(circle1X, circleArray[circleLine]);
                            DrawCircleOutline(circle2X, circleArray[circleLine]);
                            DrawCircleOutline(circle3X, circleArray[circleLine]);
                            AddLabel(575, circleArray[circleLine], circleLine + 1);
                        } else if (circleLine == circleArray.Length)
                        {
                            int newXAxis = 395;
                            for (int i = 0; i < answer.Length; i++)
                            {
                                switch (answer[i])
                                {
                                    case "Red":
                                        DrawCircle(newXAxis, 370, Brushes.Red);
                                        break;
                                    case "Yellow":
                                        DrawCircle(newXAxis, 370, Brushes.Yellow);
                                        break;
                                    case "Blue":
                                        DrawCircle(newXAxis, 370, Brushes.Blue);
                                        break;
                                    case "Black":
                                        DrawCircle(newXAxis, 370, Brushes.Black);
                                        break;
                                    case "Green":
                                        DrawCircle(newXAxis, 370, Brushes.Green);
                                        break;
                                }
                                newXAxis += 60;
                            }
                        }
                        break;
                }
            } 
        }
        private static string GetOrdinalSuffix(int num)
        {
            string number = num.ToString();
            if (number.EndsWith("1")) return num + "st";
            if (number.EndsWith("2")) return num + "nd";
            if (number.EndsWith("3")) return num + "rd";
            return num + "th";
        }
        private void AddLabel(int xAxis, int yAxis, int content)
        {
            Label namelabel = new Label();
            namelabel.Location = new Point(xAxis, yAxis);
            namelabel.Text = GetOrdinalSuffix(content) + " Guess";
            this.Controls.Add(namelabel);
        }
        private void btnRed_Click(object sender, EventArgs e)
        {
            ButtonPush(Brushes.Red, "Red");
        }
        private void btnBlue_Click(object sender, EventArgs e)
        {
            ButtonPush(Brushes.Blue, "Blue");
        }
        private void btnGreen_Click(object sender, EventArgs e)
        {
            ButtonPush(Brushes.Green, "Green");
        }
        private void btnYellow_Click(object sender, EventArgs e)
        {
            ButtonPush(Brushes.Yellow, "Yellow");
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            ButtonPush(Brushes.Black, "Black");
        }
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            btnStartGame.Dispose();
            DrawCircleOutline(circle1X, circleyAxis);
            DrawCircleOutline(circle2X, circleyAxis);
            DrawCircleOutline(circle3X, circleyAxis);
            DrawCircle(circle1X, circleyAxis + 300, Brushes.White);
            DrawCircle(circle2X, circleyAxis + 300, Brushes.White);
            DrawCircle(circle3X, circleyAxis + 300, Brushes.White);
            AddLabel(575, circleyAxis, circleLine + 1);
            Randomizer.Randomize(answer);
            Array.Resize(ref answer, 3);
        }
    }
}
