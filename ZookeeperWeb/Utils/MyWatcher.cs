using org.apache.zookeeper;
using org.apache.zookeeper.data;

namespace ZookeeperBrowser.Utils
{
    public class MyWatcher : Watcher
    {
        public string Name { get; private set; }

        public MyWatcher(string name)
        {
            this.Name = name;
        }

        public override Task process(WatchedEvent @event)
        {
            var path = @event.getPath();
            var state = @event.getState();
            //这里仅仅只是简单的输出节点路径、监听事件响应状态和监听事件类型
            Console.WriteLine($"{Name} recieve: Path-{path} State-{@event.getState()} Type-{@event.get_Type()}");
            return Task.FromResult(0);
        }
    }
}
