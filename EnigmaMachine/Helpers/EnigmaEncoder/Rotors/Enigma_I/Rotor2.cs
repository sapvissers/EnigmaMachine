namespace EnigmaMachine.Helpers.EnigmaEncoder.Rotors.Enigma_I
{
    public class Rotor2 : Rotor
    {
        public Rotor2()
        {
            Identifier = "II";

            wiring.Add('A', 'A');
            wiring.Add('B', 'J');
            wiring.Add('C', 'D');
            wiring.Add('D', 'K');
            wiring.Add('E', 'S');
            wiring.Add('F', 'I');
            wiring.Add('G', 'R');
            wiring.Add('H', 'U');
            wiring.Add('I', 'X');
            wiring.Add('J', 'B');
            wiring.Add('K', 'L');
            wiring.Add('L', 'H');
            wiring.Add('M', 'W');
            wiring.Add('N', 'T');
            wiring.Add('O', 'M');
            wiring.Add('P', 'C');
            wiring.Add('Q', 'Q');
            wiring.Add('R', 'G');
            wiring.Add('S', 'Z');
            wiring.Add('T', 'N');
            wiring.Add('U', 'P');
            wiring.Add('V', 'Y');
            wiring.Add('W', 'F');
            wiring.Add('X', 'V');
            wiring.Add('Y', 'O');
            wiring.Add('Z', 'E');

            notch = 'M';
        }
    }
}
