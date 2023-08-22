public class WeaponProjectileSpawnerData : ComponentData<AttackProjectileSpawner>
{
    public WeaponProjectileSpawnerData()
    {
        ComponentDependency = typeof(WeaponProjectileSpawner);
    }
}