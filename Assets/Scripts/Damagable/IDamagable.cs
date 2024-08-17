public interface IDamagable
{
    public float HP { get; set; }
    public void Damage(DamageInfo info);
}