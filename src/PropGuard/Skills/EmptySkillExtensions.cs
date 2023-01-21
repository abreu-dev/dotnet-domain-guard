namespace PropGuard.Skills
{
    public static partial class PropGuardSkillExtensions
    {
        public static IPropGuard<string> Empty(this IPropGuard<string> propGuard)
        {
            if (string.IsNullOrEmpty(propGuard.PropValue))
            {
                propGuard.SkillsExecuted.AddFail(PropGuardSkill.Empty);
            }
            else
            {
                propGuard.SkillsExecuted.AddSucceed(PropGuardSkill.Empty);
            }

            return propGuard;
        }

        public static IPropGuard<int> Empty(this IPropGuard<int> propGuard)
        {
            if (propGuard.PropValue.Equals(0))
            {
                propGuard.SkillsExecuted.AddFail(PropGuardSkill.Empty);
            }
            else
            {
                propGuard.SkillsExecuted.AddSucceed(PropGuardSkill.Empty);
            }

            return propGuard;
        }

        public static IPropGuard<Guid> Empty(this IPropGuard<Guid> propGuard)
        {
            if (propGuard.PropValue.Equals(Guid.Empty))
            {
                propGuard.SkillsExecuted.AddFail(PropGuardSkill.Empty);
            }
            else
            {
                propGuard.SkillsExecuted.AddSucceed(PropGuardSkill.Empty);
            }

            return propGuard;
        }
    }
}
