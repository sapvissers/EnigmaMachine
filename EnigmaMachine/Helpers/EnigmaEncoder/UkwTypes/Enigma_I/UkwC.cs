using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes.Enigma_I
{
	public class UkwC : UkwType
	{
		public UkwC()
		{
			Identifier = "UKW-C";

			wiring.Add('A', 'F');
			wiring.Add('B', 'V');
			wiring.Add('C', 'P');
			wiring.Add('D', 'J');
			wiring.Add('E', 'I');
			wiring.Add('F', 'A');
			wiring.Add('G', 'O');
			wiring.Add('H', 'Y');
			wiring.Add('I', 'E');
			wiring.Add('J', 'D');
			wiring.Add('K', 'R');
			wiring.Add('L', 'Z');
			wiring.Add('M', 'X');
			wiring.Add('N', 'W');
			wiring.Add('O', 'G');
			wiring.Add('P', 'C');
			wiring.Add('Q', 'T');
			wiring.Add('R', 'K');
			wiring.Add('S', 'U');
			wiring.Add('T', 'Q');
			wiring.Add('U', 'S');
			wiring.Add('V', 'B');
			wiring.Add('W', 'N');
			wiring.Add('X', 'M');
			wiring.Add('Y', 'H');
			wiring.Add('Z', 'L');
		}
	}
}
