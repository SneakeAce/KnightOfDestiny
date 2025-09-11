using System.Collections.Generic;

public interface IEnemyControllersFactory
{
    List<EnemyController> EnemyControllers { get; }

    EnemyController CreateEnemyController(IEnemy enemy);
}
