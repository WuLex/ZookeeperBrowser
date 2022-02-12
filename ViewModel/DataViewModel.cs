using org.apache.zookeeper.data;
using System.Text;


namespace ZookeeperBrowser.ViewModel
{
    public class DataViewModel
    {
        private Stat _stat;

        //[PropertyOrder(1)]
        public string DATA { get; private set; }

        public int Aversion { get { return _stat.getAversion(); } }
        public long Ctime { get { return _stat.getCtime(); } }
        public int Cversion { get { return _stat.getCversion(); } }
        public long Czxid { get { return _stat.getCzxid(); } }
        public int DataLength { get { return _stat.getDataLength(); } }
        public long EphemeralOwner { get { return _stat.getEphemeralOwner(); } }
        public long Mtime { get { return _stat.getMtime(); } }
        public long Mzxid { get { return _stat.getMzxid(); } }
        public int NumChildren { get { return _stat.getNumChildren(); } }
        public long Pzxid { get { return _stat.getPzxid(); } }
        public int Version { get { return _stat.getVersion(); } }

        public DataViewModel(byte[] data, Stat stat)
        {
            DATA = Encoding.UTF8.GetString(data ?? new byte[0]);
            _stat = stat;
        }
    }

   
}
