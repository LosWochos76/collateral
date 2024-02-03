using System;

class Program
{
    public static void Main() 
    {
        var file_content = File.ReadAllLines("Data.csv");
        var sales_list = CsvParser.ParseToSalesList(file_content);
        var turnover = sales_list.GetTurnover();
        
        Console.WriteLine(turnover);
    }
}