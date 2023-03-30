namespace ZookeeperBrowser.Models
{
    /// <summary>
    /// 未使用
    /// 
    /// $ echo mntr | nc 192.168.1.229 2181
    //zk_version      3.4.5-cdh6.3.1--1, built on 09/26/2019 09:28 GMT    # 版本
    //zk_avg_latency  0                                                   # 平均延时
    //zk_max_latency  51                                                  # 最大延时
    //zk_min_latency  0                                                   # 最小延时
    //zk_packets_received     825166                                      # 收包数
    //zk_packets_sent 844514                                              # 发包数
    //zk_num_alive_connections        10                                  # 连接数
    //zk_outstanding_requests 0                                           # 堆积请求数
    //zk_server_state leader                                              # 状态
    //zk_znode_count  399                                                 # znode 数量
    //zk_watch_count  74                                                  # watch 数量
    //zk_ephemerals_count     9                                           # 临时节点(znode)
    //zk_approximate_data_size        28043                               # 数据大小
    //zk_open_file_descriptor_count   59                                  # 打开的文件描述符数量
    //zk_max_file_descriptor_count    32768                               # 最大文件描述符数量
    //zk_fsync_threshold_exceed_count 0
    //zk_followers    2                                                   # follower 数量，leader角色才会有这个输出
    //zk_synced_followers     2                                           # 同步的 follower 数量
    //zk_pending_syncs        0                                           # 准备同步数，leader角色才会有这个输出
    //zk_last_proposal_size   32                                          # 最近一次 Proposal 消息大小
    //zk_max_proposal_size    1337                                        # 最大 Proposal 消息大小
    //zk_min_proposal_size    32                                          # 最小 Proposal 消息大小
    /// </summary>
    public class ZookeeperMetrics
    {
        /// <summary>
        ///
        /// </summary>
        public string ZkVersion { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ZkAvgLatency { get; set; }

        /// <summary>
        ///
        /// </summary>

        public int ZkMaxLatency { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ZkMinLatency { get; set; }

        /// <summary>
        /// </summary>
        public long ZkPacketsReceived { get; set; }

        /// <summary>
        /// </summary>
        public long ZkPacketsSent { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ZkNumAliveConnections { get; set; }

        /// <summary>
        /// </summary>
        public int ZkOutstandingRequests { get; set; }

        /// <summary>
        /// </summary>
        public string ZkServerState { get; set; }

        /// <summary>
        /// </summary>
        public int ZkZnodeCount { get; set; }

        /// <summary>
        /// </summary>
        public int ZkWatchCount { get; set; }

        /// <summary>
        /// </summary>
        public int ZkEphemeralsCount { get; set; }

        /// <summary>
        /// </summary>
        public int ZkApproximateDataSize { get; set; }

        /// <summary>
        /// </summary>
        public int ZkOpenFileDescriptorCount { get; set; }

        /// <summary>
        /// </summary>
        public int ZkMaxFileDescriptorCount { get; set; }

        /// <summary>
        /// </summary>
        public int ZkFsyncThresholdExceedCount { get; set; }

        /// <summary>
        /// </summary>
        public int ZkFollowers { get; set; }

        /// <summary>
        /// </summary>
        public int ZkSyncedFollowers { get; set; }

        /// <summary>
        /// </summary>
        public int ZkPendingSyncs { get; set; }

        /// <summary>
        /// </summary>
        public int ZkLastProposalSize { get; set; }

        /// <summary>
        /// </summary>
        public int ZkMaxProposalSize { get; set; }

        /// <summary>
        /// </summary>
        public int ZkMinProposalSize { get; set; }

        /// <summary>
        /// </summary>
    }

    /// <summary>
    /// 未使用
    /// </summary>
    public class ZooKeeperMetricsModel
    {
        public string Version { get; set; }
        public long AvgLatency { get; set; }
        public long MaxLatency { get; set; }
        public long MinLatency { get; set; }
        public long PacketsReceived { get; set; }
        public long PacketsSent { get; set; }
        public int NumAliveConnections { get; set; }
        public int OutstandingRequests { get; set; }
        public string ServerState { get; set; }
        public int ZNodeCount { get; set; }
        public int WatchCount { get; set; }
        public int EphemeralsCount { get; set; }
        public long ApproximateDataSize { get; set; }
        public int OpenFileDescriptorCount { get; set; }
        public int MaxFileDescriptorCount { get; set; }
        public int FSyncThresholdExceedCount { get; set; }
        public int Followers { get; set; }
        public int SyncedFollowers { get; set; }
        public int PendingSyncs { get; set; }
        public int LastProposalSize { get; set; }
        public int MaxProposalSize { get; set; }
        public int MinProposalSize { get; set; }
    }
}