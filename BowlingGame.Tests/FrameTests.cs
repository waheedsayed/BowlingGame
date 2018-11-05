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
    }
}
