using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine.Helpers
{
	/// <summary>
	/// Helps with the convertion and mutation of characters
	/// </summary>
	public static class CharacterConverter
	{
		/// <summary>
		/// Converts a character to it's corresponding index
		/// For example: A = 1, B = 2 etc.
		/// </summary>
		/// <param name="character">Character that needs to be converted</param>
		/// <returns>Corresponding index belonging to the input character</returns>
		public static int CharacterToNumber(char character)
		{
			int index = char.ToUpper(character) - 64;

			return index;
		}

		/// <summary>
		/// Converts a index into it's corresponding character
		/// For example: 1 = A, 2 = B etc.
		/// </summary>
		/// <param name="index">Index that needs to be converted</param>
		/// <returns>Corresponding character belonging to the input index</returns>
		public static char NumberToCharacter(int index)
		{
			char character = (char)('A' + (char)((index - 1) % 26));

			return character;
		}

		/// <summary>
		/// Mutate a character through specific wiring in a forward direction
		/// </summary>
		/// <param name="input">Character that needs to be mutated</param>
		/// <param name="wiring">Wiring through which the character will pass</param>
		/// <returns>Mutated character</returns>
		public static char mutateForward(char input, Dictionary<char, char> wiring)
		{
			if (!wiring.ContainsKey(input))
			{
				throw new Exception("No wiring found for the character " + input);
			}

			char output = wiring[input];

			return output;
		}

		/// <summary>
		/// Mutate a character through specific wiring in a backward direction
		/// </summary>
		/// <param name="input">Character that needs to be mutated</param>
		/// <param name="wiring">Wiring through which the character will pass</param>
		/// <returns>Mutated character</returns>
		public static char mutateBackward(char input, Dictionary<char, char> wiring)
		{
			if (!wiring.ContainsValue(input))
			{
				throw new Exception("No wiring found for the character " + input);
			}

			char output = wiring.FirstOrDefault(x => x.Value == input).Key;

			return output;
		}
	}
}
