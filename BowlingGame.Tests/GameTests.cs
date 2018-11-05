using System;
using Xunit;
using BowlingGame;

namespace BowlingGame.Tests
{
    public class GameTests
    {
        [Fact]
        public void Game_has_Ten_frames_and_current_is_One()
        {
            var bowlingGame = new Game();

            Assert.Equal(1, bowlingGame.CurrentFrameNumber);
            Assert.Equal(10, bowlingGame.Frames.Length);
            Assert.Equal(0, bowlingGame.Score);

            foreach(var frame in bowlingGame.Frames)
            {
                Assert.NotNull(frame);
            }
        }

        [Fact]
        public void Roll_a_NoPins_bowl()
        {
            var bowlingGame = new Game();

            bowlingGame.Roll(0);
            Assert.Equal(0, bowlingGame.Score);
            Assert.Equal(1, bowlingGame.CurrentFrameNumber);
            Assert.Equal(0, bowlingGame.CurrentFrame.Score);
            Assert.Equal(1, bowlingGame.CurrentFrame.Attempts);
        }

        [Fact]
        public void Roll_a_second_NoPins_bowl()
        {
            var bowlingGame = new Game();

            bowlingGame.Roll(0);
            bowlingGame.Roll(0);
            Assert.Equal(0, bowlingGame.Score);
            Assert.Equal(2, bowlingGame.CurrentFrameNumber);
            Assert.Equal(0, bowlingGame.CurrentFrame.Score);
            Assert.Equal(0, bowlingGame.PreviousFrame.Score);
            Assert.Equal(2, bowlingGame.PreviousFrame.Attempts);
        }

        [Fact]
        public void Roll_a_spare()
        {
            var bowlingGame = new Game();

            bowlingGame.Roll(5);
            bowlingGame.Roll(5);
            Assert.Equal(10, bowlingGame.Score);
            Assert.Equal(2, bowlingGame.CurrentFrameNumber);
            Assert.Equal(0, bowlingGame.CurrentFrame.Score);
            Assert.Equal(10, bowlingGame.PreviousFrame.Score);
            Assert.Equal(2, bowlingGame.PreviousFrame.Attempts);
        }


        [Fact]
        public void Roll_a_spare_and_bowl()
        {
            var bowlingGame = new Game();

            bowlingGame.Roll(7);
            bowlingGame.Roll(3);
            bowlingGame.Roll(4);
            Assert.Equal(4, bowlingGame.CurrentFrame.Score);
            Assert.Equal(1, bowlingGame.CurrentFrame.Attempts);
            Assert.Equal(14, bowlingGame.PreviousFrame.Score);
            Assert.Equal(2, bowlingGame.PreviousFrame.Attempts);
            bowlingGame.Roll(2);
            Assert.Equal(20, bowlingGame.Score);
            Assert.Equal(3, bowlingGame.CurrentFrameNumber);
        }

        [Fact]
        public void Roll_a_strike_and_2bowls()
        {
            var bowlingGame = new Game();

            bowlingGame.Roll(10);
            bowlingGame.Roll(3);
            bowlingGame.Roll(6);

            Assert.Equal(3, bowlingGame.CurrentFrameNumber);
            Assert.Equal(28, bowlingGame.Score);
            Assert.Equal(0, bowlingGame.CurrentFrame.Score);
            Assert.Equal(9, bowlingGame.PreviousFrame.Score);
            Assert.Equal(19, bowlingGame.BeforePreviousFrame.Score);

            Assert.Equal(0, bowlingGame.CurrentFrame.Attempts);
            Assert.Equal(2, bowlingGame.PreviousFrame.Attempts);
            Assert.Equal(1, bowlingGame.BeforePreviousFrame.Attempts);

            Assert.Equal(FrameStatus.Ready, bowlingGame.CurrentFrame.Status);
            Assert.Equal(FrameStatus.Bowled, bowlingGame.PreviousFrame.Status);
            Assert.Equal(FrameStatus.Strike, bowlingGame.BeforePreviousFrame.Status);
        }

        [Fact]
        public void Roll_2strike_and_2bowls()
        {
            var bowlingGame = new Game();

            bowlingGame.Roll(10);
            bowlingGame.Roll(10);
            bowlingGame.Roll(4);
            bowlingGame.Roll(2);

            Assert.Equal(4, bowlingGame.CurrentFrameNumber);
            Assert.Equal(46, bowlingGame.Score);
            Assert.Equal(0, bowlingGame.CurrentFrame.Score);
            Assert.Equal(6, bowlingGame.PreviousFrame.Score);
            Assert.Equal(16, bowlingGame.BeforePreviousFrame.Score);

            Assert.Equal(0, bowlingGame.CurrentFrame.Attempts);
            Assert.Equal(2, bowlingGame.PreviousFrame.Attempts);
            Assert.Equal(1, bowlingGame.BeforePreviousFrame.Attempts);

            Assert.Equal(FrameStatus.Ready, bowlingGame.CurrentFrame.Status);
            Assert.Equal(FrameStatus.Bowled, bowlingGame.PreviousFrame.Status);
            Assert.Equal(FrameStatus.Strike, bowlingGame.BeforePreviousFrame.Status);
        }

        [Fact]
        public void Roll_a_perfect_game()
        {
            var bowlingGame = new Game();

            // ten strikes
            bowlingGame.Roll(10);
            bowlingGame.Roll(10);
            bowlingGame.Roll(10);
            bowlingGame.Roll(10);
            bowlingGame.Roll(10);
            bowlingGame.Roll(10);
            bowlingGame.Roll(10);
            bowlingGame.Roll(10);
            bowlingGame.Roll(10);
            bowlingGame.Roll(10);
            Console.WriteLine($"Score: {bowlingGame.Score}");

            // two extra rolls
            bowlingGame.Roll(10);
            Console.WriteLine($"Score: {bowlingGame.Score}");
            bowlingGame.Roll(10);
            Console.WriteLine($"Score: {bowlingGame.Score}");

            Assert.True(bowlingGame.NoMorePlays);
            Assert.Equal(3, bowlingGame.CurrentFrame.Attempts);
            Assert.Equal(10, bowlingGame.CurrentFrameNumber);
            Assert.Equal(300, bowlingGame.Score);
        }
    }
}