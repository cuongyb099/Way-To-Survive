public interface IDamagable
{
    public float Hp { get; set; }

    public void Damage(DamageInfo info);
}
