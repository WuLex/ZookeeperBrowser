using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ZookeeperBrowser.Models.Commands
{
    public class ZookeeperStats
    {
        /// <summary>
        /// ZooKeeper 服务器版本
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// 当前 Zookeeper 服务器是否以只读模式运行
        /// </summary>
        [JsonProperty("read_only")]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// 服务器统计信息
        /// </summary>
        [JsonProperty("server_stats")]
        public ServerStats ServerStats { get; set; }

        /// <summary>
        /// 最近客户端响应的信息
        /// </summary>
        [JsonProperty("client_response")]
        public ClientResponse ClientResponse { get; set; }

        /// <summary>
        /// 当前 Zookeeper 集合中节点的数量
        /// </summary>
        [JsonProperty("node_count")]
        public int NodeCount { get; set; }

        /// <summary>
        /// 当前客户端连接的详细信息列表
        /// </summary>
        [JsonProperty("connections")]
        public List<object> Connections { get; set; }

        /// <summary>
        /// 当前使用安全套接字层 (SSL) 连接的客户端的详细信息列表
        /// </summary>
        [JsonProperty("secure_connections")]
        public List<object> SecureConnections { get; set; }

        /// <summary>
        /// 原始请求的命令名称，本例中为 "stats"
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }

        /// <summary>
        /// 错误信息，如果请求成功则为 null
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public class ServerStats
    {
        /// <summary>
        /// 发送的数据包数量
        /// </summary>
        [JsonProperty("packets_sent")]
        public int PacketsSent { get; set; }

        /// <summary>
        /// 接收的数据包数量
        /// </summary>
        [JsonProperty("packets_received")]
        public int PacketsReceived { get; set; }

        /// <summary>
        /// 超过 FSync 阈值的数量
        /// </summary>
        [JsonProperty("fsync_threshold_exceed_count")]
        public int FSyncThresholdExceedCount { get; set; }

        /// <summary>
        /// 最近客户端响应的信息
        /// </summary>
        [JsonProperty("client_response_stats")]
        public ClientResponseStats ClientResponseStats { get; set; }

        /// <summary>
        /// 最后处理的 zxid
        /// </summary>
        [JsonProperty("last_processed_zxid")]
        public long LastProcessedZxid { get; set; }

        /// <summary>
        /// 待处理请求的数量
        /// </summary>
        [JsonProperty("outstanding_requests")]
        public int OutstandingRequests { get; set; }

        /// <summary>
        /// 数据目录大小
        /// </summary>
        [JsonProperty("data_dir_size")]
        public long DataDirectorySize { get; set; }

        /// <summary>
        /// Zookeeper 服务器的日志目录大小
        /// </summary>
        [JsonProperty("log_dir_size")]
        public long LogDirectorySize { get; set; }

        /// <summary>
        /// 服务器状态
        /// </summary>
        [JsonProperty("server_state")]
        public string ServerState { get; set; }
    }

    public class ClientResponseStats
    {
        /// <summary>
        /// 最近一次响应的缓冲区大小
        /// </summary>
        [JsonPropertyName("last_buffer_size")]
        public int LastBufferSize { get; set; }

        /// <summary>
        /// 所有响应中最小的缓冲区大小
        /// </summary>
        [JsonPropertyName("min_buffer_size")]
        public int MinBufferSize { get; set; }

        /// <summary>
        /// 所有响应中最大的缓冲区大小
        /// </summary>
        [JsonPropertyName("max_buffer_size")]
        public int MaxBufferSize { get; set; }
    }

    public class ClientResponse
    {
        /// <summary>
        /// 最近一次响应的缓冲区大小
        /// </summary>
        [JsonPropertyName("last_buffer_size")]
        public int LastBufferSize { get; set; }

        /// <summary>
        /// 所有响应中最小的缓冲区大小
        /// </summary>
        [JsonPropertyName("min_buffer_size")]
        public int MinBufferSize { get; set; }

        /// <summary>
        /// 所有响应中最大的缓冲区大小
        /// </summary>
        [JsonPropertyName("max_buffer_size")]
        public int MaxBufferSize { get; set; }
    }
}