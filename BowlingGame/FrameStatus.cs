namespace BowlingGame
{
    public enum FrameStatus
    {
        // Frame ready for Bowler to roll
        Ready = 0,
        // Bowler played (1 or 2 attempts) but no awards
        Bowled = 1,
        // Bowler awared a spare
        Spare = 2,
        // Bowler awared a strike
        Strike = 3
    }
}
