using System;
using System.Collections.Generic;
using System.Linq;

namespace EnigmaMachine.Helpers
{
    /// <summary>
    /// Helps with the convertion and mutation of characters
    /// </summary>
    public static class CharacterConverter
    {
        private const int alphabeticLength = 26;

        /// <summary>
        /// Converts a character to it's corresponding index
        /// For example: A = 1, B = 2 etc.
        /// </summary>
        /// <param name="character">Character that needs to be converted</param>
        /// <returns>Corresponding index belonging to the input character</returns>
        public static int CharacterToNumber(char character)
        {
            if (!Char.IsLetter(character))
            {
                throw new ArgumentOutOfRangeException("character must be a letter");
            }

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

            if (index <= 0 || index > alphabeticLength)
            {
                throw new ArgumentOutOfRangeException("index must be between 1 and alphabeticLength");
            }

            char character = (char)('A' + (char)((index - 1) % alphabeticLength));

            return character;
        }

        /// <summary>
        /// Mutate a character through specific wiring in a forward direction
        /// </summary>
        /// <param name="input">Character that needs to be mutated</param>
        /// <param name="wiring">Wiring through which the character will pass</param>
        /// <returns>Mutated character</returns>
        public static char MutateForward(char input, Dictionary<char, char> wiring)
        {
            input = char.ToUpper(input);

            if (!wiring.ContainsKey(input))
            {
                return input;
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
        public static char MutateBackward(char input, Dictionary<char, char> wiring)
        {
            input = char.ToUpper(input);

            if (!wiring.ContainsValue(input))
            {
                return input;
            }

            char output = wiring.FirstOrDefault(x => x.Value == input).Key;

            return output;
        }
    }
}
