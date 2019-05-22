// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Report a score to a UnnyNet LeaderBoard")]
    public class UnnyNetReportLeaderboard: UnnyNetBaseAction
    {
        [Tooltip("The Leaderboard Name")]
        public FsmString leaderboardName;

        [Tooltip("The Score")]
        public FsmFloat score;

        [Tooltip("The Score as a string, must be a valid float")]
        public FsmString scoreAsString;

        [ActionSection("Result")]
        [Tooltip("true if report succeeded")]
        [UIHint(UIHint.Variable)]
        public FsmBool success;

        public override void Reset()
        {
            base.Reset();
            leaderboardName = null;
            score = null;
            scoreAsString = new FsmString() { UseVariable = true };
            success = null;
        }

        public override void OnEnter()
        {
            float _score;

            if (!string.IsNullOrEmpty(scoreAsString.Value))
            {
                if (!float.TryParse(scoreAsString.Value, out _score))
                {
                    LogError("Score as string failed to parse as a float");
                }
            }else{
                _score = score.Value;
            }

            UnnyNet.UnnyNet.ReportLeaderboards(leaderboardName.Value, _score, BaseCallback);
        }
    }
}