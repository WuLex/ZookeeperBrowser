using org.apache.zookeeper.data;
using System.Text;

namespace ZookeeperBrowser.ViewModel
{
    /// <summary>
    /// get命令可以获取指定ZNode的数据内容和状态属性信息
    /// </summary>
    public class DataViewModel
    {
        /// <summary>
        /// 状态属性信息
        /// </summary>
        private Stat _stat;

        public string DATA { get; private set; }

        /// <summary>
        /// 表示对此znode的ACL进行更改的次数
        /// </summary>
        public int Aversion
        {
            get { return _stat.getAversion(); }
        }

        /// <summary>
        /// 表示从1970-01-01T00:00:00Z开始以毫秒为单位的znode创建时间
        /// </summary>
        public long Ctime
        {
            get { return _stat.getCtime(); }
        }

        /// <summary>
        /// 这表示对此znode的子节点进行的更改次数。
        /// </summary>
        public int Cversion
        {
            get { return _stat.getCversion(); }
        }

        /// <summary>
        /// 这是导致创建znode更改的事务ID
        /// </summary>
        public long Czxid
        {
            get { return _stat.getCzxid(); }
        }

        /// <summary>
        /// 这是znode数据字段的长度
        /// </summary>
        public int DataLength
        {
            get { return _stat.getDataLength(); }
        }

        /// <summary>
        /// 如果znode是ephemeral类型节点，则这是znode所有者的 session ID。 如果znode不是ephemeral节点，则该字段设置为零。
        /// </summary>
        public long EphemeralOwner
        {
            get { return _stat.getEphemeralOwner(); }
        }

        /// <summary>
        /// 表示从1970-01-01T00:00:00Z开始以毫秒为单位的znode最近修改时间。
        /// </summary>
        public long Mtime
        {
            get { return _stat.getMtime(); }
        }

        /// <summary>
        /// 这是最后修改znode更改的事务ID。
        /// </summary>
        public long Mzxid
        {
            get { return _stat.getMzxid(); }
        }

        /// <summary>
        /// 这表示znode的子节点的数量。
        /// </summary>
        public int NumChildren
        {
            get { return _stat.getNumChildren(); }
        }

        /// <summary>
        /// 这是用于添加或删除子节点的znode更改的事务ID。
        /// </summary>
        public long Pzxid
        {
            get { return _stat.getPzxid(); }
        }

        /// <summary>
        /// 表示对该znode的数据所做的更改次数。
        /// </summary>
        public int Version
        {
            get { return _stat.getVersion(); }
        }

        public DataViewModel(byte[] data, Stat stat)
        {
            DATA = Encoding.UTF8.GetString(data ?? new byte[0]);
            _stat = stat;
        }
    }
}