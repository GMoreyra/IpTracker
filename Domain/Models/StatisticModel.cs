namespace Domain.Models
{
    public class StatisticModel
    {
        public string CountryName { get; set; }
        public int DistanceToBaKms { get; set; }
        public int InvocationCounter { get; set; } = 1;
    }
}
