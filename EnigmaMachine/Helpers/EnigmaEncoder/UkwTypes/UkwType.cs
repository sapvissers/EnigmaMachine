using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes
{
	public abstract class UkwType
	{
		public string Identifier { get; set; }

		protected Dictionary<char, char> wiring = new Dictionary<char, char>();
		
		/// <summary>
		/// Mutates a character through the specified wiring
		/// </summary>
		/// <param name="input">Character to be mutated</param>
		/// <returns>Mutated character</returns>
		public char mutate(char input)
		{
			return CharacterConverter.MutateForward(input, wiring);
		}
	}
}
