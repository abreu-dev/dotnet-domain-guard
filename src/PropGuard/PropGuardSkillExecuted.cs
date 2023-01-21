namespace PropGuard
{
    public class PropGuardSkillExecuted
    {
        public PropGuardSkill Skill { get; }
        public bool Succeeded { get; }

        public PropGuardSkillExecuted(PropGuardSkill skill, bool succeeded)
        {
            Skill = skill;
            Succeeded = succeeded;
        }
    }
}
