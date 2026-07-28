using TinyLibrary.Domain.Models;

namespace TinyLibrary.Domain.Tests
{
    public class MemberTests
    {
        [Test]
        public void Penalize_SetsPenalizedUntil()
        {
            var member = new Member("Test Name", "test@test.com", DateTime.Now.AddDays(-1));

            DateTimeOffset expectedPenalizedUntil = DateTime.Now.AddDays(7);

            member.Penalize(expectedPenalizedUntil);

            Assert.That(member.PenalizedUntil, Is.EqualTo(expectedPenalizedUntil));

        }

        [Test]
        public void IsPenalized_ReturnsTrue_WhenPenalizedUntilIsInFuture()
        {
            var member = new Member("Test Name", "test@test.com", DateTime.Now.AddDays(-1));

            DateTimeOffset expectedPenalizedUntil = DateTime.Now.AddDays(1);

            member.Penalize(expectedPenalizedUntil);

            Assert.That(member.IsPenalized(DateTime.Now), Is.True);
        }

        [Test]
        public void IsPenalized_ReturnsFalse_WhenPenalizedUntilIsInPast()
        {
            var member = new Member("Test Name", "test@test.com", DateTime.Now.AddDays(-2));

            DateTimeOffset expectedPenalizedUntil = DateTime.Now.AddDays(-1);

            member.Penalize(expectedPenalizedUntil);

            Assert.That(member.IsPenalized(DateTime.Now), Is.False);

        }

        [Test]
        public void IsPenalized_ReturnsFalse_WhenNeverPenalized()
        {
            var member = new Member("Test Name", "test@test.com", DateTime.Now.AddDays(-1));

            Assert.That(member.IsPenalized(DateTime.Now), Is.False);

        }



    }
}
