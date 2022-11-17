namespace ZookeeperBrowser.Dtos
{
    public class TreeDataModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string href { get; set; }
        public bool Checked { get; set; }
        public bool disabled { get; set; }
        public bool spread { get; set; }

        public List<TreeDataModel> children { get; set; }
    }
}