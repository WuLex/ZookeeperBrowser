using Newtonsoft.Json;

namespace ZookeeperBrowser.Models.Commands
{
    public class MonitorCommandResult
    {
        // ZooKeeper连接的数量
        public int Connections { get; set; }
        
        // 发送的总数据量（字节）
        public long Sent { get; set; }
        // 接收的总数据量（字节）
        public long Received { get; set; }
        // 节点数
        public long NodeCount { get; set; }
       
     
       
        // 日志文件大小
        public string LogFileSize { get; set; }
        // 最近一次请求的延迟（毫秒）
        public int Latency { get; set; }
        
      
        // 响应时间的最大值（毫秒）
        public int MaxResponseTime { get; set; }
        // 响应时间的最小值（毫秒）
        public int MinResponseTime { get; set; }
        // 响应时间的平均值（毫秒）
        public int AvgResponseTime { get; set; }
        // 超过fsync限制的次数
        public int FsyncThresholdExceedCount { get; set; }
        // follower的数量
        public int Followers { get; set; }
        // 已同步的follower的数量
        public int SyncedFollowers { get; set; }
        // 等待同步的follower的数量
        public int PendingSyncs { get; set; }

        /// <summary>
        /// Zookeeper版本信息
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Zookeeper的状态，可以是standalone、leader或follower
        /// </summary>
        [JsonProperty("server_state")]
        public string ServerState { get; set; }

        /// <summary>
        /// 临时节点数
        /// </summary>
        [JsonProperty("ephemerals_count")]
        public int EphemeralsCount { get; set; }

        /// <summary>
        /// 存活连接数
        /// </summary>
        [JsonProperty("num_alive_connections")]
        public int NumAliveConnections { get; set; }

        /// <summary>
        /// 平均延迟
        /// </summary>
        [JsonProperty("avg_latency")]
        public double AverageLatency { get; set; }

        /// <summary>
        /// 等待响应请求数
        /// </summary>
        [JsonProperty("outstanding_requests")]
        public int OutstandingRequests { get; set; }

        /// <summary>
        /// Znode节点数
        /// </summary>
        [JsonProperty("znode_count")]
        public int ZnodeCount { get; set; }

        /// <summary>
        /// 全局会话数
        /// </summary>
        [JsonProperty("global_sessions")]
        public int GlobalSessions { get; set; }

        /// <summary>
        /// 非MTLS远程连接数
        /// </summary>
        [JsonProperty("non_mtls_remote_conn_count")]
        public int NonMtlsRemoteConnCount { get; set; }

        /// <summary>
        /// 最后一次客户端响应大小
        /// </summary>
        [JsonProperty("last_client_response_size")]
        public int LastClientResponseSize { get; set; }

        /// <summary>
        /// 发送数据包数
        /// </summary>
        [JsonProperty("packets_sent")]
        public int PacketsSent { get; set; }

        /// <summary>
        /// 接收数据包数
        /// </summary>
        [JsonProperty("packets_received")]
        public int PacketsReceived { get; set; }

        /// <summary>
        /// 最大客户端响应大小
        /// </summary>
        [JsonProperty("max_client_response_size")]
        public int MaxClientResponseSize { get; set; }

        /// <summary>
        /// 连接断开概率
        /// </summary>
        [JsonProperty("connection_drop_probability")]
        public double ConnectionDropProbability { get; set; }

        /// <summary>
        /// Watcher数量
        /// </summary>
        [JsonProperty("watch_count")]
        public int WatchCount { get; set; }

        /// <summary>
        /// 鉴权失败次数
        /// </summary>
        [JsonProperty("auth_failed_count")]
        public int AuthFailedCount { get; set; }

        /// <summary>
        /// 最小延迟
        /// </summary>
        [JsonProperty("min_latency")]
        public int MinLatency { get; set; }

        /// <summary>
        /// 最大文件描述符数量
        /// </summary>
        [JsonProperty("max_file_descriptor_count")]
        public int MaxFileDescriptorCount { get; set; }

      
        /// <summary>
        /// 平均延迟（毫秒）
        /// </summary>
        [JsonProperty("avg_latency")]
        public double AvgLatency { get; set; }

        /// <summary>
        /// 数据大小的估计值，格式为"size"或"size(KB)"或"size(MB)"或"size(GB)"
        /// </summary>
        [JsonProperty("approximate_data_size")]
        public int ApproximateDataSize { get; set; }

        /// <summary>
        /// 打开的文件描述符数
        /// </summary>
        [JsonProperty("open_file_descriptor_count")]
        public int OpenFileDescriptorCount { get; set; }

        [JsonProperty("local_sessions")]
        public int LocalSessions { get; set; }

        [JsonProperty("uptime")]
        public int Uptime { get; set; }

        /// <summary>
        /// 最大延迟（毫秒）
        /// </summary>
        [JsonProperty("max_latency")]
        public int MaxLatency { get; set; }

        [JsonProperty("outstanding_tls_handshake")]
        public int OutstandingTlsHandshake { get; set; }

        [JsonProperty("min_client_response_size")]
        public int MinClientResponseSize { get; set; }

        [JsonProperty("non_mtls_local_conn_count")]
        public int NonMtlsLocalConnCount { get; set; }

        [JsonProperty("proposal_count")]
        public int ProposalCount { get; set; }

        [JsonProperty("watch_bytes")]
        public int WatchBytes { get; set; }

        [JsonProperty("outstanding_changes_removed")]
        public int OutstandingChangesRemoved { get; set; }

        [JsonProperty("throttled_ops")]
        public int ThrottledOps { get; set; }

        [JsonProperty("stale_requests_dropped")]
        public int StaleRequestsDropped { get; set; }

        [JsonProperty("large_requests_rejected")]
        public int LargeRequestsRejected { get; set; }

        [JsonProperty("insecure_admin_count")]
        public int InsecureAdminCount { get; set; }

        [JsonProperty("connection_rejected")]
        public int ConnectionRejected { get; set; }

        [JsonProperty("cnxn_closed_without_zk_server_running")]
        public int CnxnClosedWithoutZkServerRunning { get; set; }

    }
}
