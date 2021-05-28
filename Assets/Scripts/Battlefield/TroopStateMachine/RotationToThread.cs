namespace Battlefield
{
    public class RotationToThread : Rotation
    {
        EnemyDetector _enemyDetector;

        public RotationToThread(
            Troop troopInfo, UnitsController unitsController, EnemyDetector enemyDetector) :
            base(troopInfo, unitsController)
        {
            _enemyDetector = enemyDetector;
        }

        protected override Directions FindTargetDirection()
        {
            return _enemyDetector.threadDirection;
        }

    }
}