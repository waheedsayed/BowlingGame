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
        public int DueBonus { get; private set; }  = 0;

        public void Roll(int numberOfPins)
        {
            // Roll and strike the given number of pin
            // Set score for current frame and set status
            if (numberOfPins < NoPins || numberOfPins > TenPins)
                throw new ArgumentOutOfRangeException(nameof(numberOfPins), "Acceptable range: [0-10]");
            // We can validate number of attempts per frame here but I won't - according to test instructions.

            if ((Status == FrameStatus.Strike || Status == FrameStatus.Spare) && DueBonus == 0)
                throw new InvalidOperationException("Cannot roll for this frame any more");

            Attempts++;
            Score += numberOfPins;

            if (CheckIfStrike() || CheckIfSpare())
                return;

            Status = FrameStatus.Bowled;
        }

        public void AddBonus(int numberOfPins)
        {
            // Add bonus score to this frame
            // Validate due bonus
            if (DueBonus == 0)
                throw new InvalidOperationException("Cannot add bonus while no bonus is due!");

            // Validate score <= 10 (per addition)
            if (numberOfPins < NoPins || numberOfPins > TenPins)
                throw new ArgumentOutOfRangeException(nameof(numberOfPins), "Acceptable range: [0-10]");

            DueBonus--;
            Score += numberOfPins;
        }

        private bool CheckIfStrike()
        {
            if (Status == FrameStatus.Ready && Score == TenPins && Attempts == FirstAttempt)
            {
                Status = FrameStatus.Strike;
                DueBonus = 2;
                return true;
            }

            return false;
        }

        private bool CheckIfSpare()
        {
            if (Status == FrameStatus.Bowled && Score == TenPins && Attempts == SecondAttempt)
            {
                Status = FrameStatus.Spare;
                DueBonus = 1;
                return true;
            }

            return false;
        }
    }
}
