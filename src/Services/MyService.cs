public interface IHealthService
{
    string GetHealthStatus();
}

public class HealthService : IHealthService
{
    public string GetHealthStatus()
    {
        return "Backend is working!";
    }
}


