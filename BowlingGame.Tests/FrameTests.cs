using System;
using Shouldly;
using Xunit;

namespace BowlingGame.Tests
{
    public class FrameTests
    {
        [Fact]
        public void New_Frame_Status_is_Ready_and_Score_and_Attempts_are_Zeros()
        {
            var frame = new Frame();
            frame.Status.ShouldBe(FrameStatus.Ready);
            frame.Score.ShouldBe(0);
            frame.Attempts.ShouldBe(0);
        }

        [Fact]
        public void Roll_accepts_from_Zero_to_Ten_pins()
        {
            for (int numberOfPins = 0; numberOfPins <= 10; numberOfPins++)
            {
                var frame = new Frame();
                frame.Roll(numberOfPins);
                frame.Score.ShouldBe(numberOfPins);
            }
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

            exception.Message.ShouldBe("Acceptable range: [0-10]\nParameter name: numberOfPins");
            frame.Status.ShouldBe(FrameStatus.Ready);
            frame.Score.ShouldBe(0);
        }

        [Fact]
        public void Status_is_Bowled_after_1st_attempt_score_less_than_Ten()
        {
            for (int numberOfPins = 0; numberOfPins <= 9; numberOfPins++)
            {
                var frame = new Frame();
                frame.Roll(numberOfPins);
                frame.Attempts.ShouldBe(1);
                frame.Status.ShouldBe(FrameStatus.Bowled);
            }
        }

        [Fact]
        public void Status_is_Bowled_after_2nd_attempt_score_less_than_Ten()
        {
            for (int numberOfPins = 0; numberOfPins <= 9; numberOfPins++)
            {
                var frame = new Frame();
                frame.Roll(9 - numberOfPins);
                frame.Roll(numberOfPins);
                frame.Attempts.ShouldBe(2);
                frame.Status.ShouldBe(FrameStatus.Bowled);
            }
        }

        [Fact]
        public void Status_is_Strike_after_1st_attempt_and_Score_is_Ten_and_Two_DueBonus()
        {
            var frame = new Frame();
            frame.Roll(10);
            frame.Status.ShouldBe(FrameStatus.Strike);
            frame.Score.ShouldBe(10);
            frame.DueBonus.ShouldBe(2);
        }

        [Fact]
        public void Status_is_Spare_after_2nd_attempt_Score_is_Ten_and_One_DueBonus()
        {
            for (int numberOfPins = 1; numberOfPins <= 10; numberOfPins++)
            {
                var frame = new Frame();
                frame.Roll(10 - numberOfPins);
                frame.Roll(numberOfPins);
                frame.Score.ShouldBe(10);
                frame.Status.ShouldBe(FrameStatus.Spare);
                frame.DueBonus.ShouldBe(1);
            }
        }
    }
}