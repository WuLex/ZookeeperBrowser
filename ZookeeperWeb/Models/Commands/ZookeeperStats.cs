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
        /// 是否只读模式
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
        /// 节点数量
        /// </summary>
        [JsonProperty("node_count")]
        public int NodeCount { get; set; }

        /// <summary>
        /// 连接列表
        /// </summary>
        [JsonProperty("connections")]
        public List<object> Connections { get; set; }

        /// <summary>
        /// 安全连接列表
        /// </summary>
        [JsonProperty("secure_connections")]
        public List<object> SecureConnections { get; set; }

        /// <summary>
        /// 命令
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }

        /// <summary>
        /// 错误信息
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
        /// 日志目录大小
        /// </summary>
        [JsonProperty("log_dir_size")]
        public long LogDirectorySize { get; set; }

        /// <summary>
        /// 服务器状态
        /// </summary>
        [JsonProperty("server_state")]
        public string ServerState { get; set; }

        /// <summary>
        ///
    }

    public class ClientResponseStats
    {
        [JsonPropertyName("last_buffer_size")]
        public int LastBufferSize { get; set; }

        [JsonPropertyName("min_buffer_size")]
        public int MinBufferSize { get; set; }

        [JsonPropertyName("max_buffer_size")]
        public int MaxBufferSize { get; set; }
    }

    public class ClientResponse
    {
        [JsonPropertyName("last_buffer_size")]
        public int LastBufferSize { get; set; }

        [JsonPropertyName("min_buffer_size")]
        public int MinBufferSize { get; set; }

        [JsonPropertyName("max_buffer_size")]
        public int MaxBufferSize { get; set; }
    }
}