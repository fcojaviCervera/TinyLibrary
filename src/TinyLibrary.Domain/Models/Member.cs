namespace TinyLibrary.Domain.Models
{
    public class Member
    {
        private readonly Guid id;
        private readonly string name;
        private readonly string email;
        private readonly DateTimeOffset joinedAt;
        private DateTimeOffset? penalizedUntil;

        public Guid Id => id;
        public string Name => name;
        public string Email => email;
        public DateTimeOffset JoinedAt => joinedAt;
        public DateTimeOffset? PenalizedUntil => penalizedUntil;
        public Member(string name, string email, DateTimeOffset joinedAt)
        {
            id = Guid.NewGuid();
            this.name = name;
            this.email = email;
            this.joinedAt = joinedAt;
        }

        public void Penalize(DateTimeOffset until)
        {
            penalizedUntil = until;
        }

        public bool IsPenalized(DateTimeOffset now)
        {
            return penalizedUntil > now;
        }
    }
}
