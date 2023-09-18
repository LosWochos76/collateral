using System;
using System.Collections.Generic;

namespace Bundesliga
{
    public class Match
    {
        public Match()
        {
            MatchResults = new List<MatchResult>();
        }

        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public List<MatchResult> MatchResults { get; set; }
        public DateTime MatchDateTime { get; set; }
        public int MatchID { get; set; }
        public bool MatchIsFinished { get; set; }
        public Group Group { get; set; }

        public MatchResult FinalResult 
        {
            get 
            {
                foreach (var m in MatchResults)
                    if (m.ResultTypeID == 2)
                        return m;

                return null;
            }
        }
    }
}
