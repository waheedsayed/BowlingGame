using System;

namespace BowlingGame
{
    public class Game
    {
        public Frame[] Frames { get; private set; }

        public void Start()
        {
            // Initialise 10 frames            
            throw new NotImplementedException(); 
        }

        public void Roll(int numberOfPins)
        {
            // Roll and strike the given number of pin
            // Decide to advance frame or not
            // Calculate score for current frame, and previous if applicable 
            // Validation: number <= 10
            throw new NotImplementedException(); 
        }

        public int Score()
        {
            // Return game total score
            throw new NotImplementedException(); 
        }
    }
}
