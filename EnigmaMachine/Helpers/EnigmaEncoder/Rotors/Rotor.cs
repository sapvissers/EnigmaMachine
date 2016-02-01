using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine.Helpers.EnigmaEncoder.Rotors
{
	public abstract class Rotor
	{
		public string Identifier { get; set; }

		protected Dictionary<char, char> wiring = new Dictionary<char, char>();

		protected char notch;

		protected int position;

		/// <summary>
		/// Set rotor position by char
		/// </summary>
		/// <param name="position"></param>
		public void setPosition(char position)
		{
			this.position = CharacterConverter.CharacterToNumber(position);
		}

		/// <summary>
		/// Set rotor position by index
		/// </summary>
		/// <param name="position"></param>
		public void setPosition(int position)
		{
			this.position = position;
			rotateToPostion();
        }

		/// <summary>
		/// Rotate rotor 1 turn
		/// </summary>
		/// <returns>returns whether or not the next rotor should be turned</returns>
		public bool rotate()
		{
			position++;

            if (position > wiring.Count)
			{
				position -= wiring.Count;
			}

			rotateToPostion();

			char positionLetter = CharacterConverter.NumberToCharacter(position);

			return positionLetter == notch;
		}

		/// <summary>
		/// Do mutation forward through the rotor
		/// </summary>
		/// <param name="input">Character to be mutated</param>
		/// <returns>Mutated character</returns>
		public char mutateForward(char input)
		{
			return CharacterConverter.mutateForward(input, wiring);
		}

		/// <summary>
		/// Do mutation backwards through the rotor
		/// </summary>
		/// <param name="input">Character to be mutated</param>
		/// <returns>Mutated character</returns>
		public char mutateBackward(char input)
		{
			return CharacterConverter.mutateBackward(input, wiring);
		}

		private void rotateToPostion()
		{
			Dictionary<char, char> originalWiring = new Dictionary<char, char>(wiring);

			for (int i = 0, wiringCount = wiring.Count; i < wiringCount; i++)
			{
				int indexNumber = i + position;

				while (indexNumber > wiringCount)
				{
					indexNumber -= wiringCount;
                }
				char originalIndex = CharacterConverter.NumberToCharacter(indexNumber);
				
				char newIndex = CharacterConverter.NumberToCharacter(i + 1);

				wiring[newIndex] = originalWiring[originalIndex];
			}
        }
	}
}