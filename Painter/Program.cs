
using System;
using System.Collections;

namespace PaintCalculator
{
    class Paint
    {
        public string colour;
        public float cost;
        public float totalLitres = 0;
        public float litresPerTin;
        public SortedDictionary<float, float> TinSizes = new SortedDictionary<float, float>();
        public IDictionary<float, float> TinUsage = new SortedDictionary<float, float>();
        public Paint(string colourGiven)
        {
            colour = colourGiven;

        }

        public int numberOfTins()
        {
            float tins = totalLitres / litresPerTin;
            return ((int)Math.Ceiling(tins));
        }

        public void addTin(float size, float cost)
        {
            TinSizes.Add(size, cost);
        }

        public void outputTins()
        {
            foreach (KeyValuePair<float, float> kvp in TinSizes)
            {
                Console.WriteLine("Size {0}: ", kvp.Key);
            }
        }

        public float calculateCombinations()
        {
            float totalCost = 0;
            for (int i = TinSizes.Count - 1; i >= 0; i--)
            {
                int numberOfTins = 0;
                while ((totalLitres > TinSizes.ElementAt(i).Key))
                {
                    numberOfTins += 1;
                    totalLitres -= TinSizes.ElementAt(i).Key;
                }
                if (i == 0 && totalLitres > 0)
                {
                    numberOfTins += 1;
                }

                Console.WriteLine("You need {1} tin/tins at size of {0} ", TinSizes.ElementAt(i).Key, numberOfTins);
                totalCost += (numberOfTins * TinSizes.ElementAt(i).Value);
            }
            Console.WriteLine("The overall cost for {0} paint is :{1}", colour, totalCost);
            return totalCost;
        }

    }


    class Program
    {


        static void Main(string[] args)
        {

            //variable declaration
            float doorWidth = 0, doorLength = 0;
            float windowWidth = 0, windowLength = 0;
            float paintPerLitre = 2.5f;
            float wallWidth = 0, wallLength = 0;
            int paintDoor = -1;
            int coats = 0;
            int walls = 0;
            List<Paint> paints = new List<Paint>();
            float totalCost = 0;
            string paintColourName = "Yes";
            float wallArea = wallWidth * wallLength;
            float currentPaintCost;




            // Number of walls
            Console.WriteLine("Welcome to your paint app ");
            


            while (walls <= 0)
            {
                
                try 
                {
                    Console.WriteLine("Please enter the number of walls you would like to paint, make sure it is a postive number :) :");
                    walls = int.Parse(Console.ReadLine()); 
                }

                catch (Exception e) 
                { 
                    Console.WriteLine("Please enter a positive number :) : "); 
                }
            }



            // Wall measurements
            for (int i = 0; i < walls; i++)
            {


                Console.WriteLine("Please enter the width of the wall number {0} in meters: ", i + 1);
                try { wallWidth = float.Parse(Console.ReadLine()); }
                catch (Exception e) { Console.WriteLine(e.Message);}

                while (wallWidth <= 0)
                {
                    Console.WriteLine("Please enter a positive number :) : ");
                    try { wallWidth = float.Parse(Console.ReadLine()); }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }


                while (wallLength <= 0)
                {
                    try 
                    {
                        Console.WriteLine("Please enter the length of the wall number {0} in meters: ", i + 1);
                        wallLength = float.Parse(Console.ReadLine()); 
                    }
                    catch (Exception e) { Console.WriteLine("Please enter a correct value: "); }
                }



                wallArea = wallWidth * wallLength;
                // Any doors in the way

                Console.WriteLine("How many doors would you not want to paint on wall number {0}? Please enter 0 if none:", i + 1);
                while (paintDoor < 0)
                {
                    try
                    {
                        paintDoor = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input, please enter a correct value:");
                    }
                }


                int x = 0;
                while (x < paintDoor)
                {
                    Console.WriteLine("Please enter the width of the door in meters: ");
                    try { doorWidth = float.Parse(Console.ReadLine()); }
                    catch (Exception e) { Console.WriteLine(e.Message);}
                    while (doorWidth < 0 || doorWidth> wallWidth)
                    {
                        Console.WriteLine("Please enter a correct value :) : ");
                        try { doorWidth = float.Parse(Console.ReadLine()); }
                        catch (Exception e) { Console.WriteLine(e.Message); }
                    }

                    Console.WriteLine("Please enter the length of the door in meters: ");
                    try { doorLength = float.Parse(Console.ReadLine()); }
                    catch (Exception e) { Console.WriteLine(e.Message);}

                    while (doorLength < 0 || doorLength > wallLength)
                    {
                        Console.WriteLine("Please enter a correct value :) : ");
                        try { doorLength = float.Parse(Console.ReadLine());}
                        catch (Exception e) { Console.WriteLine(e.Message);}
                    }

                    wallArea -= doorWidth * doorLength;
                    if (wallArea < 0)
                    {
                        Console.WriteLine("The door is too big for the wall, please enter a correct measurement :) :");
                        wallArea += doorWidth * doorLength;
                        x--;
                    }
                    x++;
                }



                // Any windows in the way

                Console.WriteLine("Please enter the number of windows that are in the way, if none just enter 0:  ");
                int windows = int.Parse(Console.ReadLine());
                x = 0;

                while (x < windows)
                {
                    Console.WriteLine("Please enter the width of the window in meters: ");
                    try { windowWidth = float.Parse(Console.ReadLine()); }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                    while (windowWidth < 0 || windowWidth > wallWidth)
                    {
                        Console.WriteLine("Please enter a correct value :) : ");
                        try { windowWidth = float.Parse(Console.ReadLine()); }
                        catch (Exception e) { Console.WriteLine(e.Message); }
                    }
                    Console.WriteLine("Please enter the length of the window in meters: ");
                    try { windowLength = float.Parse(Console.ReadLine()); }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                    while (windowLength < 0 || windowLength > wallLength)
                    {
                        Console.WriteLine("Please enter a correct value :) : ");
                        try { windowLength = float.Parse(Console.ReadLine()); }
                        catch (Exception e) { Console.WriteLine(e.Message); }
                    }
                    wallArea -= windowWidth * windowLength;
                    if (wallArea < 0)
                    {
                        Console.WriteLine("The window is too big for the wall, please enter a correct measurement :) : ");
                        wallArea += windowWidth * windowLength;
                        x--;
                    }
                    x++;
                }

              

                // Number of coats
                while (coats < 1)
                {
                    try { Console.WriteLine("Please enter the number of coats you would like to have for this wall: ");  
                     coats = int.Parse(Console.ReadLine()); }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }



                // Calculate the paint needed
                float paintNeeded = (wallArea / paintPerLitre) * coats;



                // Colour for the Paint for the wall
                Console.WriteLine("Please enter the colour you want to paint the wall: ");
                paintColourName = Console.ReadLine();
                //Colour doesn't exist
                if (paints.Count == 0)
                {

                    Paint colouredPaint = new Paint(paintColourName);
                    Console.WriteLine("How many sizes of tin cans can you buy for this colour?  ");
                    int numberOfSize = int.Parse(Console.ReadLine());

                    for (int size = 0; size < numberOfSize; size++)
                    {
                        Console.WriteLine("Please enter the litres for this sized tin: ");
                        float litresPerTin = float.Parse(Console.ReadLine());

                        Console.WriteLine("Please enter the cost for this sized tin: ");
                        currentPaintCost = float.Parse(Console.ReadLine());


                        colouredPaint.addTin(litresPerTin, currentPaintCost);
                    }

                    paints.Add(colouredPaint);
                    colouredPaint.totalLitres = paintNeeded;

                }

                else
                {
                    bool exist = false;
                    foreach (var paint in paints)
                    {
                        //Colour already exist
                        if (paint.colour == paintColourName)
                        {
                            Console.WriteLine("It exists");
                            paint.totalLitres += paintNeeded;
                            Console.WriteLine(paint.totalLitres);
                            exist = true;
                        }

                    }

                    if (!exist)
                    {

                        Paint colouredPaint = new Paint(paintColourName);

                        Console.WriteLine("How many sizes of tin cans can you buy for this colour? ");
                        int numberOfSize = int.Parse(Console.ReadLine());

                        for (int size = 0; size < numberOfSize; size++)
                        {
                            Console.WriteLine("Please enter the cost for this sized tin: ");
                            currentPaintCost = float.Parse(Console.ReadLine());

                            Console.WriteLine("Please enter the litres for this sized tin: ");
                            float litresPerTin = float.Parse(Console.ReadLine());


                            colouredPaint.addTin(currentPaintCost, litresPerTin);
                        }


                        paints.Add(colouredPaint);
                        colouredPaint.totalLitres = paintNeeded;
                    }



                }


            }
            // Output for each paint and cost
            foreach (var paint in paints)
            {
                Console.WriteLine("----------------");
                Console.WriteLine("The total litres of paint needed for {0} is {1}", paint.colour, paint.totalLitres);
                float paintTotalCost = paint.calculateCombinations();

                Console.WriteLine("Total cost for {0} paint is {1}", paint.colour, paintTotalCost);
                totalCost += paintTotalCost;
                Console.WriteLine("----------------");

            }
            Console.WriteLine("----------------");
            Console.WriteLine("The total cost of all the walls are: " + totalCost.ToString("#.##"));
        }
    }
}