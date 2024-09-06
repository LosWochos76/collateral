public partial class Match
{
    public long MatchId { get; set; }
    public DateTimeOffset MatchDateTime { get; set; }
    public string TimeZoneId { get; set; }
    public long LeagueId { get; set; }
    public string LeagueName { get; set; }
    public long LeagueSeason { get; set; }
    public string LeagueShortcut { get; set; }
    public DateTimeOffset MatchDateTimeUtc { get; set; }
    public Group Group { get; set; }
    public Team Team1 { get; set; }
    public Team Team2 { get; set; }
    public DateTimeOffset LastUpdateDateTime { get; set; }
    public bool MatchIsFinished { get; set; }
    public MatchResult[] MatchResults { get; set; }
    public Goal[] Goals { get; set; }
    public object Location { get; set; }
    public object NumberOfViewers { get; set; }
}

public partial class Goal
{
    public long GoalId { get; set; }
    public long ScoreTeam1 { get; set; }
    public long ScoreTeam2 { get; set; }
    public long GoalGetterId { get; set; }
    public string GoalGetterName { get; set; }
    public bool IsPenalty { get; set; }
    public bool IsOwnGoal { get; set; }
    public bool IsOvertime { get; set; }
    public object Comment { get; set; }
}

public partial class Group
{
    public string GroupName { get; set; }
    public long GroupOrderId { get; set; }
    public long GroupId { get; set; }
}

public partial class MatchResult
{
    public long ResultId { get; set; }
    public long PointsTeam1 { get; set; }
    public long PointsTeam2 { get; set; }
    public long ResultOrderId { get; set; }
    public long ResultTypeId { get; set; }
    public string ResultDescription { get; set; }
}

public partial class Team
{
    public long TeamId { get; set; }
    public string TeamName { get; set; }
    public string ShortName { get; set; }
    public Uri TeamIconUrl { get; set; }
    public object TeamGroupName { get; set; }
}
