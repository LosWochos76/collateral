using System;
using System.Linq;
using System.Collections.Generic;

namespace Bundesliga
{
    public class MatchList
    {
        public MatchList(List<Match> list) 
        {
            Matches = list;
        }

        public MatchList() 
        {
            Matches = new List<Match>();
        }

        public List<Match> Matches { get; set; }

        public MatchList ByGroupOrderID(int id) 
        {
            var result = new MatchList();
            foreach (var m in Matches)
                if (m.Group.GroupOrderID == id)
                    result.Matches.Add(m);

            return result;
        }

        public double AverageHomeGoals
        {
            get
            {
                int sum_home_goals = 0;
                int match_count = 0;

                foreach (var m in Matches)
                {
                    if (m.MatchIsFinished)
                    {
                        sum_home_goals += m.FinalResult.PointsTeam1;
                        match_count++;
                    }
                }

                return (double)sum_home_goals / match_count;
            }
        }
    }
}
