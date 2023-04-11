namespace ZookeeperBrowser.Models.Commands
{
    public class MonitorData
    {
        public int RequestCount { get; set; }
        public double AverageLatency { get; set; }
        public double MinLatency { get; set; }
        public double MaxLatency { get; set; }
        public double ReceivedBytes { get; set; }
        public double SentBytes { get; set; }
    }

    public class ConfigurationData
    {
        public string Version { get; set; }
        public string BuildDate { get; set; }
        public string GitHash { get; set; }
    }

    public class ServerStatsData
    {
        public int Connections { get; set; }
        public int OutstandingRequests { get; set; }
    }

    public class StatsData
    {
        public int NodeCount { get; set; }
        public int WatchCount { get; set; }
        public int EphemeralsCount { get; set; }
        public int ApproximateDataSize { get; set; }
    }
}
