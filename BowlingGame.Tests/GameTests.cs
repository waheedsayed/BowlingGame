// No pins means bowler has knocked nothing of the pins
// A perfect game means the bowler has knocked all pins in every attemtp

using System;
using Xunit;
using BowlingGame;
using Shouldly;

namespace BowlingGame.Tests
{
    public class GameTests
    {
        [Fact]
        public void New_Game_has_Ten_frames_and_current_is_One()
        {
            var bowlingGame = new Game();

            bowlingGame.CurrentFrameNumber.ShouldBe(1);
            bowlingGame.Frames.Length.ShouldBe(10);
            bowlingGame.Score.ShouldBe(0);

            foreach(var frame in bowlingGame.Frames)
            {
                Assert.NotNull(frame);
            }
        }

        [Fact]
        public void Roll_a_no_pins()
        {
            var bowlingGame = new Game();

            bowlingGame.Roll(0);
            bowlingGame.Score.ShouldBe(0);
            bowlingGame.CurrentFrameNumber.ShouldBe(1);
        }

        [Fact]
        public void Roll_a_second_no_pins()
        {
            var bowlingGame = new Game();

            bowlingGame.Roll(0);
            bowlingGame.Roll(0);
            bowlingGame.Score.ShouldBe(0);
            bowlingGame.CurrentFrameNumber.ShouldBe(2);
        }

        [Fact]
        public void Roll_a_spare()
        {
            var bowlingGame = new Game();

            bowlingGame.Roll(5);
            bowlingGame.Roll(5);
            bowlingGame.Score.ShouldBe(10);
            bowlingGame.CurrentFrameNumber.ShouldBe(2);
        }


        [Fact]
        public void Roll_a_spare_and_bowl()
        {
            var bowlingGame = new Game();

            bowlingGame.Roll(7);
            bowlingGame.Roll(3);
            bowlingGame.Roll(4);
            bowlingGame.CurrentFrame.Score.ShouldBe(4);
            bowlingGame.PreviousFrame.Score.ShouldBe(14);
            bowlingGame.Roll(2);
            bowlingGame.Score.ShouldBe(20);
            bowlingGame.CurrentFrameNumber.ShouldBe(3);
        }

        [Fact]
        public void Roll_a_strike_and_2bowls()
        {
            var bowlingGame = new Game();

            bowlingGame.Roll(10);
            bowlingGame.Roll(3);
            bowlingGame.Roll(6);

            bowlingGame.CurrentFrameNumber.ShouldBe(3);
            bowlingGame.Score.ShouldBe(28);
            bowlingGame.CurrentFrame.Score.ShouldBe(0);
            bowlingGame.PreviousFrame.Score.ShouldBe(9);
            bowlingGame.PrePreviousFrame.Score.ShouldBe(19);
        }

        [Fact]
        public void Roll_2strike_and_2bowls()
        {
            var bowlingGame = new Game();

            bowlingGame.Roll(10);
            bowlingGame.Roll(10);
            bowlingGame.Roll(4);
            bowlingGame.Roll(2);

            bowlingGame.CurrentFrameNumber.ShouldBe(4);
            bowlingGame.Score.ShouldBe(46);
            bowlingGame.CurrentFrame.Score.ShouldBe(0);
            bowlingGame.PreviousFrame.Score.ShouldBe(6);
            bowlingGame.PrePreviousFrame.Score.ShouldBe(16);

            bowlingGame.CurrentFrame.Status.ShouldBe(FrameStatus.Ready);
            bowlingGame.PreviousFrame.Status.ShouldBe(FrameStatus.Bowled);
            bowlingGame.PrePreviousFrame.Status.ShouldBe(FrameStatus.Strike);
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
            Console.WriteLine($"Score of ten strikes: {bowlingGame.Score}");

            // two extra rolls
            bowlingGame.Roll(10);
            bowlingGame.Roll(10);
            Console.WriteLine($"Score after 2 extra rolls: {bowlingGame.Score}");

            bowlingGame.CurrentFrame.Attempts.ShouldBe(3); // The only exceptional time that you get 3 attempts
            bowlingGame.CurrentFrameNumber.ShouldBe(10); // This is the last frame
            bowlingGame.Score.ShouldBe(300); // Perfect game score

            Assert.True(bowlingGame.GameOver);
        }
    }
}