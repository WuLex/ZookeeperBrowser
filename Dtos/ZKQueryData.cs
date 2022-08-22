namespace ZookeeperBrowser.Dtos
{
    public class ZKQueryData
    {
            /// <summary>
            /// zookeeper节点路径
            /// </summary>
       public string nodepath { get; set; }

       /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页数据量
        /// </summary>
        public int Limit { get; set; }
    }
}
