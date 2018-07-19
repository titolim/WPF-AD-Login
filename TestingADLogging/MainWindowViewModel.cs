using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security;
using System.Windows.Input;

namespace TestingADLogging
{
    public class MainWindowViewModel
    {
        public string ApplicationName { get; } = "Testing Active Directory Logging";
        public ICommand LogInCommand { get; private set; }
        public string Username { get; set; }

        public MainWindowViewModel()
        {
            this.LogInCommand = new DelegateCommand<object>(this.LogIn, x => true);

            try
            {
                this.Username = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').Last();
            }
            catch { }
        }

        private void LogIn(object arg)
        {
            try
            {
                LdapConnection connection = new LdapConnection("");
                NetworkCredential credential = new NetworkCredential(this.Username, (arg as IHavePassword).SecurePassword);
                connection.Credential = credential;
                connection.Bind();
                System.Windows.MessageBox.Show("You are now Logged!");
            }
            catch (LdapException lexc)
            {
                System.Windows.MessageBox.Show(lexc.Message);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
