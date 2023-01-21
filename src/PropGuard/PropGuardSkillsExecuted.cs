namespace PropGuard
{
    public class PropGuardSkillsExecuted : List<PropGuardSkillExecuted>
    {
        public void AddSucceed(PropGuardSkill skill)
        {
            Add(new PropGuardSkillExecuted(skill, true));
        }

        public void AddFail(PropGuardSkill skill)
        {
            Add(new PropGuardSkillExecuted(skill, false));
        }
    }
}
