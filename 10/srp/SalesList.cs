class SalesList
{
    private List<SalesPosition> list = new List<SalesPosition>();

    public void Add(SalesPosition obj)
    {
        list.Add(obj);
    }

    public double GetTurnover() 
    {
        double sum = 0;
        foreach (var obj in list)
            sum += obj.Turnover;

        return sum;
    }
}