class CsvParser
{
    public static SalesList ParseToSalesList(string[] file_content) 
    {
        var result = new SalesList();
        for (int i=1; i<file_content.Length; i++)
            result.Add(ParseToSalesPosition(file_content[i]));

        return result;
    }

    public static SalesPosition ParseToSalesPosition(string line) 
    {
        var elements = line.Split(';');
        return new SalesPosition() 
        {
            ProductName = elements[0],
            Price = Convert.ToDouble(elements[1]),
            Quantity = Convert.ToInt32(elements[2])
        };
    }
}