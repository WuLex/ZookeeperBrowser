var App = (function () {
    function App() {
        this.isInit = true;
        this.Init();
    }
    App.prototype.Init = function () {
        var that = this;
        var connStr = localStorage.getItem("ConnString");
        if (connStr == null || connStr == "null" || connStr=="") {
            connStr = "127.0.0.1";
        }
        this.vmApp = new Vue({
            el: '#app',
            data: {
                zooKeeperHost: connStr,
                zNodes: [],
                currentNode: {},
                opData: { data: '' }
            },
            mounted: function () {
                this.$nextTick(function () {
                });
            },
            methods: {
                RefreshNodes: function () {
                    that.Connect();
                },
                Connect: function () {
                    that.RefreshNodes();
                },
                DisConnect: function () {
                    that.DisConnect();
                },
                Create: function () {
                    that.Create();
                },
                Set: function () {
                    that.Set();
                },
                Delete: function () {
                    that.Delete();
                }
            }
        });
    };
    App.prototype.Connect = function () {
        localStorage.setItem('ConnString', this.vmApp.$data.zooKeeperHost);
        this.RefreshNodes();
    };
    App.prototype.DisConnect = function () {
        var that = this;
        that.Api('DisConnect', {}, function () {
            $('#zNodes').treeview('remove');
            that.vmApp.$data.currentNode = {};
        });
    };
    App.prototype.Get = function () {
        var that = this;
        that.Api('Get', that.vmApp.$data.opData, function (resp) {
            that.vmApp.$data.opData.data = resp.body.data;
        });
    };
    App.prototype.Delete = function () {
        var that = this;
        that.Api('Delete', { path: that.vmApp.$data.currentNode.path }, function () {
            setTimeout(function () { that.RefreshNodes(); }, 500);
        });
    };
    App.prototype.Set = function () {
        var that = this;
        that.Api('Set', that.vmApp.$data.opData, function () {
            setTimeout(function () { that.RefreshNodes(); }, 500);
        });
    };
    App.prototype.Create = function () {
        var that = this;
        that.Api('Create', that.vmApp.$data.opData, function () {
            setTimeout(function () { that.RefreshNodes(); }, 500);
        });
    };
    App.prototype.RefreshNodes = function () {
        var that = this;
        that.vmApp.$data.currentNode = {};
        var reqBody = {
            "parentPath": "/"
        };
        that.Api('GetChildren', reqBody, function (resp) {
            if (!that.isInit) {
                $('#zNodes').treeview('remove');
            }
            that.vmApp.$data.zNodes = resp.body.children;
            var $zNodesTree = $('#zNodes').treeview({
                data: resp.body.children
            });
            that.isInit = false;
            $('#zNodes').on('nodeSelected', function (event, data) {
                that.vmApp.$data.currentNode = data;
                that.vmApp.$data.opData.path = data.path;
                that.Get();
            });
        });
    };
    App.prototype.Api = function (action, reqBody, success) {
        var that = this;
        var reqMsg = {
            "header": {
                "connectString": that.vmApp.$data.zooKeeperHost
            },
            "body": reqBody
        };
        $.ajax({
            url: '/ZKManager/' + action,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            data: JSON.stringify(reqMsg),
            success: success
        });
    };
    return App;
}());
var RequestMessage = (function () {
    function RequestMessage() {
    }
    return RequestMessage;
}());
var RequestHeader = (function () {
    function RequestHeader() {
    }
    return RequestHeader;
}());
