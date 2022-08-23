
namespace ZookeeperBrowser.ViewModel
{
    public class CnnString 
    {
        public string Name { get; set; }

        public string Value { get; private set; }

        public CnnString(string name, string value)
        {
            Name = name;
            Value = value;
        }   
    }
}
