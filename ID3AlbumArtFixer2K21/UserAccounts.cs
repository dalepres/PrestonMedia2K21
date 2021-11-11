using System;
using System.Collections.Generic;
using System.Text;

namespace ID3AlbumArtFixer
{
    public class UserAccount
    {
        protected UserAccount()
        {
        }

        public string AccountName { get; set; }

        public override int GetHashCode()
        {
            return AccountName.GetHashCode();
        }
    }

    public class User : UserAccount
    {
        public User(string name)
        {
            AccountName = name;
        }

        public override string ToString()
        {
            return AccountName + " (User)";
        }
    }

    public class Group : UserAccount
    {
        public Group(string name)
        {
            AccountName = name;
        }

        public override string ToString()
        {
            return AccountName + " (Group)";
        }
    }
}
