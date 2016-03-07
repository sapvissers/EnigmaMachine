using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnigmaMachine.Helpers.EnigmaEncoder.Rotors;
using EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes;
using EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes.Enigma_I;
using EnigmaMachine.Helpers.EnigmaEncoder.Rotors.Enigma_I;
using System.Collections.ObjectModel;

namespace EnigmaMachine.Helpers.EnigmaEncoder.EnigmaEncoders
{
	public class Enigma_I : IEnigmaEncoder
	{
		private ObservableCollection<string> availableUkwTypes;
		private ObservableCollection<string> availableRotors;

		private UkwType ukwType;
		private Rotor[] rotors;
		private int[] rotorPositions;
		private Dictionary<char, char> plugboard;


		/// <summary>
		/// Constructor without any encoder data
		/// </summary>
		public Enigma_I()
		{
			fillRotorsAndUkwTypes();
		}

		/// <summary>
		/// Constructor, setting up the encoder
		/// </summary>
		/// <param name="ukwType">Selected UKW Type</param>
		/// <param name="rotors">Selected rotors</param>
		/// <param name="rotorPositions">Starting position of each rotor</param>
		/// <param name="plugboard">All plugboard modifications</param>
		public Enigma_I(UkwType ukwType, Rotor[] rotors, int[] rotorPositions, Dictionary<char, char> plugboard)
		{
			if (ukwType == null)
			{
				throw new NullReferenceException("ukwType can't be null");
			}

			if (rotors == null)
			{
				throw new NullReferenceException("rotors can't be null");
			}

			if (rotorPositions == null)
			{
				throw new NullReferenceException("rotorPositions can't be null");
			}

			if (plugboard == null)
			{
				throw new NullReferenceException("plugboard can't be null");
			}

			this.ukwType = ukwType;
			this.rotors = rotors;
			this.rotorPositions = rotorPositions;
			this.plugboard = plugboard;

			fillRotorsAndUkwTypes();
		}

		#region getters
		/// <summary>
		/// Returns all available UKW Types for this encoder
		/// </summary>
		/// <returns>Available UKW Types</returns>
		public ObservableCollection<string> GetAvailableUkwTypes()
		{
			return availableUkwTypes;
		}

		/// <summary>
		/// Returns all available rotors for this encoder
		/// </summary>
		/// <returns>Available rotors</returns>
		public ObservableCollection<string> GetAvailableRotors()
		{
			return availableRotors;
		}

		/// <summary>
		/// Returns UWK Type for this encoder
		/// </summary>
		/// <returns>UKW Type</returns>
		public UkwType GetUkwType()
		{
			return ukwType;
		}

		/// <summary>
		/// Returns selected rotors for this encoder
		/// </summary>
		/// <returns>Selected rotors</returns>
		public Rotor[] GetRotors()
		{
			return rotors;
		}

		/// <summary>
		/// Returns Positions of the selected rotors for this encoder
		/// </summary>
		/// <returns>Positions of the selected rotors</returns>
		public int[] GetRotorPositions()
		{
			return rotorPositions;
		}

		/// <summary>
		/// Returns Plugboard combinations for this encoder
		/// </summary>
		/// <returns>Plugboard combinations</returns>
		public Dictionary<char, char> GetPlugboard()
		{
			return plugboard;
		}
		#endregion

		/// <summary>
		/// The entire encoding process
		/// </summary>
		/// <param name="input">String that needs encoding</param>
		/// <returns>Encoded string</returns>
		public string Encode(string input)
		{
			if(input == null || input == "")
			{
				return "";
			}

			char[] inputCharacters = input.ToCharArray();

			int amountOfCharacters = inputCharacters.Length;

			char[] outputCharacters = new char[amountOfCharacters];

			for (int i = 0; i < amountOfCharacters; i++)
			{
				outputCharacters[i] = mutateCharacter(inputCharacters[i]);
            }

			string output = new string(outputCharacters);

			return output;
		}

		/// <summary>
		/// Mutates a character through the plugboard, rotors, UKW and back again
		/// </summary>
		/// <param name="inputCharacter">Character to be mutated</param>
		/// <returns>Mutated character</returns>
		private char mutateCharacter(char inputCharacter)
		{
			char outputCharacter = inputCharacter;

			// If character is not a letter, return unmutated value
			if (!Char.IsLetter(outputCharacter))
			{
				return outputCharacter;
            }

			// Character should always be upper case
			outputCharacter = Char.ToUpper(outputCharacter);

			int amountOfRotors = rotors.Length;

			// Rotate rotor(s)
			int rotorIndex = 0;
			bool turnNextRotor;

            do
			{
				turnNextRotor = rotors[rotorIndex].Rotate();
				rotorIndex++;
            } while (turnNextRotor && rotorIndex < amountOfRotors);

			// Pass through plugboard
			if (plugboard.ContainsKey(outputCharacter))
			{
				outputCharacter = plugboard[outputCharacter];
			}

			// Pass through rotors
			for (int i = 0; i < amountOfRotors; i++)
			{
				outputCharacter = rotors[i].MutateForward(outputCharacter);
            }

			// Pass through UKW
			outputCharacter = ukwType.mutate(outputCharacter);

			// Pass through rotors in reverse
			for (int i = amountOfRotors - 1; i >= 0; i--)
			{
				outputCharacter = rotors[i].MutateBackward(outputCharacter);
			}

			// Pass through plugboard in reverse
			if (plugboard.ContainsValue(outputCharacter))
			{
				outputCharacter = plugboard.FirstOrDefault(x => x.Value == outputCharacter).Key;
			}

			return outputCharacter;
		}

		private void fillRotorsAndUkwTypes()
		{
			availableUkwTypes = new ObservableCollection<string>();
			availableUkwTypes.Add("UKW-A");
			availableUkwTypes.Add("UKW-B");
			availableUkwTypes.Add("UKW-C");

			availableRotors = new ObservableCollection<string>();
			availableRotors.Add("I");
			availableRotors.Add("II");
			availableRotors.Add("III");
			availableRotors.Add("IV");
			availableRotors.Add("V");
		}
	}
}
