using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace PDFBinder
{
    public class ProcessLinker
    {
        IChannel channel;

        public bool IsServer
        {
            get { return channel is IpcServerChannel; }
        }

        public ProcessLinker()
        {
            try
            {
                channel = new IpcServerChannel("pdfbinder");
                ChannelServices.RegisterChannel(channel, false);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(IpcInterface), "files", WellKnownObjectMode.Singleton);
            }
            catch (RemotingException)
            {
                channel = new IpcClientChannel();
                ChannelServices.RegisterChannel(channel, true);
                RemotingConfiguration.RegisterWellKnownClientType(typeof(IpcInterface), "ipc://pdfbinder/files");
            }
        }

        public void SendFileList(params string[] files)
        {
            var ipc = new IpcInterface();
            
            foreach (string file in files)
            {
                ipc.AddInputFile(file);
            }

            ipc.UpdateUI();
        }

        class IpcInterface : MarshalByRefObject
        {
            public void AddInputFile(string path)
            {
                Program.MainForm.AddInputFile(path);
            }

            public void UpdateUI()
            {
                Program.MainForm.UpdateUI();
            }
        }
    }
}
