using System;

namespace SlothThinking
{
    public class Sloth : ISloth, IEquatable<ISloth>
    {
        public bool Equals(ISloth other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string BattleTag { get; set; }
        public int Mmr { get; set; }
        public int AllMmr { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ISloth) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(Sloth left, Sloth right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Sloth left, Sloth right)
        {
            return !Equals(left, right);
        }
    }
}