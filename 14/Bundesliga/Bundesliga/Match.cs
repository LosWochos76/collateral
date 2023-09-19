public class Match
{
    public int matchID { get; set; }
    public DateTime matchDateTime { get; set; }
    public string timeZoneID { get; set; }
    public int leagueId { get; set; }
    public string leagueName { get; set; }
    public int leagueSeason { get; set; }
    public string leagueShortcut { get; set; }
    public DateTime matchDateTimeUTC { get; set; }
    public Group group { get; set; }
    public Team Team1 { get; set; }
    public Team Team2 { get; set; }
    public DateTime lastUpdateDateTime { get; set; }
    public bool matchIsFinished { get; set; }
    public Matchresult[] matchResults { get; set; }
    public Goal[] goals { get; set; }
    public object location { get; set; }
    public object numberOfViewers { get; set; }
}

public class Group
{
    public string groupName { get; set; }
    public int groupOrderID { get; set; }
    public int groupID { get; set; }
}

public class Team
{
    public int teamId { get; set; }
    public string teamName { get; set; }
    public string shortName { get; set; }
    public string teamIconUrl { get; set; }
    public object teamGroupName { get; set; }
}

public class Matchresult
{
    public int resultID { get; set; }
    public string resultName { get; set; }
    public int pointsTeam1 { get; set; }
    public int pointsTeam2 { get; set; }
    public int resultOrderID { get; set; }
    public int resultTypeID { get; set; }
    public string resultDescription { get; set; }
}

public class Goal
{
    public int goalID { get; set; }
    public int scoreTeam1 { get; set; }
    public int scoreTeam2 { get; set; }
    public int matchMinute { get; set; }
    public int goalGetterID { get; set; }
    public string goalGetterName { get; set; }
    public bool isPenalty { get; set; }
    public bool isOwnGoal { get; set; }
    public bool isOvertime { get; set; }
    public object comment { get; set; }
}
