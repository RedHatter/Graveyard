using System;
using System.Reflection;

namespace HDT.Plugins.Graveyard.Properties
{
    public static class LibraryInfo
    {
        public static readonly AssemblyName AssemblyName = Assembly.GetExecutingAssembly().GetName();
        public static readonly Version AssemblyVersion = AssemblyName.Version;

        public static string Name = AssemblyName.Name;
        public static readonly Version Version = new Version(AssemblyVersion.Major, AssemblyVersion.Minor, AssemblyVersion.Build);
    }
}
