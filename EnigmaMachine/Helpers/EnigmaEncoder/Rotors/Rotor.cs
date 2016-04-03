using System.Collections.Generic;

namespace EnigmaMachine.Helpers.EnigmaEncoder.Rotors
{
    public abstract class Rotor
    {
        public string Identifier { get; set; }

        protected Dictionary<char, char> wiring = new Dictionary<char, char>();

        private Dictionary<char, char> rotatedWiring;
        public Dictionary<char, char> RotatedWiring { get { return rotatedWiring; } }

        protected char notch;

        private int position;

        /// <summary>
        /// Set rotor position by char
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(char position)
        {
            this.position = CharacterConverter.CharacterToNumber(position);
            rotateToPostion();
        }

        /// <summary>
        /// Set rotor position by index
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(int position)
        {
            this.position = position;
            rotateToPostion();
        }

        /// <summary>
        /// Get current rotor position index
        /// </summary>
        /// <returns>Position</returns>
        public int GetPosition()
        {
            return position;
        }

        /// <summary>
        /// Rotate rotor 1 turn
        /// </summary>
        /// <returns>returns whether or not the next rotor should be turned</returns>
        public bool Rotate()
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
        public char MutateForward(char input)
        {
            return CharacterConverter.MutateForward(input, rotatedWiring);
        }

        /// <summary>
        /// Do mutation backwards through the rotor
        /// </summary>
        /// <param name="input">Character to be mutated</param>
        /// <returns>Mutated character</returns>
        public char MutateBackward(char input)
        {
            return CharacterConverter.MutateBackward(input, rotatedWiring);
        }

        private void rotateToPostion()
        {
            rotatedWiring = new Dictionary<char, char>(wiring);

            for (int i = 0, wiringCount = wiring.Count; i < wiringCount; i++)
            {
                rotateWire(i, wiringCount);
            }
        }

        private void rotateWire(int index, int wiringCount)
        {
            int indexNumber = index + position;

            while (indexNumber > wiringCount)
            {
                indexNumber -= wiringCount;
            }
            char originalIndex = CharacterConverter.NumberToCharacter(indexNumber);

            char newIndex = CharacterConverter.NumberToCharacter(index + 1);

            rotatedWiring[newIndex] = wiring[originalIndex];
        }
    }
}