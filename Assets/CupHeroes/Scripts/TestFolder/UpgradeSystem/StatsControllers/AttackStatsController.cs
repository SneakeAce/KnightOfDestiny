public class AttackStatsController : IAttackStatsController
{
    private const float DividerForCoversionToProcetage = 100f;

    private IEntity _entity;

    private int _amountTargetsForAttack;
    private float _damage;
    private float _attackSpeedProcent;
    private float _attacksPerSecond;
    private float _attackRange;

    public int AmountTargetsForAttack => _amountTargetsForAttack;
    public float Damage => _damage;
    public float AttackSpeedProcent => _attackSpeedProcent;
    public float AttacksPerSecond => _attacksPerSecond;
    public float AttackRange => _attackRange;


    public void Initialize(IEntity entity)
    {
        _entity = entity;

        SetBaseParameters();
    }

    public void ModifyAttackSpeed(float value)
    {
        _attackSpeedProcent += value;
        _attacksPerSecond = _attackSpeedProcent / DividerForCoversionToProcetage;
    }

    public void ModifyDamage(float value)
    {
        _damage += value;
    }

    public void ModifyAmountTargetsForAttack(int value)
    {
        _amountTargetsForAttack += value;
    }

    public void ResetValues()
    {
        SetBaseParameters();
    }

    private void SetBaseParameters()
    {
        _amountTargetsForAttack = _entity.Config.AttackStats.BaseAmountTargetsForAttack;
        _damage = _entity.Config.AttackStats.BaseDamage;
        _attackSpeedProcent = _entity.Config.AttackStats.BaseAttackSpeedProcent;

        _attacksPerSecond = _attackSpeedProcent / DividerForCoversionToProcetage;

        _attackRange = _entity.Config.AttackStats.BaseAttackRange;
    }
}
