namespace GalacticEmpire.Domain.Models.EmpireModel
{
    public class FightPoint
    {
        public double AttackPointMultiplier { get; set; } = 1;
        public double DefensePointMultiplier { get; set; } = 1;

        public int AttackPointBonus { get; set; } = 0;
        public int DefensePointBonus { get; set; } = 0;
    }
}