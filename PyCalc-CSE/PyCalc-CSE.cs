using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization; 
class PyCalc_CSE
{
    private const string UPDATE_VERSION_URL = "https://gist.githubusercontent.com/Chill-Astro/e8c32d9a2b30780e3b6fd2819a51b330/raw/PyC_V.txt";
    private const string CURRENT_VERSION = "1.0";
    private static readonly HttpClient client = new HttpClient();
    static async Task Main(string[] args)
    {
        Console.WriteLine("PyCalc : A Simple and Lightweight Calculator. Made in C#!");
        Console.WriteLine($"Version : {CURRENT_VERSION}\n");

        await CheckForUpdatesAsync(); // Call the async update check

        Console.WriteLine("\nSelect a Mathematical Operation : ");
        Console.WriteLine("1. Addition");
        Console.WriteLine("2. Subtraction");
        Console.WriteLine("3. Multiplication");
        Console.WriteLine("4. Division");
        Console.WriteLine("5. Exponents (x^y)");
        Console.WriteLine("6. Square Root");
        Console.WriteLine("7. Cube Root");
        Console.WriteLine("8. Approximate / Rounding");
        Console.WriteLine("9. Heron's Formula");
        Console.WriteLine("10. Simple Interest");
        Console.WriteLine("11. Compound Interest");
        Console.WriteLine("12. Prime No. Check");
        Console.WriteLine("13. Triangle Check");
        Console.WriteLine("14. Right Triangle Check");
        Console.WriteLine("15. Perimiter [Various Shapes]");
        Console.WriteLine("16. Area [Varoius Shapes]");
        Console.WriteLine("17. Volume [Various Shapes]");
        Console.WriteLine("18. Surface Area [Various Shapes]");
        Console.WriteLine("19. Curved Surface Area [Various Shapes]");
        Console.WriteLine("20. Diagonal Calculation [Various Shapes]");
        Console.WriteLine("21. Factorial Calculator");
        Console.WriteLine("22. Exit PyCalc");

        while (true)
        {
            Console.Write("Enter choice [ 1 - 22 ] : ");
            string ch = Console.ReadLine();
            Console.WriteLine();

            switch (ch)
            {
                case "1":
                    PerformBinaryOperation("+", (a, b) => a + b);
                    break;
                case "2":
                    PerformBinaryOperation("-", (a, b) => a - b);
                    break;
                case "3":
                    PerformBinaryOperation("*", (a, b) => a * b);
                    break;
                case "4":
                    PerformDivision();
                    break;
                case "5":
                    PerformBinaryOperation("^", Math.Pow);
                    break;
                case "6":
                    PerformSquareRoot();
                    break;
                case "7":
                    PerformCubeRoot();
                    break;
                case "8":
                    PerformRounding();
                    break;
                case "9":
                    PerformHeronsFormula();
                    break;
                case "10":
                    PerformSimpleInterest();
                    break;
                case "11":
                    PerformCompoundInterest();
                    break;
                case "12":
                    CheckPrimeNumber();
                    break;
                case "13":
                    CheckTriangle();
                    break;
                case "14":
                    CheckRightTriangle();
                    break;
                case "15":
                    PerformPerimeterCalculations();
                    break;
                case "16":
                    PerformAreaCalculations(); // Placeholder call
                    break;
                case "17":
                    PerformVolumeCalculations(); // Placeholder call
                    break;
                case "18":
                    PerformSurfaceAreaCalculations(); // Placeholder call
                    break;
                case "19":
                    PerformCurvedSurfaceAreaCalculations(); // Placeholder call
                    break;
                case "20":
                    PerformDiagonalCalculations(); // Placeholder call
                    break;
                case "21":
                    CalculateFactorial();
                    break;
                case "22":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please enter a Valid Input!\n");
                    continue;
            }

            string nextCalc;
            do
            {
                Console.Write("Do you Want to Perform Another Calculation? [y/n]: ");
                nextCalc = Console.ReadLine().Trim().ToLower();
                Console.WriteLine();

                if (nextCalc == "n" || nextCalc == "no")
                {
                    return; // Exit Main method
                }
                else if (nextCalc == "y" || nextCalc == "yes")
                {
                    break; // Continue the main loop
                }
                else
                {
                     Console.WriteLine("Please enter 'y' or 'n'.\n");
                }
            } while (true);

            Console.WriteLine();
        }
    }
    private static async Task CheckForUpdatesAsync() // Update Check Thingy
    {
        try
        {
            client.Timeout = TimeSpan.FromSeconds(5);
            HttpResponseMessage response = await client.GetAsync(UPDATE_VERSION_URL);
            response.EnsureSuccessStatusCode();
            string latestVersion = (await response.Content.ReadAsStringAsync()).Trim();
            Version current = new Version(CURRENT_VERSION);
            Version latest = new Version(latestVersion);

            if (latest.CompareTo(current) > 0)
            {
                Console.WriteLine($"🎉 A NEW version of PyCalc-CSE is Available!");                
                Console.WriteLine("Please visit github.com/Chill-Astro/PyCalc to download the latest release!");
            }
            else if (latest.CompareTo(current) == 0)
            {
                Console.WriteLine("🎉 PyCalc-CSE is up to date!");
            }
            else
            {
                 Console.WriteLine("⚠️ This appears to be a DEV. Build.\n");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("⚠️ Could not check for updates. Please check your internet connection.");
            Console.WriteLine($"Error: {e.Message}");
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
             Console.WriteLine("⚠️ Update check timed out. Please check your internet connection.");
        }
        catch (FormatException)
        {             
             Console.WriteLine("⚠️ Could not parse the version from the update source.");         
        }
        catch (Exception e)
        {            
            Console.WriteLine("⚠️ An unexpected error occurred during the update check.");
            Console.WriteLine($"Error: {e.Message}");        
        }         
    }   
    private static void PerformBinaryOperation(string operatorSymbol, Func<double, double, double> operationFunc)
    {
        try
        {
            Console.Write("Enter first number : ");
            double x = double.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Enter second number : ");
            double y = double.Parse(Console.ReadLine());
            Console.WriteLine();

            double result = operationFunc(x, y);
            Console.WriteLine($"{x} {operatorSymbol} {y} = {result}\n");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.\n");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Input number is too large or too small.\n");
        }
        catch (Exception ex)
        {
             Console.WriteLine($"An error occurred: {ex.Message}\n");
        }
    }
    private static void PerformDivision()
    {
         try
         {
             Console.Write("Enter first number : ");
             double x = double.Parse(Console.ReadLine());
             Console.WriteLine();

             Console.Write("Enter second number : ");
             double y = double.Parse(Console.ReadLine());
             Console.WriteLine();

             if (y == 0)
             {
                 Console.WriteLine("Division by Zero Not Defined!\n");
             }
             else
             {
                 Console.WriteLine($"{x} / {y} = {x / y}\n");
             }
         }
         catch (FormatException)
         {
             Console.WriteLine("Invalid input. Please enter numbers only.\n");
         }
         catch (OverflowException)
         {
            Console.WriteLine("Input number is too large or too small.\n");
         }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred: {ex.Message}\n");
         }
    }
    private static void PerformSquareRoot()
    {
        try
        {
            Console.Write("Enter a number : ");
            double x = double.Parse(Console.ReadLine());
            Console.WriteLine();

            if (x < 0)
            {
                Console.WriteLine("Cannot calculate square root of a negative number.\n");
            }
            else
            {
                 Console.WriteLine($"Square root of {x} = {Math.Sqrt(x)}\n");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a number.\n");
        }
        catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred: {ex.Message}\n");
         }
    }
    private static void PerformCubeRoot()
    {
         try
        {
            Console.Write("Enter a number : ");
            double x = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine($"Cube root of {x} = {Math.Cbrt(x)}\n");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a number.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred: {ex.Message}\n");
         }
    }
    private static void PerformRounding()
    {
         try
        {
            Console.Write("Enter a number to round : ");
            double x = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine($"Rounded value: {Math.Round(x)}\n");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a number.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred: {ex.Message}\n");
         }
    }
    private static void PerformHeronsFormula()
    {
        try
        {
            Console.WriteLine("NOTE: Some Calculations may print 0.0 depending on the values!\n");
            Console.Write("Enter the first side [a] : ");
            double a = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Enter the second side [b] : ");
            double b = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Enter third side [c] : ");
            double c = double.Parse(Console.ReadLine());
            Console.WriteLine();

            if (a <= 0 || b <= 0 || c <= 0)
            {
                Console.WriteLine("Sides must be positive.\n");
                return;
            }

            if ((a + b > c) && (a + c > b) && (b + c > a))
            {
                 double s = (a + b + c) / 2.0; // Use 2.0 for double division
                 Console.WriteLine($"Perimeter / 2 (s) = {s}");

                 double area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));
                 Console.WriteLine($"Area : {area}\n");
            }
            else
            {
                Console.WriteLine("The given sides do not form a valid triangle.\n");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred: {ex.Message}\n");
         }
    }
    private static void PerformSimpleInterest()
    {
        try
        {
            Console.Write("Enter the Principal : ");
            double p = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Enter the Rate [%] : ");
            double r = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Enter the Time [Years] : ");
            double t = double.Parse(Console.ReadLine());
            Console.WriteLine();

            double si = (p * r * t) / 100.0;
            Console.WriteLine($"Simple Interest : {si}\nAmount : {(si + p)}\n");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred: {ex.Message}\n");
         }
    }
    private static void PerformCompoundInterest()
    {
        try
        {
            Console.WriteLine("Compound Interest Calculation\n");
            Console.Write("Enter the Principal : ");
            double p = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Enter the Rate [%] : ");
            double r = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Enter the number of times interest is compounded per year : ");
            double n = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Enter the Time [Years] : ");
            double t = double.Parse(Console.ReadLine());
            Console.WriteLine();

            if (n <= 0)
            {
                Console.WriteLine("Number of times compounded per year must be positive.\n");
                return;
            }

            double amount = p * Math.Pow((1 + r / (100.0 * n)), n * t);
            double ci = amount - p;
            Console.WriteLine($"Compound Interest : {ci}\nAmount : {amount}\n");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred: {ex.Message}\n");
         }
    }
    private static void CheckPrimeNumber()
    {
        try
        {
            Console.Write("Enter a Number : ");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (x < 2)
            {
                Console.WriteLine($"{x} is not a Prime Number\n");
            }
            else
            {
                bool isPrime = true;
                for (int i = 2; i <= Math.Sqrt(x); i++) // Optimized loop
                {
                    if (x % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    Console.WriteLine($"{x} is a Prime Number\n");
                }
                else
                {
                    Console.WriteLine($"{x} is not a Prime Number\n");
                }
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter an integer number.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small for an integer.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred: {ex.Message}\n");
         }
    }
    private static void CheckTriangle()
    {
         try
        {
            Console.Write("Enter first side [a] : ");
            double a = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Enter second side [b] : ");
            double b = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Enter third side [c] : ");
            double c = double.Parse(Console.ReadLine());
            Console.WriteLine();

            if (a <= 0 || b <= 0 || c <= 0)
            {
                Console.WriteLine("Sides must be positive.\n");
            }
            else if ((a + b > c) && (a + c > b) && (b + c > a))
            {
                Console.WriteLine("Valid Triangle\n");
            }
            else
            {
                Console.WriteLine("Not a Valid Triangle\n");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred: {ex.Message}\n");
         }
    }
    private static void CheckRightTriangle()
    {
         try
        {
            Console.Write("Enter first side [a] : ");
            double a = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Enter second side [b] : ");
            double b = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Enter third side [c] : ");
            double c = double.Parse(Console.ReadLine());
            Console.WriteLine();

            if (a <= 0 || b <= 0 || c <= 0)
            {
                Console.WriteLine("Sides must be positive.\n");
            }
            else
            {
                double[] sides = { a, b, c };
                Array.Sort(sides);
                double s1 = sides[0], s2 = sides[1], s3 = sides[2];

                // Use a small tolerance for floating-point comparison
                if (Math.Abs(Math.Pow(s1, 2) + Math.Pow(s2, 2) - Math.Pow(s3, 2)) < 1e-9)
                {
                    Console.WriteLine("Is a Right Triangle\n");
                }
                else
                {
                    Console.WriteLine("Not a Right Triangle.\n");
                }
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred: {ex.Message}\n");
         }
    }
    private static void PerformPerimeterCalculations()
    {
        Console.WriteLine("Perimeter Calculation [Various Shapes]\n" +
                          "Select a shape : \n" +
                          "1. Equilateral Triangle\n" +
                          "2. Isosceles Triangle\n" +
                          "3. Square / Rhombus\n" +
                          "4. Rectangle / Parallelogram\n" +
                          "5. Circle\n");

        Console.Write("Enter shape choice [ 1 - 5 ]: ");
        string sch = Console.ReadLine();
        Console.WriteLine();
        try
        {
            switch (sch)
            {
                case "1":
                    Console.Write("Enter Side Length [a] : ");
                    double a1 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine($"Perimeter : {3 * a1}\n");
                    break;
                case "2":
                    Console.Write("Enter Equal Side's Length : ");
                    double a2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter Non-Equal Side's Length : ");
                    double b2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine($"Perimeter : {2 * a2 + b2}\n");
                    break;
                case "3":
                    Console.Write("Enter Length of Side [a] : ");
                    double a3 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine($"Perimeter : {4 * a3}\n");
                    break;
                case "4":
                    Console.Write("Enter Length [l] : ");
                    double l4 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter Breadth [b] : ");
                    double b4 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine($"Perimeter : {2 * (l4 + b4)}\n");
                    break;
                case "5":
                    Console.Write("Enter the Radius [r] : ");
                    double r5 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double circumference = 2 * Math.PI * r5;
                    Console.WriteLine($"Circumference : {circumference}\n");
                    break;
                default:
                    Console.WriteLine("Invalid Option! Exiting Perimeter Calculation ...\n");
                    break;
            }
        }
         catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred during perimeter calculation: {ex.Message}\n");
         }
    }
     private static void PerformAreaCalculations()
    {
        Console.WriteLine("Area Calculation [Various Shapes]\n" +
                          "Select a shape : \n" +
                          "1. Equilateral Triangle\n" +
                          "2. Isosceles Triangle\n" +
                          "3. Square\n" +
                          "4. Rectangle / Parallelogram\n" +
                          "5. Rhombus\n" +
                          "6. Circle\n" +
                          "7. Semi-circle\n");

        Console.Write("Enter shape choice [ 1 - 7 ] : ");
        string sch = Console.ReadLine();
        Console.WriteLine();
        try
        {
            switch (sch)
            {
                case "1": // Equilateral Triangle
                    Console.Write("Enter Side Length [a] : ");
                    double a1 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double ar1 = (Math.Sqrt(3) / 4.0) * Math.Pow(a1, 2);
                    Console.WriteLine($"Area : {ar1}\n");
                    break;
                case "2": // Isosceles Triangle
                    Console.Write("Enter Equal Side's Length : ");
                    double a2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter Non-Equal Side's Length : ");
                    double b2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    // Check if it's a valid isosceles triangle with these sides first (optional but good practice)
                     if (2 * a2 <= b2)
                     {
                         Console.WriteLine("Invalid sides for an isosceles triangle (2a <= b).\n");
                         break;
                     }
                    double ar2 = (b2 / 4.0) * Math.Sqrt(4 * Math.Pow(a2, 2) - Math.Pow(b2, 2)); // Heron's formula for isosceles simplified
                    Console.WriteLine($"Area : {ar2}\n");
                    break;
                case "3": // Square
                    Console.Write("Enter Length of Side [a] : ");
                    double a3 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double ar3 = Math.Pow(a3, 2);
                    Console.WriteLine($"Area : {ar3}\n");
                    break;
                case "4": // Rectangle / Parallelogram
                    Console.Write("Enter Length [l] : ");
                    double l4 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter Breadth [b] : ");
                    double b4 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double ar4 = l4 * b4;
                    Console.WriteLine($"Area : {ar4}\n");
                    break;
                case "5": // Rhombus
                    Console.Write("Enter Diagonal 1 [d1] : ");
                    double d1_5 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter Diagonal 2 [d2] : ");
                    double d2_5 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double area5 = 0.5 * d1_5 * d2_5;
                    Console.WriteLine($"Area of Rhombus : {area5}\n");
                    break;
                case "6": // Circle
                    Console.Write("Enter the Radius [r] : ");
                    double r6 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double area6 = Math.PI * Math.Pow(r6, 2);
                    Console.WriteLine($"Area : {area6}\n");
                    break;
                 case "7": // Semi-circle
                    Console.Write("Enter the Radius [r] : ");
                    double r7 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double area7 = 0.5 * Math.PI * Math.Pow(r7, 2);
                    Console.WriteLine($"Area : {area7}\n");
                    break;
                default:
                    Console.WriteLine("Invalid Option! Exiting Area Calculation ...\n");
                    break;
            }
        }
         catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred during area calculation: {ex.Message}\n");
         }
    }
    private static void PerformVolumeCalculations()
    {
        Console.WriteLine("Volume Calculation [Various Shapes]\n" +
                          "Select a shape : \n" +
                          "1. Cube\n" +
                          "2. Cuboid\n" +
                          "3. Cylinder\n" +
                          "4. Cone\n" +
                          "5. Sphere\n");

        Console.Write("Enter shape choice [ 1 - 5 ] : ");
        string sch = Console.ReadLine();
        Console.WriteLine();
        try
        {
            switch (sch)
            {
                case "1": // Cube
                    Console.Write("Enter Length of Side [a] : ");
                    double a1 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double vol1 = Math.Pow(a1, 3);
                    Console.WriteLine($"Volume : {vol1}\n");
                    break;
                case "2": // Cuboid
                    Console.Write("Enter Length [l] : ");
                    double l2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter Breadth [b] : ");
                    double b2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter Height [h] : ");
                    double h2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double vol2 = l2 * b2 * h2;
                    Console.WriteLine($"Volume : {vol2}\n");
                    break;
                case "3": // Cylinder
                    Console.Write("Enter the Radius [r] : ");
                    double r3 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter the Height [h] : ");
                    double h3 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double vol3 = Math.PI * Math.Pow(r3, 2) * h3;
                    Console.WriteLine($"Volume : {vol3}\n");
                    break;
                case "4": // Cone
                    Console.Write("Enter the Radius [r] : ");
                    double r4 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter the Height [h] : ");
                    double h4 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double vol4 = (1.0/3.0) * Math.PI * Math.Pow(r4, 2) * h4; // Use 1.0/3.0 for double division
                    Console.WriteLine($"Volume : {vol4}\n");
                    break;
                case "5": // Sphere
                    Console.Write("Enter the Radius [r] : ");
                    double r5 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double vol5 = (4.0/3.0) * Math.PI * Math.Pow(r5, 3); // Use 4.0/3.0 for double division
                    Console.WriteLine($"Volume : {vol5}\n");
                    break;
                default:
                    Console.WriteLine("Invalid Option! Exiting Volume Calculation ...\n");
                    break;
            }
        }
         catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred during volume calculation: {ex.Message}\n");
         }
    }
    private static void PerformSurfaceAreaCalculations()
    {
         Console.WriteLine("Surface Area Calculation [Various Shapes]\n" +
                          "Select a shape : \n" +
                          "1. Cube\n" +
                          "2. Cuboid\n" +
                          "3. Cylinder\n" +
                          "4. Cone\n" +
                          "5. Sphere\n");

        Console.Write("Enter shape choice [ 1 - 5 ] : ");
        string sch = Console.ReadLine();
        Console.WriteLine();
        try
        {
            switch (sch)
            {
                case "1": // Cube
                    Console.Write("Enter Length of Side [a] : ");
                    double a1 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double sa1 = 6 * Math.Pow(a1, 2);
                    Console.WriteLine($"Surface Area : {sa1}\n");
                    break;
                case "2": // Cuboid
                    Console.Write("Enter Length [l] : ");
                    double l2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter Breadth [b] : ");
                    double b2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter Height [h] : ");
                    double h2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double sa2 = 2 * (l2 * b2 + b2 * h2 + h2 * l2);
                    Console.WriteLine($"Surface Area : {sa2}\n");
                    break;
                case "3": // Cylinder
                    Console.Write("Enter the Radius [r] : ");
                    double r3 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter the Height [h] : ");
                    double h3 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double sa3 = 2 * Math.PI * r3 * (r3 + h3);
                    Console.WriteLine($"Surface Area : {sa3}\n");
                    break;
                case "4": // Cone
                    Console.Write("Enter the Radius [r] : ");
                    double r4 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter the Height [h] : ");
                    double h4 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double sa4 = Math.PI * r4 * (r4 + Math.Sqrt(Math.Pow(h4, 2) + Math.Pow(r4, 2)));
                    Console.WriteLine($"Surface Area : {sa4}\n");
                    break;
                case "5": // Sphere
                    Console.Write("Enter the Radius [r] : ");
                    double r5 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double sa5 = 4 * Math.PI * Math.Pow(r5, 2);
                    Console.WriteLine($"Surface Area : {sa5}\n");
                    break;
                default:
                    Console.WriteLine("Invalid Option! Exiting Surface Area Calculation ...\n");
                    break;
            }
        }
         catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred during surface area calculation: {ex.Message}\n");
         }
    }

     private static void PerformCurvedSurfaceAreaCalculations()
    {
         Console.WriteLine("Curved Surface Area Calculation [Various Shapes]\n" +
                          "Select a shape : \n" +
                          "1. Cylinder\n" +
                          "2. Cone\n" +
                          "3. Sphere\n");

        Console.Write("Enter shape choice [ 1 - 3 ] : ");
        string sch = Console.ReadLine();
        Console.WriteLine();

        try
        {
            switch (sch)
            {
                case "1": // Cylinder
                    Console.Write("Enter the Radius [r] : ");
                    double r1 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter the Height [h] : ");
                    double h1 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double csa1 = 2 * Math.PI * r1 * h1;
                    Console.WriteLine($"Curved Surface Area : {csa1}\n");
                    break;
                case "2": // Cone
                    Console.Write("Enter the Radius [r] : ");
                    double r2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter the Height [h] : ");
                    double h2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double csa2 = Math.PI * r2 * Math.Sqrt(Math.Pow(h2, 2) + Math.Pow(r2, 2));
                    Console.WriteLine($"Curved Surface Area : {csa2}\n");
                    break;
                case "3": // Sphere
                    Console.Write("Enter the Radius [r] : ");
                    double r3 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double csa3 = 4 * Math.PI * Math.Pow(r3, 2); // Sphere CSA is same as Total Surface Area
                    Console.WriteLine($"Curved Surface Area : {csa3}\n");
                    break;
                default:
                    Console.WriteLine("Invalid Option! Exiting Curved Surface Area Calculation ...\n");
                    break;
            }
        }
         catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred during curved surface area calculation: {ex.Message}\n");
         }
    }

    private static void PerformDiagonalCalculations()
    {
        Console.WriteLine("Diagonal Calculation [Various Shapes]\n" +
                          "Select a shape : \n" +
                          "1. Square\n" +
                          "2. Rectangle\n" +
                          "3. Cube\n" +
                          "4. Cuboid\n");

        Console.Write("Enter shape choice [ 1 - 4 ] : "); // Corrected range from 5 to 4
        string sch = Console.ReadLine();
        Console.WriteLine();

        try
        {
            switch (sch)
            {
                case "1": // Square
                    Console.Write("Enter Length of Side [a] : ");
                    double a1 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double d1 = Math.Sqrt(2) * a1;
                    Console.WriteLine($"Diagonal : {d1}\n");
                    break;
                case "2": // Rectangle
                    Console.Write("Enter Length [l] : ");
                    double l2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter Breadth [b] : ");
                    double b2 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double d2 = Math.Sqrt(Math.Pow(l2, 2) + Math.Pow(b2, 2));
                    Console.WriteLine($"Diagonal : {d2}\n");
                    break;
                case "3": // Cube
                    Console.Write("Enter Length of Side [a] : ");
                    double a3 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double d3 = Math.Sqrt(3) * a3;
                    Console.WriteLine($"Diagonal : {d3}\n");
                    break;
                case "4": // Cuboid
                    Console.Write("Enter Length [l] : ");
                    double l4 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter Breadth [b] : ");
                    double b4 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter Height [h] : ");
                    double h4 = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    double d4 = Math.Sqrt(Math.Pow(l4, 2) + Math.Pow(b4, 2) + Math.Pow(h4, 2));
                    Console.WriteLine($"Diagonal : {d4}\n");
                    break;
                default:
                    Console.WriteLine("Invalid Option! Exiting Diagonal Calculation ...\n");
                    break;
            }
        }
         catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.\n");
        }
         catch (OverflowException)
        {
           Console.WriteLine("Input number is too large or too small.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred during diagonal calculation: {ex.Message}\n");
         }
    }
    private static void CalculateFactorial()
    {
        try
        {
            Console.Write("Enter a non-negative integer for factorial : ");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (x < 0)
            {
                Console.WriteLine("\nFactorial Not Defined for Negative Numbers!\n");
            }
            else if (x == 0 || x == 1)
            {
                Console.WriteLine($"\nFactorial of {x} is 1\n");
            }
            else
            {
                long fact = 1;
                checked
                {
                    for (int i = 2; i <= x; i++)
                    {
                         fact *= i;
                    }
                }
                Console.WriteLine($"\nFactorial of {x} is {fact}\n");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a non-negative integer.\n");
        }
        catch (OverflowException)
        {
            Console.WriteLine($"Input number is too large, the factorial exceeds the maximum value for a long.\n");
        }
         catch (Exception ex)
         {
             Console.WriteLine($"An error occurred: {ex.Message}\n");
         }
    }
}