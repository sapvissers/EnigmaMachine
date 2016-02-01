using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes.Enigma_I
{
	public class UkwB : UkwType
	{
		public UkwB()
		{
			Identifier = "UKW-B";

			wiring.Add('A', 'Y');
			wiring.Add('B', 'R');
			wiring.Add('C', 'U');
			wiring.Add('D', 'H');
			wiring.Add('E', 'Q');
			wiring.Add('F', 'S');
			wiring.Add('G', 'L');
			wiring.Add('H', 'D');
			wiring.Add('I', 'P');
			wiring.Add('J', 'X');
			wiring.Add('K', 'N');
			wiring.Add('L', 'G');
			wiring.Add('M', 'O');
			wiring.Add('N', 'K');
			wiring.Add('O', 'M');
			wiring.Add('P', 'I');
			wiring.Add('Q', 'E');
			wiring.Add('R', 'B');
			wiring.Add('S', 'F');
			wiring.Add('T', 'Z');
			wiring.Add('U', 'C');
			wiring.Add('V', 'W');
			wiring.Add('W', 'V');
			wiring.Add('X', 'J');
			wiring.Add('Y', 'A');
			wiring.Add('Z', 'T');
		}
	}
}
