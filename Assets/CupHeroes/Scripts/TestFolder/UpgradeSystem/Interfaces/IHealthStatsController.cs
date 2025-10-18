public interface IHealthStatsController : IStatsController
{
    void ModifyHealth(float value);
    void ModifyHealthRegenerationValue(float value);
    void ModifyHealthRegenerationSpeed(float value);
}
