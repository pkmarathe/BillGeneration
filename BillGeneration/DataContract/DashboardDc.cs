using System.Collections.Generic;
public class DashboardDc
{
    public DashboardCountDc DashboardCountDc { get; set; } 
}
public class DashboardCountDc
{
    public string TotalCategory { get; set; }
    public string TotalCategoryReciept { get; set; }
    public string TotalCustomer { get; set; }
    public string TotalBillReciept { get; set; }
}