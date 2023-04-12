using Newtonsoft.Json;

namespace ZookeeperBrowser.Models.Commands
{
    public class ZooKeeperConfiguration
    {
        /// <summary>
        /// 数据目录
        /// </summary>
        [JsonProperty("data_dir")]
        public string DataDirectory { get; set; }

        /// <summary>
        ///  数据日志文件的存储路径
        /// </summary>
        [JsonProperty("data_log_dir")]
        public string DataLogDirectory { get; set; }

        /// <summary>
        ///  Tick时间（毫秒）
        ///  ZooKeeper 使用的基本时间单位，以毫秒为单位。
        ///  此处为 2000，表示每 2 秒进行一次心跳检测。
        /// </summary>
        [JsonProperty("tick_time")]
        public int TickTime { get; set; }

        /// <summary>
        /// 最大客户端连接数
        /// </summary>
        [JsonProperty("max_client_cnxns")]
        public int MaxClientConnections { get; set; }

        /// <summary>
        /// 最小会话超时时间（毫秒）
        /// ZooKeeper 会话的最小超时时间，以毫秒为单位。
        /// 此处为 4000 毫秒，表示会话最短可以保持 4 秒钟。
        /// </summary>
        [JsonProperty("min_session_timeout")]
        public int MinSessionTimeout { get; set; }

        /// <summary>
        /// 最大会话超时时间（毫秒）
        /// ZooKeeper 会话的最大超时时间，以毫秒为单位。
        /// 此处为 40000 毫秒，表示会话最长可以保持 40 秒钟。
        /// </summary>
        [JsonProperty("max_session_timeout")]
        public int MaxSessionTimeout { get; set; }

        /// <summary>
        /// 服务器ID
        /// </summary>
        [JsonProperty("server_id")]
        public int ServerId { get; set; }

        /// <summary>
        /// 指定了客户端连接请求队列的最大长度。
        /// 当队列满了之后，任何新的连接请求将被服务器拒绝，直到队列中有空闲位置为止。
        /// 此处设置为 -1，表示使用操作系统默认值
        /// </summary>
        [JsonProperty("client_port_listen_backlog")]
        public int ClientPortListenBacklog { get; set; }

        /// <summary>
        /// 表示执行的命令，为 "configuration"，表示获取当前 ZooKeeper 配置的信息。
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }

        /// <summary>
        /// 如果执行命令时发生错误，则会将错误信息存储在此处，否则为 null。
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// ZooKeeper 服务监听客户端连接的端口号
        /// </summary>
        [JsonProperty("client_port")]
        public string ClientPort { get; set; }

        ///// <summary>
        ///// 初始同步限制
        ///// </summary>
        //public string InitLimit { get; set; }

        ///// <summary>
        ///// 同步限制
        ///// </summary>
        //public string SyncLimit { get; set; }

        ///// <summary>
        ///// 日志级别
        ///// </summary>
        //public string LogLevel { get; set; }

        ///// <summary>
        ///// 是否启用动态重配置
        ///// </summary>
        //public bool ReconfigEnabled { get; set; }

        ///// <summary>
        ///// ZooKeeper实例ID
        ///// </summary>
        //public int ZkId { get; set; }

        ///// <summary>
        ///// ZooKeeper服务器列表
        ///// </summary>
        //public string Servers { get; set; }

        ///// <summary>
        ///// 指标提供程序类名
        ///// </summary>
        //public string MetricsProviderClassName { get; set; }

        ///// <summary>
        ///// 是否监听所有IP
        ///// </summary>
        //public string QuorumListenOnAllIPs { get; set; }

        ///// <summary>
        ///// 自动清理快照保留数量
        ///// </summary>
        //public string AutoPurgeSnapRetainCount { get; set; }

        ///// <summary>
        ///// 自动清理间隔时间（小时）
        ///// </summary>
        //public string AutoPurgePurgeInterval { get; set; }
    }
}