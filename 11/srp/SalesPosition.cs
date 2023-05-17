class SalesPosition
{
    public string ProductName { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    
    public double Turnover
    {
        get { return Price * Quantity; }
    }
}