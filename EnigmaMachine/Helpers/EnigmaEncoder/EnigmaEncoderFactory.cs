using System;
using System.Collections.Generic;
using EnigmaMachine.Helpers.EnigmaEncoder.Rotors;
using EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes;
using EnigmaMachine.Helpers.EnigmaEncoder.EnigmaEncoders;
using EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes.Enigma_I;
using EnigmaMachine.Helpers.EnigmaEncoder.Rotors.Enigma_I;

namespace EnigmaMachine.Helpers.EnigmaEncoder
{
	public static class EnigmaEncoderFactory
	{
		/// <summary>
		/// Creates the requested enigma encoder.
		/// </summary>
		/// <param name="enigmaEncoder">Type of enigma encoder</param>
		/// <param name="ukwType">Type of UKW</param>
		/// <param name="rotors">Type of Rotor</param>
		/// <param name="rotorPositions">Position of the rotor</param>
		/// <param name="plugboard">Any plugboard modifications</param>

		public static IEnigmaEncoder NewEnigmaEncoder(string enigmaEncoderType, string uwkTypeName, string[] rotorNames,
			int[] rotorPositions, Dictionary<char, char> plugboard = null)
		{
			if (enigmaEncoderType == null)
			{
				throw new NullReferenceException("enigmaEncoderType can't be null");
			}

			if (uwkTypeName == null)
			{
				throw new NullReferenceException("uwkTypeName can't be null");
			}

			if (rotorNames == null)
			{
				throw new NullReferenceException("rotorNames can't be null");
			}

			if (rotorPositions == null)
			{
				throw new NullReferenceException("rotorPositions can't be null");
			}

			if (plugboard == null)
			{
				plugboard = new Dictionary<char, char>();
			}

			if (rotorNames.Length != rotorPositions.Length)
			{
				throw new Exception("The amount of rotors must be equal to the amount of rotorPositions");
			}

			// Select UKW Type
			UkwType ukwType = selectUkwType(uwkTypeName);

			// Select rotors
			List<Rotor> rotorList = new List<Rotor>();
			addRotorsToRotorList(ref rotorList, rotorNames);
			Rotor[] rotors = rotorList.ToArray();

			// Set starting positions of the rotors
			for (int i = 0, amountOfRotors = rotors.Length; i < amountOfRotors; i++)
			{
				rotors[i].SetPosition(rotorPositions[i]);
			}

			// Select Enigma Encoder
			IEnigmaEncoder enigmaEncoder = selectEnigmaEncoder(enigmaEncoderType, ukwType, rotors, rotorPositions, plugboard);

			return enigmaEncoder;
		}

		private static UkwType selectUkwType(string uwkTypeName)
		{
			UkwType ukwType = null;

			switch (uwkTypeName)
			{
				case "UKW-A":
					ukwType = new UkwA();
					break;
				case "UKW-B":
					ukwType = new UkwB();
					break;
				case "UKW-C":
					ukwType = new UkwC();
					break;
				default:
					throw new ArgumentException("Unknown UKW type: " + uwkTypeName);
			}

			return ukwType;
		}

		private static void addRotorsToRotorList(ref List<Rotor> rotorList, string[] rotorNames)
		{
			for (int i = 0; i < rotorNames.Length; i++)
			{
				switch (rotorNames[i])
				{
					case "I":
						rotorList.Add(new Rotor1());
						break;
					case "II":
						rotorList.Add(new Rotor2());
						break;
					case "III":
						rotorList.Add(new Rotor3());
						break;
					case "IV":
						rotorList.Add(new Rotor4());
						break;
					case "V":
						rotorList.Add(new Rotor5());
						break;
					default:
						throw new ArgumentException("Unknown rotor name: " + rotorNames[i]);
				}
			}
		}

		private static IEnigmaEncoder selectEnigmaEncoder(string enigmaEncoderType, UkwType ukwType, Rotor[] rotors, 
			int[] rotorPositions, Dictionary<char, char>plugboard)
		{
			IEnigmaEncoder enigmaEncoder = null;

			switch (enigmaEncoderType)
			{
				case "Enigma_I":
					enigmaEncoder = new Enigma_I(ukwType, rotors, rotorPositions, plugboard);
					break;
				default:
					throw new ArgumentException("Unknown Enigma encoder type: " + enigmaEncoderType);
			}

			return enigmaEncoder;
		}
	}
}
