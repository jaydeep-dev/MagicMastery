public interface IPlayer
{
    //Example -> If you want to increase a stat by 10%, you will pass 1.1f as multiplier.
    public void SetStatMultiplier(Stat stat, float multiplier);

    //Set it to true everytime player takes damage.
    public bool WasDamagedThisFrame { get; }
    //Example -> If you want to heal player's HP by 50%, you will pass 0.5f as multiplier.
    public void Heal(float multiplier);
}

public enum Stat
{
    Speed,
    MaxHealth,
    Defense,
    XPGainEfficiency
}