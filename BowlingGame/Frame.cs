using System;

namespace BowlingGame
{
    public class Frame
    {
        private const int NoPins = 0;
        private const int TenPins = 10;
        private const int FirstAttempt = 1;
        private const int SecondAttempt = 2;

        public int Attempts { get; private set;} = 0; 
        public int Score { get; private set;} = 0;
        public FrameStatus Status { get; private set; } = FrameStatus.Ready;

        public void Roll(int numberOfPins)
        {
            // Roll and strike the given number of pin
            // Set score for current frame and set status
            if (numberOfPins < NoPins || numberOfPins > TenPins)
                throw new ArgumentOutOfRangeException(nameof(numberOfPins), "Acceptable range: [0-10]");
            // We can validate number of attempts per frame here but I won't - according to test instructions.

            Attempts++;
            Score += numberOfPins;

            if (Score == TenPins && Attempts == FirstAttempt)
                Status = FrameStatus.Strike;
            else if (Score == TenPins && Attempts == SecondAttempt)
                Status = FrameStatus.Spare;
            else
                Status = FrameStatus.Bowled;
        }

        public void AddBonusScore(int score)
        {
            // Add bonus score to this frame
            // Validation score <= 10 (per addition)
            throw new NotImplementedException(); 
        }
    }
}
