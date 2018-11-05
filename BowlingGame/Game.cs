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

    public class Frame
    {
        public int Score { get; private set;}
        public FrameStatus Status { get; private set; }

        public void Roll(int numberOfPins)
        {
            // Roll and strike the given number of pin
            // Set score for current frame and set status 
            throw new NotImplementedException(); 
        }

        public void AddBounsScore(int score)
        {
            // Add bonus score to this frame
            // Validation score <= 10 (per addition)
            throw new NotImplementedException(); 
        }
    }

    public enum FrameStatus
    {
        // Frame ready for Bowler to roll
        Ready = 1,
        // Bowler awared a spare
        Spare = 2,
        // Bowler awared a strike
        Strike = 3
    }
}
