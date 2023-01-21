namespace PropGuard
{
    public interface IPropGuard
    {
        string PropName { get; }
        PropGuardSkillsExecuted SkillsExecuted { get; }
    }

    public interface IPropGuard<TPropType> : IPropGuard
    {
        TPropType PropValue { get; }
    }

    public class PropGuard<TPropType> : IPropGuard<TPropType>
    {
        public string PropName { get; }
        public TPropType PropValue { get; }
        public PropGuardSkillsExecuted SkillsExecuted { get; protected set; }

        public PropGuard(string propName, TPropType propValue)
        {
            PropName = propName;
            PropValue = propValue;
            SkillsExecuted = new PropGuardSkillsExecuted();
        }
    }
}
