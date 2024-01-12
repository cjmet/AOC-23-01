using System.Diagnostics;
// <Fix for Replit vs C#12>
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

class Program
{
	public static void Main(string[] args)
	{
		// </Fix for Replit vs C#12>


		int sum = 0;
		var filename = FindDefaultFile("input.txt");
		// filename =  FindDefaultFile("test.txt");
		Console.WriteLine($"Attempting to read {filename}");
        Console.WriteLine();

        Dictionary<int, string> textToNumber = new Dictionary<int, string>{
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

		Dictionary<int, string> textToNumberInvert = new Dictionary<int, string>(textToNumber);
		foreach (var number in textToNumberInvert.Keys) textToNumberInvert[number] = new string(textToNumberInvert[number].ToCharArray().Reverse().ToArray());

		foreach (string line in File.ReadLines(filename))
		{
			Console.Write($"{line} => ");
			int firstDigit = GetFirstNumber(line, textToNumber);
			string invert = new string(line.ToCharArray().Reverse().ToArray());
			int secondDigit = GetFirstNumber(invert, textToNumberInvert);
			int numberToAdd = firstDigit * 10 + secondDigit;
			Console.WriteLine(numberToAdd);
			sum += numberToAdd;
		}

		Console.WriteLine();
		Console.WriteLine($"Sum: {sum}");
		Debug.Assert(sum == 54597 || sum == 54504);
		Environment.Exit(0);
		// </main>


		/* *********************************************************************** */
		int GetFirstNumber(string input, Dictionary<int, string> textToNumberDictionary)
		{
			while (input.Length > 0)
			{
				int i;
				var chars = input.ToCharArray();

				if ((i = (int)Char.GetNumericValue(chars[0])) >= 0) return i;
				else if ((i = StartsWithTextNumber(input, textToNumberDictionary)) >= 0) return i;   // Comment out this line for part 1
				else input = input.Substring(1);
			}

			throw new Exception("No number found");
		};



		int StartsWithTextNumber(string input, Dictionary<int, string> textToNumberDictionary)
		{
			foreach (var number in textToNumberDictionary.Keys)
				if (input.StartsWith(textToNumberDictionary[number], StringComparison.OrdinalIgnoreCase))
					return number;

			return -1;
		}



		//find filename in parent direct directories or direct subdirectories
		string FindDefaultFile(string filename, SearchOption searchOption = SearchOption.AllDirectories)
		{
			string pushd = Directory.GetCurrentDirectory();
			string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), filename, searchOption);
			if (files.Length == 0)
			{
				Directory.SetCurrentDirectory("..");
				string value = FindDefaultFile(filename, SearchOption.TopDirectoryOnly);
				Directory.SetCurrentDirectory(pushd);
				return value;
			}
			else
			{
				Directory.SetCurrentDirectory(pushd);
				return files[0];
			}
		}


		// Fix for Replit vs C#12
	}
}
// </Fix for Replit vs C#12>
