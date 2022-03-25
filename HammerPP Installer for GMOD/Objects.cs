using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HammerPP_Installer_for_GMOD
{
    internal struct Group
    {
        internal string Name;
        internal List<Group> groups;
        internal List<Keyvalue> keyvalues;

        internal Group(string Name, List<Group> groups, List<Keyvalue> keyvalues)
        {
            this.Name = Name;
            this.groups = groups;
            this.keyvalues = keyvalues;
        }
    }

    internal struct Keyvalue
    {
        internal string key;
        internal string value;

        internal Keyvalue(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }

    internal struct gameConfig
    {
        internal Group group;
    }
}
