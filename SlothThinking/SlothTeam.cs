using System;

namespace SlothThinking
{
    public interface ISlothTeamInfo
    {
        int Id { get; }
        string Title { get; }
        int SlothRating { get; }
    }

    public class SlothTeamInfo : ISlothTeamInfo, IEquatable<ISlothTeamInfo>
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
            return Equals((ISlothTeamInfo) obj);
        }

        public bool Equals(ISlothTeamInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(SlothTeamInfo left, SlothTeamInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SlothTeamInfo left, SlothTeamInfo right)
        {
            return !Equals(left, right);
        }
    }
}
