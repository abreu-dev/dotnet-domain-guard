using System.Text;

namespace PropGuard.TestHelper
{
    public class TestPropGuard<TPropType> : PropGuard<TPropType>
    {
        public TestPropGuard(IPropGuard<TPropType> propGuard) : base(propGuard.PropName, propGuard.PropValue)
        {
            SkillsExecuted = propGuard.SkillsExecuted;
        }

        public void ShouldHaveSucceedFor(PropGuardSkill skill)
        {
            if (SkillsExecuted.Any(item => item.Skill == skill && item.Succeeded)) return;

            throw BuildTestException($"Expected a succeed for skill {skill}.");
        }

        public void ShouldHaveOnlySucceedFor(PropGuardSkill skill)
        {
            if (SkillsExecuted.Count != 1)
                throw BuildTestException($"Expected a single skill, but found {SkillsExecuted.Count}.");

            ShouldHaveSucceedFor(skill);
        }

        public void ShouldHaveFailFor(PropGuardSkill skill)
        {
            if (SkillsExecuted.Any(item => item.Skill == skill && !item.Succeeded)) return;

            throw BuildTestException($"Expected a fail for skill {skill}.");
        }

        public void ShouldHaveOnlyFailFor(PropGuardSkill skill)
        {
            if (SkillsExecuted.Count != 1)
                throw BuildTestException($"Expected a single skill, but found {SkillsExecuted.Count}.");

            ShouldHaveFailFor(skill);
        }

        private TestPropGuardException BuildTestException(string errorMessageBanner)
        {
            var errorMessage = new StringBuilder();

            if (SkillsExecuted.Any())
            {
                var errorMessageDetails = new StringBuilder();

                var succeeds = SkillsExecuted.Where(item => item.Succeeded).ToList();
                var errorMessageSucceedDetails = $"Skills that succeeded: {succeeds.Count}.";
                if (succeeds.Any())
                {
                    var skills = string.Join(", ", succeeds.Select(item => item.Skill));
                    errorMessageSucceedDetails = $"{errorMessageSucceedDetails} ({skills}).";
                }
                errorMessageDetails.AppendLine(errorMessageSucceedDetails.ToString());

                var fails = SkillsExecuted.Where(item => !item.Succeeded).ToList();
                var errorMessageFailDetails = $"Skills that failed: {fails.Count}.";
                if (fails.Any())
                {
                    var skills = string.Join(", ", fails.Select(item => item.Skill));
                    errorMessageFailDetails = $"{errorMessageFailDetails} ({skills}).";
                }
                errorMessageDetails.AppendLine(errorMessageFailDetails.ToString());

                errorMessage.AppendLine(errorMessageBanner);
                errorMessage.AppendLine(errorMessageDetails.ToString());
            }
            else
            {
                errorMessage.AppendLine(errorMessageBanner);
            }

            return new TestPropGuardException(errorMessage.ToString());
        }
    }
}
