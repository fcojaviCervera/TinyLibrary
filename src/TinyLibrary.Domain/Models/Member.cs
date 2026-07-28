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
        public Member(string _name, string _email, DateTimeOffset _joinedAt)
        {
            id = Guid.NewGuid();
            name = _name;
            email = _email;
            joinedAt = _joinedAt;
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
