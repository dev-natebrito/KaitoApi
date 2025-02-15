using Domain.Entities;
using MediatR;

namespace Application.CalculateDamage.Haquerin;

public class CalculateHaquerinDamageHandler : IRequestHandler<CalculateHaquerinDamageCommand, DamageResult>
{
    private const int FeltMod = 10;
    private const int ProficiencyMod = 6;
    private const int HitBonusMod = 23;
    private const int ArrowsPerHit = 5;
    private const int CriticalMod = 2;


    public Task<DamageResult> Handle(CalculateHaquerinDamageCommand request, CancellationToken cancellationToken)
    {
        var result = CalculateWeapon(request.Hits);

        return Task.FromResult(result);
    }

    // private int CalculateReckless(List<Attack> requestHits)
    // {
    //     var totalBonus = 0;
    //     var cumulativeBonus = 0;
    //
    //     foreach (var hit in requestHits)
    //     {
    //         var currentBonus = hit.Critical ? 4 : 2; // Bônus por ataque (4 para crítico, 2 para normal)
    //
    //         cumulativeBonus += currentBonus; // Acumula o bônus ao longo dos acertos
    //         totalBonus += cumulativeBonus; // Soma o bônus acumulado ao total
    //     }
    //
    //     return totalBonus;
    // }

    private DamageResult CalculateWeapon(List<Attack> requestHits)
    {
        var totalBonus = 0;
        var cumulativeBonus = 0;
        var details = new List<DetailedDamage>();

        foreach (var (index, hit) in requestHits.Index())
        {
            var currentBonus = hit.Critical ? 4 : 2; // Bônus por ataque (4 para crítico, 2 para normal)

            cumulativeBonus += currentBonus; // Acumula o bônus ao longo dos acertos
            totalBonus += cumulativeBonus;

            var die = hit.Critical ? ArrowsPerHit * CriticalMod : ArrowsPerHit;

            var stringPerHit = $"/r {die}d8 + {HitBonusMod} + {ProficiencyMod} + {cumulativeBonus}+{FeltMod}";

            var detailedDamage = new DetailedDamage(
                index,
                stringPerHit,
                hit.Critical);

            details.Add(detailedDamage);
        }

        //(5d8 + 23 + 6 + reckless) x hits
        var dice = requestHits.Sum(hit => hit.Critical ? ArrowsPerHit * CriticalMod : ArrowsPerHit);

        var stringDamage =
            $"/r {dice}d8 + {HitBonusMod * requestHits.Count} + {ProficiencyMod * requestHits.Count} + {totalBonus} + {FeltMod * requestHits.Count}";

        return new DamageResult(
            SpiritName: "Haquerin",
            AttackType: "Prismatic Reflection",
            stringDamage,
            details);
    }
}