using BundesligaDatenLader.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BundesligaDatenLader;

public class DatabaseFacade
{
    private int season;
    private OpenLigaClient client;
    private NpgsqlConnection connection;

    public DatabaseFacade(int season, string connection_string) 
    {
        this.season = season;
        client = new OpenLigaClient();
        var dataSource = NpgsqlDataSource.Create(connection_string);
        connection = dataSource.OpenConnection();
    }

    public void ImportVereine()
    {
        var result = client.GetTeams("bl1", season);
        result.Wait();
        var teams = result.Result;
        if (teams.Count() == 0)
            return;

        ClearTeams();
        InsertTeams(teams, 1);

        result = client.GetTeams("bl2", season);
        result.Wait();
        InsertTeams(result.Result, 2);
    }

    public void ClearTeams()
    {
        try
        {
            var sql = "drop table tmp.verein";
            var cmd = new NpgsqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
        } 
        catch (Exception ex)
        {
            Console.WriteLine("Konnte Tabelle Verein nicht löschen!");
        }

        try
        {
            var sql = "create table tmp.verein (" +
                "v_id serial primary key," +
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

    public void InsertTeams(IEnumerable<Team> teams, int liga)
    {
        var sql = "insert into tmp.verein (v_id, name, liga) values (:v_id, :name, :liga)";
        foreach (var team in teams)
        {
            var cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue(":v_id", team.teamId);
            cmd.Parameters.AddWithValue(":name", team.teamName);
            cmd.Parameters.AddWithValue(":liga", liga);
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"Vereinsdaten für Liga {liga} wurden geschrieben!");
    }
}
