using Newtonsoft.Json;

namespace ZookeeperBrowser.Models.Commands
{
    public class ZooKeeperConfiguration
    {
       
        [JsonProperty("data_dir")]
        public string DataDirectory { get; set; }

        [JsonProperty("data_log_dir")]
        public string DataLogDirectory { get; set; }

        /// <summary>
        ///  Tick时间（毫秒）
        /// </summary>
        [JsonProperty("tick_time")]
        public int TickTime { get; set; }

        [JsonProperty("max_client_cnxns")]
        public int MaxClientConnections { get; set; }

      
        [JsonProperty("max_session_timeout")]
        public int MaxSessionTimeout { get; set; }// 最大会话超时时间（毫秒）

        [JsonProperty("server_id")]
        public int ServerId { get; set; } // 服务器ID

        [JsonProperty("client_port_listen_backlog")]
        public int ClientPortListenBacklog { get; set; }

        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("client_port")]
        public string ClientPort { get; set; } // 客户端端口
        public string SecureClientPort { get; set; } // 安全客户端端口
        public string DataDir { get; set; } // 数据目录
        public string DataLogDir { get; set; } // 事务日志目录
 
        public int MaxClientCnxns { get; set; } // 最大客户端连接数

        [JsonProperty("min_session_timeout")]
        public int MinSessionTimeout { get; set; } // 最小会话超时时间（毫秒）
 
        public string InitLimit { get; set; } // 初始同步限制
        public string SyncLimit { get; set; } // 同步限制
        public string LogLevel { get; set; } // 日志级别
        public bool ReconfigEnabled { get; set; } // 是否启用动态重配置
        public int ZkId { get; set; } // ZooKeeper实例ID
        public string Servers { get; set; } // ZooKeeper服务器列表
        public string MetricsProviderClassName { get; set; } // 指标提供程序类名
        public string QuorumListenOnAllIPs { get; set; } // 是否监听所有IP
        public string AutoPurgeSnapRetainCount { get; set; } // 自动清理快照保留数量
        public string AutoPurgePurgeInterval { get; set; } // 自动清理间隔时间（小时）
    }
}
