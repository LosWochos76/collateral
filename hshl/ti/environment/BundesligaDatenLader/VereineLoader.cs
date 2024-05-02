using BundesligaDatenLader.Model;
using Npgsql;

namespace BundesligaDatenLader;

public class VereineLoader
{
    private int season;
    private OpenLigaClient client;
    private NpgsqlConnection connection;

    public VereineLoader(int season, string connection_string) 
    {
        this.season = season;
        client = new OpenLigaClient();
        var dataSource = NpgsqlDataSource.Create(connection_string);
        connection = dataSource.OpenConnection();
    }

    public void Import()
    {
        var result = client.GetTeams("bl1", season);
        result.Wait();
        var teams = result.Result;
        if (teams.Count() == 0)
            return;

        Clear();
        Insert(teams, 1);

        result = client.GetTeams("bl2", season);
        result.Wait();
        Insert(result.Result, 2);
    }

    private void Clear()
    {
        try
        {
            var sql = "drop table vereine";
            var cmd = new NpgsqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
        } 
        catch (Exception ex)
        {
            Console.WriteLine("Konnte Tabelle 'vereine' nicht löschen!");
        }

        try
        {
            var sql = "create table vereine (" +
                "verein_id serial primary key," +
                "name varchar(200) not null," +
                "liga int not null)";
            var cmd = new NpgsqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine("Konnte Tabelle verein nicht anlegen!");
        }
    }

    private void Insert(IEnumerable<Team> teams, int liga)
    {
        var sql = "insert into vereine (verein_id, name, liga) values (:verein_id, :name, :liga)";
        foreach (var team in teams)
        {
            var cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue(":verein_id", team.teamId);
            cmd.Parameters.AddWithValue(":name", team.teamName);
            cmd.Parameters.AddWithValue(":liga", liga);
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"Daten für Teams in Liga {liga} wurden geschrieben!");
    }
}
