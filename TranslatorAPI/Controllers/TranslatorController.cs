using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslatorAPI.Dictionaries;

namespace TranslatorAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TranslatorController : ControllerBase
	{
		public static Dictionary<char, string> t2mDict;
		public static Dictionary<string, char> m2tDict;

		public TranslatorController()
		{
			t2mDict = txt2morseDictionary.txt2morseDict;
			m2tDict = morse2txtDictionary.morse2txtDict;
		}

		[HttpGet("txt2morse")]
		public string txt2morse(string input)
		{
			string result = "";
			foreach (char c in input.ToUpper())
			{
				if (t2mDict.ContainsKey(c))
				{
					result += t2mDict[c] + " ";
				}
				else if (c == ' ')
				{
					result += "/ "; // Use '/' to separate words in Morse code
				}
			}
			return result.Trim();
		}

		[HttpGet("morse2txt")]
		public string morse2txt(string input)
		{
			string[] words = input.Split(new[] { " / " }, StringSplitOptions.None);
			string result = "";

			foreach (string word in words)
			{
				string[] characters = word.Split(' ');
				foreach (string character in characters)
				{
					if (m2tDict.ContainsKey(character))
					{
						result += m2tDict[character];
					}
				}
				result += " ";
			}

			return result.Trim();
		}
	}
}
