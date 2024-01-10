// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

int sum = 0;
var filename = "H:\\CodeKy\\Projects\\Sandbox\\input.txt";
// filename = "H:\\CodeKy\\Projects\\Sandbox\\test.txt";

Dictionary<int, string> text_to_number = new Dictionary<int, string>{
	{ 0, "Zero" },
	{ 1, "One" },
	{ 2, "Two" },
	{ 3, "Three" },
	{ 4, "Four" },
	{ 5, "Five" },
	{ 6, "Six" },
	{ 7, "Seven" },
	{ 8, "Eight" },
	{ 9, "Nine" }
};

Dictionary<int, string> text_to_number_invert = new Dictionary<int, string>(text_to_number);
foreach (var number in text_to_number_invert.Keys) text_to_number_invert[number] = new string(text_to_number_invert[number].ToCharArray().Reverse().ToArray());

foreach (string line in File.ReadLines(filename))
{
	Console.Write($"{line} => ");
	int first_digit = Fracking_Goblins(line, text_to_number);
	string invert = new string(line.ToCharArray().Reverse().ToArray());
	int second_digit = Fracking_Goblins(invert, text_to_number_invert);
	int number_to_add = first_digit * 10 + second_digit;
	Console.WriteLine(number_to_add);
	sum += number_to_add;
}

Console.WriteLine();
Console.WriteLine($"Sum: {sum}");
Debug.Assert(sum == 54597 || sum == 54504);
Environment.Exit(0);
// </main>



/* *********************************************************************** */
int Fracking_Goblins(string input, Dictionary<int, string> text_to_number)
{
	while (input.Length > 0) 
	{
		var chars = input.ToCharArray();
		int i;

		if ((i = (int)Char.GetNumericValue(chars[0])) >= 0) return i;
		else if ((i = StarsWithTextNumber(input, text_to_number)) >= 0) return i;	// Comment out this line for part 1
		else input = input.Substring(1);
	} 

	throw new Exception("No number found");
};



int StarsWithTextNumber(string input, Dictionary<int, string> text_to_number)
{
	foreach (var number in text_to_number.Keys)
	{
		if (input.StartsWith(text_to_number[number], StringComparison.OrdinalIgnoreCase))
		{
			return number;
		}
	}
	return -1;
}