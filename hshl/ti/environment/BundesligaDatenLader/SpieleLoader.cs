using BundesligaDatenLader.Model;
using Npgsql;

namespace BundesligaDatenLader;

public class SpieleLoader
{
    private int season;
    private OpenLigaClient client;
    private NpgsqlConnection connection;

    public SpieleLoader(int season, string connection_string)
    {
        this.season = season;
        client = new OpenLigaClient();
        var dataSource = NpgsqlDataSource.Create(connection_string);
        connection = dataSource.OpenConnection();
    }

    public void Import()
    {
        var result = client.GetMatches("bl1", season);
        result.Wait();
        var matches = result.Result;
        if (matches.Count() == 0)
            return;

        Clear();
        Insert(matches, 1);

        result = client.GetMatches("bl2", season);
        result.Wait();
        Insert(result.Result, 2);
    }

    private void Clear()
    {
        try
        {
            var sql = "drop table spiele";
            var cmd = new NpgsqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Konnte Tabelle 'spiele' nicht löschen: {ex.ToString()}");
        }

        try
        {
            var sql = "create table spiele (" +
                "spiel_id serial primary key," +
                "spieltag int not null," +
                "datum timestamp not null," +
                "heim_team int not null," +
                "gast_team int not null," +
                "tore_heim int," +
                "tore_gast int)";
            var cmd = new NpgsqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Konnte Tabelle 'spiele' nicht anlegen: {ex.ToString()}");
        }
    }

    public void Insert(IEnumerable<Match> matches, int liga)
    {
        var sql = "insert into spiele (spiel_id, spieltag, datum, heim_team, gast_team, tore_heim, tore_gast) values " +
            "(:spiel_id, :spieltag, :datum, :heim_team, :gast_team, :tore_heim, :tore_gast)";

        foreach (var match in matches)
        {
            var cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue(":spiel_id", match.matchID);
            cmd.Parameters.AddWithValue(":spieltag", match.group.groupOrderID);
            cmd.Parameters.AddWithValue(":datum", match.matchDateTime);
            cmd.Parameters.AddWithValue(":heim_team", match.team1.teamId);
            cmd.Parameters.AddWithValue(":gast_team", match.team2.teamId);

            if (match.matchResults.Count() > 0)
            { 
                cmd.Parameters.AddWithValue(":tore_heim", match.matchResults.First().pointsTeam1);
                cmd.Parameters.AddWithValue(":tore_gast", match.matchResults.First().pointsTeam2);
            }
            else
            {
                cmd.Parameters.AddWithValue(":tore_heim", DBNull.Value);
                cmd.Parameters.AddWithValue(":tore_gast", DBNull.Value);
            }

            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"Daten für Spiele in Liga {liga} wurden geschrieben!");
    }
}
