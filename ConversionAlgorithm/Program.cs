using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversionAlgorithm
{
    class Program
    {
        static double inputNum;
        static string inputUnit;
        static string[] input;
        static double tsp;
        static double tbsp;
        static double cup;
        static double pint;
        static double quart;
        static double gallon;
        static double ounce;
        static double lbs;
        static double flOz;
        static double mL;
        static double gram;
        static double stick;
        static string[] measureType = { "tsp", "tbsp", "cup", "pt", "qt", "gal", "oz", "lbs", "floz", "mL", "g", "stick" };
        // sticks will only be allowed/used if butter is the ingredient
        static bool butterUsed;
        static int enteredMeasureType = 0;
        static List<Ingredient> ingredients = new List<Ingredient>();
        static string simplified;
        static double[] conversionRate = { 3, 16, 2, 2, 4, 0, 16 };
        //see values[] (line 91)        values[n] * conversionRate[n] = values[n+1]

        //1 mL = .0338140227 fl oz

        /*
1 cup granulated sugar	        7 ounces (207 grams)
1 cup confectioner's sugar	    4 ounces (118 grams)
1 cup brown sugar, packed	    7 ounces (207 grams)
1 cup cornstarch	            4 1/2 ounces (133 grams)
1 cup cornmeal	                5 ounces (148 grams)
1 cup cocoa powder	            3 ounces (89 grams) 
1 cup butter                    	226.8g (2 sticks)
         
1 cup of vegetable oil	    7.7 oz	218 g
1 cup of canola oil	        7.6 oz	215 g
1 cup of heavy cream	    8.2 oz	232 g
1 cup of sour cream	        8.5 oz	242 g
1 cup of buttermilk	        8.5 oz	242 g
1 cup of whole milk	        8.5 oz	242 g
1 cup half and half	        8.5 oz	242
             

1 tsp baking powder		    4.9 g
1 tsp baking soda		    5 g
1 tsp yeast, instant		3.2 g
1 tsp yeast, active dry		3.1 g
1 tsp salt		            6.7 g

            Baking Soda: 1 tsp= 7 grams
Baking Powder: 1 tsp= 0.17oz, 5 grams
             */




        static void Main(string[] args)
        {
            ingredients.Add(new Ingredient("water"));
            ingredients.Add(new Ingredient("butter", 229.5));
            ingredients.Add(new Ingredient("flour", 124.9));
            ingredients.Add(new Ingredient("sifted flour", 99.37));
            ingredients.Add(new Ingredient("salt", 241.3));
            ingredients.Add(new Ingredient("baking powder", 179.8));
            ingredients.Add(new Ingredient("baking soda", 205.8));
            ingredients.Add(new Ingredient("sugar", 191.6));
            ingredients.Add(new Ingredient("powdered sugar", "confectioner's sugar", 130.1));
            ingredients.Add(new Ingredient("brown sugar", 220));
            ingredients.Add(new Ingredient("cornstarch", 151.4));
            ingredients.Add(new Ingredient("cornmeal", 170.3));
            ingredients.Add(new Ingredient("cocoa powder", 111.2));

            //this link has a lot of ingredient conversions
            //https://www.convert-me.com/en/convert/cooking/?u=v.cup&v=1&s=baking%20soda


            //index is the index of the list ingredients of the ingredient being used 
            int index =Interface();
            //Interface() is mostly used for Debugging/Testing
            //if Interface() is removed then a method should be created for passing  inputNum, inputUnit, and finding the index of the ingredeint 
            CalculateUnits(index);

            double[] values = { tsp, tbsp, cup, pint, quart, gallon, ounce, lbs, flOz, mL, gram, stick };
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine(measureType[i] + ":\t" + values[i]);
            }
            SimplifyVolume(values);
            Console.ReadLine();
        }

        static void ShowUnits()
        {
            Console.WriteLine("Units:");
            foreach (string s in measureType)
            {
                Console.WriteLine(s);
            }
            Console.Write("\t\tnote: stick is only used for butter");
        }
        static void ShowIngredients()
        {
            foreach (Ingredient i in ingredients)
            {
                Console.WriteLine(i.name);
            }
        }

        static void CalculateUnits(int index)
        {
            switch (enteredMeasureType)
            {
                case 0:
                    tsp = inputNum;
                    TspToTbsp();
                    break;
                case 1:
                    tbsp = inputNum;
                    TbspToTsp();
                    TbspToCups();
                    break;
                case 2:
                    cup = inputNum;
                    CupsToTbsp();
                    CupsToPints();
                    CupsToFloz();
                    if (butterUsed)
                    {
                        CupsToSticks();
                    }
                    break;
                case 3:
                    pint = inputNum;
                    PintsToCups();
                    PintsToQuarts();
                    break;
                case 4:
                    quart = inputNum;
                    QuartsToPints();
                    QuartsToGallons();
                    break;
                case 5:
                    gallon = inputNum;
                    GallonsToQuarts();
                    break;
                case 6:
                    ounce = inputNum;
                    OuncesToPounds();
                    OuncesToGrams();
                    break;
                case 7:
                    lbs = inputNum;
                    PoundsToOunces();
                    break;
                case 8:
                    flOz = inputNum;
                    FlozToCups();
                    FlozToMl();
                    break;
                case 9:
                    mL = inputNum;
                    MlToFloz();
                    break;
                case 10:
                    gram = inputNum;
                    GramsToOunces();
                    break;
                case 11:
                    stick = inputNum;
                    StickToCups();
                    break;
            }
            if (index != -1)
            {
                if (gram == 0)
                {
                    gram = ingredients[index].CalculateMass(mL);
                    GramsToOunces();
                }
                else
                {
                    mL = ingredients[index].CalculateVolume(gram);
                    MlToFloz();
                }
            }
        }

        static void TspToTbsp()
        {
            Console.WriteLine(tsp);
            tbsp = tsp / 3;
            TbspToCups();
        }
        static void TbspToCups()
        {
            cup = tbsp / 16;
            CupsToPints();
            CupsToFloz();
        }
        static void CupsToPints()
        {
            pint = cup / 2;
            PintsToQuarts();
        }
        static void PintsToQuarts()
        {
            quart = pint / 2;
            QuartsToGallons();
        }
        static void QuartsToGallons()
        {
            gallon = quart / 4;
        }
        static void OuncesToPounds()
        {
            lbs = ounce / 16;
        }

        static void TbspToTsp()
        {
            tsp = 3 * tbsp;
        }
        static void CupsToTbsp()
        {
            tbsp = cup * 16;
            TbspToTsp();
        }
        static void PintsToCups()
        {
            cup = pint * 2;
            CupsToTbsp();
            CupsToFloz();
        }
        static void QuartsToPints()
        {
            pint = quart * 2;
            PintsToCups();
        }
        static void GallonsToQuarts()
        {
            quart = gallon * 4;
            QuartsToPints();
        }
        static void PoundsToOunces()
        {
            ounce = lbs * 16;
            OuncesToGrams();
        }
        static void FlozToCups()
        {
            cup = flOz / 8;
            CupsToTbsp();
            CupsToPints();
            if (butterUsed)
            {
                CupsToSticks();
            }
        }
        static void CupsToFloz()
        {
            flOz = cup * 8;
            FlozToMl();
        }

        static void MlToFloz()
        {
            flOz = mL * .0338140227;
            FlozToCups();
            //1 mL = .0338140227 fl oz
        }
        static void FlozToMl()
        {
            mL = flOz / .0338140227;
        }

        static void GramsToOunces()
        {
            ounce = gram / 28.34952313;
            //28.34952313
            OuncesToPounds();
        }
        static void OuncesToGrams()
        {
            gram = ounce * 28.34952313;
        }

        static void CupsToSticks()
        {
            stick = cup * 2;
        }
        static void StickToCups()
        {
            cup = stick / 2;
            CupsToFloz();
            CupsToPints();
            CupsToTbsp();
        }

        static void SimplifyVolume(double[] array/*, int start*/)
        {
            simplified = "\n";
            int wholeNum = 0;
            double num = array[0];
            double remainder = 0;
            double[] whole = new double[array.Length];
            for (int i = 0; i < whole.Length; i++)
            {
                //if (i != 0)
                //{
                //    if (conversionRate[i - 1] == 0)
                //    {
                //        num = array[i];
                //    }
                //}

                if (i < conversionRate.Length)
                {
                    if (conversionRate[i] == 0)
                    {
                        wholeNum = (int)num;
                        remainder = num - (wholeNum * conversionRate[i]);
                        whole[i] = remainder;
                        num = array[i + 1];
                    }
                    else
                    {
                        wholeNum = (int)(num / conversionRate[i]);
                        remainder = num - (wholeNum * conversionRate[i]);
                        whole[i] = remainder;
                        num = wholeNum;
                    }
                }
                else
                {
                    whole[i] = num;
                }
            }

            //b is used in the end condition in a loop.
            //if the ingredient is not butter then the loop ends sooner and last index is skipped
            int b = 2;
            if (butterUsed)
            {
                b = 1;
            }
            for (int i = whole.Length - b; i >= 0; i--)
            {
                //the last 4 elements don't simplfy into any other units that is why  whole.Length - 5  is used
                if (i > whole.Length - 5)
                {
                    simplified += ((float)array[i] + " " + measureType[i] + "\tOR\t");
                }
                else
                {
                    //array[5] is gallon and array[6] is ounce so "or" is inserted to separate them out visually
                    if (i == 5)
                    {
                        simplified += "OR\t";
                    }
                    simplified += ((float)whole[i] + " " + measureType[i] + "\t");
                }
            }
            Console.WriteLine(simplified);
        }

        //asks the user for the amount the unit and ingredient being used and sets them to global variables
        static int Interface()
        {
            Console.WriteLine("Press 'M' to see how to type units. Press 'I' to view available ingredients. Or enter measurement in the format: number + ' ' + unit (+ ' ' + ingredient)");
            string temp = Console.ReadLine();
            while (temp == "")
            {
                Console.WriteLine("I'm not a mind reader!");
                Console.WriteLine("Press 'M' to see how to type units. Press 'I' to view available ingredients. Or enter measurement in the format: number + ' ' + unit (+ ' ' + ingredient)");
                temp = Console.ReadLine();
            }
            input = temp.Split(' ');
            while (input[0].ToLower() == "m" || input[0].ToLower() == "i")
            {
                if (input[0].ToLower() == "m")
                {
                    ShowUnits();
                }
                else
                {
                    ShowIngredients();
                }
                Console.WriteLine("Press 'M' to see how to type units. Press 'I' to view available ingredients. Or enter measurement in the format: number + ' ' + unit (+ ' ' + ingredient)");
                input = Console.ReadLine().Split(' ');
            }
            string[] fraction = input[0].Split('/');
            if (fraction.Length > 1)
            {
                inputNum = Convert.ToDouble(fraction[0]) / Convert.ToDouble(fraction[1]);
            }
            else
            {
                inputNum = Convert.ToDouble(input[0]);
            }
            if (input.Length == 1)
            {
                Console.WriteLine("You forgot the units. Please enter unit or Press 'M' to see how to type units.");
                inputUnit = Console.ReadLine().ToLower();
                while (inputUnit == "m")
                {
                    ShowUnits();
                    Console.WriteLine("Please enter unit.");
                    inputUnit = Console.ReadLine().ToLower();
                }
            }
            else
            {
                inputUnit = input[1].ToLower();
            }
            if (inputUnit == "ml")
            {
                inputUnit = "mL";
            }
            enteredMeasureType = Array.IndexOf(measureType, inputUnit);
            while (enteredMeasureType == -1)
            {
                Console.WriteLine("ERROR: The unit of {0} is not recognized. Please enter a different unit or Press 'M' to see how to type units.", inputUnit);
                inputUnit = Console.ReadLine().ToLower();
                while (inputUnit == "m")
                {
                    ShowUnits();
                    Console.WriteLine("Please enter unit.");
                    inputUnit = Console.ReadLine().ToLower();
                }
                enteredMeasureType = Array.IndexOf(measureType, inputUnit);
            }
            int index = -1;
            if (input.Length > 2)
            {
                string ing = input[2].ToLower();
                for (int i = 3; i < input.Length; i++)
                {
                    ing += (" " + input[i].ToLower());
                }

                index = ingredients.FindIndex(Ingredient => Ingredient.name == ing);
                if (input.Length > 2 && index == -1)
                {
                    do
                    {
                        Console.WriteLine("ERROR: {0} was not found. Press 'I' to see ingredients or Please enter a different ingredient or Press ENTER to skip.", ing);
                        ing = Console.ReadLine();
                        while (ing.ToLower() == "i")
                        {
                            ShowIngredients();
                            Console.WriteLine("Please enter ingredient or Press ENTER to skip.");
                            ing = Console.ReadLine().ToLower();
                        }
                        index = ingredients.FindIndex(Ingredient => Ingredient.name == ing);
                    } while (ing != "" && index == -1);
                }
                if (ing == "butter")
                {
                    butterUsed = true;
                }
                else if (inputUnit == "stick" && ing == "")
                {
                    butterUsed = true;
                }
                else if (inputUnit == "stick")
                {
                    Console.WriteLine("ERROR: The unit stick is only used with butter");
                    string reply;
                    do
                    {
                        Console.WriteLine("Use butter instead of {0}? Yes [Y] or No I want to enter a different unit [N]", ing);
                        reply = Console.ReadLine();
                        if (reply.ToUpper() == "Y")
                        {
                            butterUsed = true;
                            index = ingredients.FindIndex(Ingredient => Ingredient.name == "butter");
                        }
                        else if (reply.ToUpper() == "N")
                        {
                            Console.WriteLine("Please enter new unit or Press 'M' to see how to type units.");
                            inputUnit = Console.ReadLine().ToLower();
                            while (inputUnit == "m")
                            {
                                ShowUnits();
                                Console.WriteLine("Please enter unit.");
                                inputUnit = Console.ReadLine().ToLower();
                            }
                        }
                        else
                        {
                            Console.WriteLine("I don't understand. Enter 'Y' or 'N'.");
                        }
                    } while (reply.ToUpper() != "Y" && reply.ToUpper() != "N");
                }
            }
            else if (inputUnit == "stick")
            {
                butterUsed = true;
            }
            return index;
        }
    }
}
public class Ingredient
{
    public double volumeToMass;
    public string name;
    public string alternateName;
    double c = 236.588236512895;
    public Ingredient(string s)
    {
        name = s;
        volumeToMass = 1;
    }
    public Ingredient(string s, double d)
    {
        name = s;
        volumeToMass = d/c;
    }
    public Ingredient(string s, string aN, double d)
    {
        name = s;
        volumeToMass = d / c;
        alternateName = aN;
    }
    public double CalculateMass(double mL)
    {
        return mL * volumeToMass;
    }
    public double CalculateVolume(double g)
    {
        return g / volumeToMass;
    }
}
/*
 * 
 * 3 teaspoons = 1 tablespoon

4 tablespoons = 1/4 cup

5 tablespoons + 1 teaspoon = 1/3 cup

8 tablespoons = 1/2 cup

1 cup = 1/2 pint

2 cups = 1 pint

4 cups (2 pints) = 1 quart

4 quarts = 1 gallon

16 ounces = 1 pound

Dash or pinch = less than 1/8 teaspoon

Common Abbreviations

tsp = teaspoon

Tbsp = tablespoon

oz = ounce

pt = pint

qt = quart

gal = gallon

lb = pound

236.588236512895 ml in a cup

1 cup all-purpose flour	        5 ounces (148 grams)
1 cup cake flour	            4 1/2 ounces (133 grams)
1 cup bread flour	            5 1/2 ounces (163 grams)
1 cup granulated sugar	        7 ounces (207 grams)
1 cup confectioner's sugar	    4 ounces (118 grams)
1 cup brown sugar, packed	    7 ounces (207 grams)
1 cup cornstarch	            4 1/2 ounces (133 grams)
1 cup cornmeal	                5 ounces (148 grams)
1 cup cocoa powder	            3 ounces (89 grams)
 * */
