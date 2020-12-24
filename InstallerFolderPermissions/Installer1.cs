using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;

namespace InstallerFolderPermissions {
    [RunInstaller(true)]
    public partial class Installer1 : Installer {
        public Installer1() {
            InitializeComponent();
        }
        public override void Install(IDictionary stateSaver) {
            // This gets the named parameters passed in from your custom action
            string folder = Context.Parameters["folder"];

            // This gets the "Authenticated Users" group, no matter what it's called
            SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);

            // Create the rules
            FileSystemAccessRule writerule = new FileSystemAccessRule(sid, FileSystemRights.Write, AccessControlType.Allow);

            if (!string.IsNullOrEmpty(folder) && Directory.Exists(folder)) {
                // Get your file's ACL
                DirectorySecurity fsecurity = Directory.GetAccessControl(folder);

                // Add the new rule to the ACL
                fsecurity.AddAccessRule(writerule);

                // Set the ACL back to the file
                Directory.SetAccessControl(folder, fsecurity);
            }

            // Explicitly call the overriden method to properly return control to the installer
            base.Install(stateSaver);
        }
    }
}
