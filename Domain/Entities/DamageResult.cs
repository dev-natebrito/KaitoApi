namespace Domain.Entities;

public record DamageResult(
    string SpiritName,
    string AttackType,
    string TotalDamageString,
    List<DetailedDamage> Details);