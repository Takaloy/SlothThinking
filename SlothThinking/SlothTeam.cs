using System;

namespace SlothThinking
{
    public interface ISlothTeam
    {
        int Id { get; }
        string Title { get; }
        int SlothRating { get; }
    }

    public class SlothTeam : ISlothTeam, IEquatable<ISlothTeam>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SlothRating { get; set; }

        public override string ToString()
        {
            return $"{Title.Trim()} : {SlothRating}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ISlothTeam) obj);
        }

        public bool Equals(ISlothTeam other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(SlothTeam left, SlothTeam right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SlothTeam left, SlothTeam right)
        {
            return !Equals(left, right);
        }
    }
}
