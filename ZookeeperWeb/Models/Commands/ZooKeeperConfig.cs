namespace ZookeeperBrowser.Models.Commands
{
    public class ZooKeeperConfig
    {
        public string ClientPort { get; set; }
        public string DataDir { get; set; }
        public int TickTime { get; set; }
        public int MinSessionTimeout { get; set; }
        public int MaxSessionTimeout { get; set; }
        public int SyncLimit { get; set; }
        public int InitLimit { get; set; }
        public string SnapshotSizeFactor { get; set; }
        public string ElectionAlg { get; set; }
        public string QuorumListenOnAllIPs { get; set; }
        public string QuorumCnxnTimeoutMs { get; set; }
        public string QuorumPort { get; set; }
        public string QuorumLearnerPort { get; set; }
        public string QuorumElectionPort { get; set; }
        public string QuorumPeerPortUnsecure { get; set; }
        public string QuorumCertProviderEnabled { get; set; }
        public string QuorumSaslEnabled { get; set; }
        public string QuorumServerSaslRequired { get; set; }
        public string QuorumLearnerSaslRequired { get; set; }
        public string QuorumSaslAuthRequired { get; set; }
        public string QuorumSaslAuthEnabled { get; set; }
        public string QuorumCnxnThreadsSize { get; set; }
        public string QuorumSnapSyncThreads { get; set; }
        public string QuorumSnapshotSizeFactor { get; set; }
        public string QuorumInitLimit { get; set; }
        public string QuorumSyncLimit { get; set; }
        public string QuorumFollowerSyncLimit { get; set; }
        public string QuorumMaxInFlightCommits { get; set; }
        public string QuorumLeaderServes { get; set; }
        public string QuorumDistributedLogEnabled { get; set; }
        public string QuorumDistributedLogReplayIntervalSeconds { get; set; }
        public string QuorumDistributedLogBufferSize { get; set; }
        public string QuorumDistributedLogMaxLogSize { get; set; }
        public string QuorumDistributedLogMaxEntrySize { get; set; }
        public string QuorumDistributedLogMaxLogChunks { get; set; }
        public string QuorumDistributedLogRemoveInactiveSegments { get; set; }
        public string QuorumDistributedLogMaxCommitQueued { get; set; }
        public string QuorumDistributedLogSegmentDeleteDelaySeconds { get; set; }
        public string QuorumDistributedLogServerId { get; set; }
        public string QuorumDistributedLogStartDelaySeconds { get; set; }
        public string QuorumEnableSslQuorum { get; set; }
        public string QuorumTlsProvider { get; set; }
        public string QuorumKeyStoreLocation { get; set; }
        public string QuorumKeyStorePassword { get; set; }
        public string QuorumTrustStoreLocation { get; set; }
        public string QuorumTrustStorePassword { get; set; }
        public string QuorumCertAlias { get; set; }
        public string QuorumSecureClientPort { get; set; }
        public string QuorumServerSslEnabled { get; set; }
        public string QuorumRequireClientAuthScheme { get; set; }
    }
}
