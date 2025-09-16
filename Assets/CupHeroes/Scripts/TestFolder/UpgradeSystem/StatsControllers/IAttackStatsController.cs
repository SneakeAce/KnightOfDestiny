public interface IAttackStatsController : IStatsController
{
    public int AmountTargetsForAttack { get; }
    public float Damage { get; }
    public float AttackSpeedProcent { get; }
    public float AttacksPerSecond { get; }
    public float AttackRange { get; }

    void ModifyDamage(float value);
    void ModifyAttackSpeed(float value);
    void ModifyAmountTargetsForAttack(int value);
}
