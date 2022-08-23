namespace ZookeeperBrowser.Dtos
{
    public class ZTreeDataModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Pid { get; set; }

        public bool IsParent { get; set; } = true;

        public bool open { get; set; } = false;
    }
}