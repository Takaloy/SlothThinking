using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothThinking
{
    public interface ISloth
    {
        int Id { get; }

        string Title { get; }

        string BattleTag { get; }

        /// <summary>
        /// only ranked mode MMR
        /// </summary>
        int Mmr { get; }
        
        /// <summary>
        /// includes both ranked and unranked mode MMR
        /// </summary>
        int AllMmr { get; }
    }

    public class Sloth : ISloth, IEquatable<ISloth>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BattleTag { get; set; }
        public int Mmr { get; set; }
        public int AllMmr { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ISloth) obj);
        }

        public bool Equals(ISloth other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
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
