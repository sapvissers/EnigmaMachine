using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine.Helpers.EnigmaEncoder.Rotors.Enigma_I
{
	public class Rotor4 : Rotor
	{
		public Rotor4()
		{
			Identifier = "IV";

			wiring.Add('A', 'E');
			wiring.Add('B', 'S');
			wiring.Add('C', 'O');
			wiring.Add('D', 'V');
			wiring.Add('E', 'P');
			wiring.Add('F', 'Z');
			wiring.Add('G', 'J');
			wiring.Add('H', 'A');
			wiring.Add('I', 'Y');
			wiring.Add('J', 'Q');
			wiring.Add('K', 'U');
			wiring.Add('L', 'I');
			wiring.Add('M', 'R');
			wiring.Add('N', 'H');
			wiring.Add('O', 'X');
			wiring.Add('P', 'L');
			wiring.Add('Q', 'N');
			wiring.Add('R', 'F');
			wiring.Add('S', 'T');
			wiring.Add('T', 'G');
			wiring.Add('U', 'K');
			wiring.Add('V', 'D');
			wiring.Add('W', 'C');
			wiring.Add('X', 'M');
			wiring.Add('Y', 'W');
			wiring.Add('Z', 'B');

			notch = 'R';
		}
	}
}
