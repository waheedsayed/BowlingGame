using System;
using System.Linq;

namespace BowlingGame
{
    public class Game
    {
        private const int TenFrames = 10;
        private const int TenPins = 10;
        private const int FirstAttempt = 1;
        private const int SecondAttempt = 2;

        private int ExtraDueRolls = 0;

        public int CurrentFrameNumber { get; private set; } = 1;
        public Frame[] Frames { get; private set; } = new Frame[TenFrames];
        public bool IsGameOver  { get; private set; } = false;
        public int Score => Frames.Sum(f => f.Score);

        public Frame CurrentFrame => this[CurrentFrameNumber];
        public Frame PreviousFrame => this[CurrentFrameNumber-1];
        public Frame PrePreviousFrame => this[CurrentFrameNumber-2];

        public Game()
        {
            // Initialise the 10 frames
            for (int i = 0; i < TenFrames; i++)
            {
                Frames[i] = new Frame();
            }
        }

        public Frame this[int index]
        {
            get {
                if (index - 1 < 0) return null;
                return Frames[index-1];
            }
        }

        public void Roll(int numberOfPins)
        {
            // Roll and strike the given number of pin
            // Decide to advance frame or not
            // Calculate score for current frame, and previous if applicable 
            // Validation: number <= 10
            // Validation: NoMorePlays is false

            if (IsGameOver)
                throw new InvalidOperationException("GAME OVER!");

            this[CurrentFrameNumber].Roll(numberOfPins);

            DistributeBonus(numberOfPins);

            if (CurrentFrameNumber < TenFrames)
            {
                CheckToAdvanceFrame(numberOfPins);
            }
            else
            {
                ClaimExtraRolls();
                CheckIfGameOver();
            }
        }

        private void CheckToAdvanceFrame(int numberOfPins)
        {
            if (numberOfPins == TenPins || CurrentFrame.Attempts == SecondAttempt)
                CurrentFrameNumber++;
        }

        private void DistributeBonus(int numberOfPins)
        {
            if (PreviousFrame?.DueBonus >= 1)
                PreviousFrame?.AddBonus(numberOfPins);

            if (PrePreviousFrame?.DueBonus >= 1)
                PrePreviousFrame?.AddBonus(numberOfPins);
        }

        private void ClaimExtraRolls()
        {
            if (CurrentFrame.Status == FrameStatus.Strike && CurrentFrame.Attempts == FirstAttempt)
                ExtraDueRolls = 2;

            if (CurrentFrame.Status == FrameStatus.Spare && CurrentFrame.Attempts == SecondAttempt)
                ExtraDueRolls = 1;
        }

        private void CheckIfGameOver()
        {
            if (--ExtraDueRolls < 0)
                IsGameOver = true;
        }
    }
}
