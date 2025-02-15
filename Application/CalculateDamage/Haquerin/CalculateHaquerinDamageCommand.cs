using Domain.Entities;
using MediatR;

namespace Application.CalculateDamage.Haquerin;

public record CalculateHaquerinDamageCommand(List<Attack> Hits) : IRequest<DamageResult>;