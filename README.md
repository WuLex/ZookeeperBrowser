# ZookeeperBrowser
Zookeeper node  Visualization Tool

## CoreAPI项目API地址
http://localhost:5120/api-docs/index.html

-------------------------------------------------------

## 如图:
![image](https://raw.githubusercontent.com/WuLex/UsefulPicture/main/zookeeperwebtool/coreapi.png)

## ZookeeperBrowser项目地址
http://localhost:5264/Login/Index

-------------------------------------------------------

## 如图:
![image](https://raw.githubusercontent.com/WuLex/UsefulPicture/main/zookeeperwebtool/login.png)


![image](https://raw.githubusercontent.com/WuLex/UsefulPicture/main/zookeeperwebtool/home.png)


![image](https://raw.githubusercontent.com/WuLex/UsefulPicture/main/zookeeperwebtool/index.png)

-------------------------------------------------------
##  调用AdminServer提供 HTTP 接口返回数据
3.5.0 中的新功能： `AdminServer` 是一个嵌入式 Jetty 服务器，它为四个字母的单词命令提供 HTTP 接口。默认情况下，服务器在端口 8080 上启动，并通过转到 URL“/commands/[命令名称]”发出命令，例如 `http://localhost:8080/commands/stat`。命令响应以 JSON 形式返回。与原始协议不同，命令不限于四个字母的名称，命令可以有多个名称；例如，“stmk”也可以称为“`set_trace_mask`”。要查看所有可用命令的列表，请将浏览器指向 URL `/commands`（例如，`http://localhost:8080/commands`）。

 1. http://localhost:8080/commands
 2. http://localhost:8080/commands/configuration
 3. http://localhost:8080/commands/monitor
 4. http://localhost:8080/commands/server_stats
 5. http://localhost:8080/commands/stats

![image](https://raw.githubusercontent.com/WuLex/UsefulPicture/main/zookeeperwebtool/commands.png)

