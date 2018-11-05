using System;
using Xunit;
using BowlingGame;

namespace BowlingGame.Tests
{
    public class FrameTests
    {
        [Fact]
        public void Status_is_ready_before_rolling()
        {
            var frame = new Frame();
            Assert.Equal(FrameStatus.Ready, frame.Status);
        }

        [Fact]
        public void Score_is_Zero_before_rolling()
        {
            var frame = new Frame();
            Assert.Equal(FrameStatus.Ready, frame.Status);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        public void Roll_accepts_from_Zero_to_Ten_pins1(int numberOfPins)
        {
            var frame = new Frame();
            frame.Roll(numberOfPins);
            Assert.Equal(numberOfPins, frame.Score);
            Assert.Equal(FrameStatus.Bowled, frame.Status);
        }

        [Theory]
        [InlineData(10)]
        public void Roll_accepts_from_Zero_to_Ten_pins2(int numberOfPins)
        {
            var frame = new Frame();
            frame.Roll(numberOfPins);
            Assert.Equal(numberOfPins, frame.Score);
            Assert.Equal(FrameStatus.Strike, frame.Status);
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(-1)]
        [InlineData(11)]
        [InlineData(17)]
        public void Roll_rejects_numbers_out_of_acceptable_range(int numberOfPins)
        {
            var frame = new Frame();

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => frame.Roll(numberOfPins)); 

            Assert.Equal("Acceptable range: [0-10]\nParameter name: numberOfPins", exception.Message);
            Assert.Equal(FrameStatus.Ready, frame.Status);
            Assert.Equal(0, frame.Score);
        }

        [Fact]
        public void Roll_a_Strike_from_first_attempt()
        {
            var frame = new Frame();
            frame.Roll(10);
            Assert.Equal(FrameStatus.Strike, frame.Status);
            Assert.Equal(10, frame.Score);
        }

        [Fact]
        public void Roll_a_Spare_from_second_attempt()
        {
            var frame = new Frame();
            frame.Roll(5);
            frame.Roll(5);
            Assert.Equal(FrameStatus.Spare, frame.Status);
            Assert.Equal(10, frame.Score);
        }
    }
}