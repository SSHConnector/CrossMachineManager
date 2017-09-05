using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrossMachineManager
{
    public class InvokeSSH
    {
        private string host;
        private string username;
        private string password;
        public string ErrorMessage;
        public int ExitStatus;
        public InvokeSSH(string host, string username, string password)
        {
            this.host = host;
            this.username = username;
            this.password = password;
        }

        private void HandleKeyEvent(object sender, AuthenticationPromptEventArgs e)
        {
            foreach (AuthenticationPrompt prompt in e.Prompts)
            {
                if (prompt.Request.IndexOf("Password:", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    prompt.Response = this.password;
                }
            }
        }

        public string Run(string command)
        {
            string result = string.Empty;
            try {
                using (var client = new SshClient(this.host,this.username, this.password))
                {
                    client.Connect();
                    SshCommand cmd = client.RunCommand(command);
                    result = cmd.Result;
                    ErrorMessage = cmd.Error;
                    ExitStatus = cmd.ExitStatus;
                    client.Disconnect();
                }
            }
            catch (Exception exc)
            {
                if (exc.ToString().Contains("No suitable authentication method found to complete authentication"))
                {
                    KeyboardInteractiveAuthenticationMethod keybAuth = new KeyboardInteractiveAuthenticationMethod(this.username);
                    keybAuth.AuthenticationPrompt += new EventHandler<AuthenticationPromptEventArgs>(HandleKeyEvent);
                    var connectionInfo = new ConnectionInfo(this.host, 22, this.username, keybAuth);
                    using (var client = new SshClient(connectionInfo))
                    {
                        client.Connect();
                        SshCommand cmd = client.RunCommand(command);
                        result = cmd.Result;
                        ErrorMessage = cmd.Error;
                        ExitStatus = cmd.ExitStatus;
                        client.Disconnect();
                    }
                }
            }
            
            return result;
        }

        public string RunShell(string command)
        {
            string result = string.Empty;
            try
            {
                using (var client = new SshClient(this.host, this.username, this.password))
                {
                    client.Connect();
                    IDictionary<Renci.SshNet.Common.TerminalModes, uint> termkvp = new Dictionary<Renci.SshNet.Common.TerminalModes, uint>();
                    termkvp.Add(Renci.SshNet.Common.TerminalModes.ECHO, 53);

                    ShellStream shellStream = client.CreateShellStream("xterm", 80, 24, 800, 600, 1024, termkvp);

                    StreamReader reader = new StreamReader(shellStream);
                    StreamWriter writer = new StreamWriter(shellStream);
                    writer.AutoFlush = true;

                    result += reader.ReadToEnd();
                    while (shellStream.Length == 0)
                    {
                        Thread.Sleep(500);
                    }

                    Thread.Sleep(500);
                    result += reader.ReadToEnd();

                    writer.WriteLine(command);
                    while (shellStream.Length == 0)
                    {
                        Thread.Sleep(500);
                    }

                    Thread.Sleep(1000);
                    result += reader.ReadToEnd();

                    shellStream.Dispose();
                    client.Disconnect();
                    client.Dispose();
                }
            }
            catch (Exception exc)
            {
                if (exc.ToString().Contains("No suitable authentication method found to complete authentication"))
                {

                    KeyboardInteractiveAuthenticationMethod keybAuth = new KeyboardInteractiveAuthenticationMethod(this.username);
                    keybAuth.AuthenticationPrompt += new EventHandler<AuthenticationPromptEventArgs>(HandleKeyEvent);
                    var connectionInfo = new ConnectionInfo(this.host, 22, this.username, keybAuth);
                    using (var client = new SshClient(connectionInfo))
                    {
                        client.Connect();
                        IDictionary<Renci.SshNet.Common.TerminalModes, uint> termkvp = new Dictionary<Renci.SshNet.Common.TerminalModes, uint>();
                        termkvp.Add(Renci.SshNet.Common.TerminalModes.ECHO, 53);

                        ShellStream shellStream = client.CreateShellStream("xterm", 80, 24, 800, 600, 1024, termkvp);

                        StreamReader reader = new StreamReader(shellStream);
                        StreamWriter writer = new StreamWriter(shellStream);
                        writer.AutoFlush = true;

                        result += reader.ReadToEnd();
                        while (shellStream.Length == 0)
                        {
                            Thread.Sleep(500);
                        }

                        Thread.Sleep(500);
                        result += reader.ReadToEnd();

                        writer.WriteLine(command);
                        while (shellStream.Length == 0)
                        {
                            Thread.Sleep(500);
                        }

                        Thread.Sleep(1000);
                        result += reader.ReadToEnd();

                        shellStream.Dispose();
                        client.Disconnect();
                        client.Dispose();
                    }
                }
            }
            return result;
        }

        public void Download(string linuxFilePath, string winFilePath)
        {
            try
            {
                using (var ftp = new SftpClient(this.host, this.username, this.password))
                {
                    ftp.Connect();
                    Stream output = new FileStream(winFilePath, FileMode.CreateNew);
                    ftp.DownloadFile(linuxFilePath, output);
                    output.Close();
                    output.Dispose();
                    ftp.Disconnect();
                    ftp.Dispose();
                }
            }
            catch (Exception exc)
            {
                if (exc.ToString().Contains("No suitable authentication method found to complete authentication"))
                {

                    KeyboardInteractiveAuthenticationMethod keybAuth = new KeyboardInteractiveAuthenticationMethod(this.username);
                    keybAuth.AuthenticationPrompt += new EventHandler<AuthenticationPromptEventArgs>(HandleKeyEvent);
                    var connectionInfo = new ConnectionInfo(this.host, 22, this.username, keybAuth);
                    using (var ftp = new SftpClient(connectionInfo))
                    {
                        ftp.Connect();
                        Stream output = new FileStream(winFilePath, FileMode.CreateNew);
                        ftp.DownloadFile(linuxFilePath, output);
                        output.Close();
                        output.Dispose();
                        ftp.Disconnect();
                        ftp.Dispose();
                    }
                }
            }
        }

        public void Upload(string linuxFilePath, string winFilePath)
        {
            try
            {
                using (var ftp = new SftpClient(this.host, this.username, this.password))
                {
                    ftp.Connect();
                    Stream input = File.OpenRead(winFilePath);
                    ftp.UploadFile(input, linuxFilePath, true);
                    input.Close();
                    input.Dispose();
                    ftp.Disconnect();
                    ftp.Dispose();
                }
            }
            catch (Exception exc)
            {
                if (exc.ToString().Contains("No suitable authentication method found to complete authentication"))
                {

                    KeyboardInteractiveAuthenticationMethod keybAuth = new KeyboardInteractiveAuthenticationMethod(this.username);
                    keybAuth.AuthenticationPrompt += new EventHandler<AuthenticationPromptEventArgs>(HandleKeyEvent);
                    var connectionInfo = new ConnectionInfo(this.host, 22, this.username, keybAuth);
                    using (var ftp = new SftpClient(connectionInfo))
                    {
                        ftp.Connect();
                        Stream input = File.OpenRead(winFilePath);
                        ftp.UploadFile(input, linuxFilePath, true);
                        input.Close();
                        input.Dispose();
                        ftp.Disconnect();
                        ftp.Dispose();
                    }
                }
            }
        }
    }
}
