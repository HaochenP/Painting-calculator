
using System;
using System.Collections;

namespace PaintCalculator
{

    class Paint
    {
        public string colour { get; set; }
        public float TotalLitres { get; set; }
        public SortedDictionary<float, float> TinsSizes { get; set; }

        public Paint(string colour)
        {
            this.colour = colour;
            TinsSizes = new SortedDictionary<float, float>();
        }

        public void AddTin(float size, float cost)
        {
            TinsSizes.Add(size, cost);
        }

        public float CalculateCombinations()
        {
            float totalCost = 0;
            float remainingLitres = TotalLitres;
            foreach (var tin in TinsSizes.Reverse())
            {
                int numberOfTins = (int)Math.Floor(remainingLitres / tin.Key);
                remainingLitres -= numberOfTins * tin.Key;

                Console.WriteLine("You need {1} tin/tins at size of {0} ", tin.Key, numberOfTins);
                totalCost += numberOfTins * tin.Value;
            }

            if (remainingLitres > 0)
            {
                Console.WriteLine("You need {1} tin/tins at size of {0} ", remainingLitres, 1);
                totalCost += remainingLitres * TinsSizes.First().Value;
            }

            Console.WriteLine("The overall cost for {0} paint is :{1}", colour, totalCost);
            return totalCost;
        }










        class Program
        {
            private static float GetPositiveFloat(string prompt)
            {
                float input = 0;
                bool valid = false;
                while (!valid)
                {
                    Console.WriteLine(prompt);
                    string inputString = Console.ReadLine();
                    valid = float.TryParse(inputString, out input);
                    if (!valid)
                    {
                        Console.WriteLine("Invalid input");
                    }
                    else if (input <= 0)
                    {
                        Console.WriteLine("Input must be greater than 0");
                        valid = false;
                    }
                }
                return input;
            }

            private static int GetPositiveInt(string prompt)
            {
                int input = 0;
                bool valid = false;
                while (!valid)
                {
                    Console.WriteLine(prompt);
                    string inputString = Console.ReadLine();
                    valid = int.TryParse(inputString, out input);
                    if (!valid)
                    {
                        Console.WriteLine("Invalid input");
                    }
                    else if (input <= 0)
                    {
                        Console.WriteLine("Input must be greater than 0");
                        valid = false;
                    }
                }
                return input;
            }


            private static float GetTotalArea(int count, string type)
            {
                float area = 0;

                float elementWidth = GetPositiveFloat("Please enter the width of  " + type + " " + count + " in meters: ");
                float elementHeight = GetPositiveFloat("Please enter the height of " + type + " " + count + " in meters: ");
                area += elementWidth * elementHeight;

                return area;
            }

            private static int GetNonNegativeInt(string prompt)
            {
                int input = 0;
                bool valid = false;
                while (!valid)
                {
                    Console.WriteLine(prompt);
                    string inputString = Console.ReadLine();
                    valid = int.TryParse(inputString, out input);
                    if (!valid)
                    {
                        Console.WriteLine("Invalid input");
                    }
                    else if (input < 0)
                    {
                        Console.WriteLine("Input must be greater than or equal to 0");
                        valid = false;
                    }
                }
                return input;
            }

            static void Main(string[] args)
            {

                //variable declaration
                List<Paint> paints = new List<Paint>();
                float totalCost = 0;



                // Number of walls
                Console.WriteLine("Welcome to your paint app ");

                int wallCount = GetPositiveInt("Please enter the number of walls: ");

                for (int i = 0; i < wallCount; i++)
                {
                    Console.WriteLine("Wall number {0}", i + 1);
                    float wallArea = GetTotalArea(i, "Wall");



                    // Number of windows
                    int windowCount = GetNonNegativeInt("Please enter the number of windows: ");
                    for (int window = 0; window < windowCount; window++)
                    {
                        Console.WriteLine("Window number {0}", i + 1);
                        float windowArea = GetTotalArea(window, "Window");
                        wallArea -= windowArea;
                    }

                    // Number of doors
                    int doorCount = GetNonNegativeInt("Please enter the number of doors: ");
                    for (int door = 0; door < doorCount; door++)
                    {
                        Console.WriteLine("Door number {0}", door + 1);
                        float doorArea = GetTotalArea(door, "Door");
                        wallArea -= doorArea;
                    }

                    // Total area
                    float totalArea = wallArea;
                    Console.WriteLine("The total area to be painted is {0} meters squared", totalArea);

                    // Paint
                    int coats = GetPositiveInt("Please enter the number of coats: ");
                    float paintNeeded = totalArea / 2.5f * coats;

                    // Paint colours
                    Console.WriteLine("Please enter the colour you would like to use: ");
                    string colour = Console.ReadLine();

                    Paint paint = paints.Find(p => p.colour == colour);
                    if (paint == null)
                    {
                        paint = new Paint(colour);
                        int tinCount = GetPositiveInt("How many sizes of tins can you buy?: ");
                        for (int x = 0; x < tinCount; x++)
                        {
                            float size = GetPositiveFloat("Please enter the size of tin " + (x + 1) + " in litres: ");
                            float cost = GetPositiveFloat("Please enter the cost of tin " + (x + 1) + " in pounds: ");
                            paint.AddTin(size, cost);
                        }


                        paints.Add(paint);
                    }
                    paint.TotalLitres += paintNeeded;
                }
                foreach (var paint in paints)
                {
                    Console.WriteLine("----------------");
                    Console.WriteLine("The total litres of paint needed for {0} is {1}", paint.colour, paint.TotalLitres);
                    float paintTotalCost = paint.CalculateCombinations();
                    Console.WriteLine("Total cost for {0} paint is {1}", paint.colour, paintTotalCost);
                    totalCost += paintTotalCost;
                    Console.WriteLine("----------------");

                }
                Console.WriteLine("The total cost for all paints is {0}", totalCost);



            }
        }
    }
}