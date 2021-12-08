//====================================================================================================
//The Free Edition of Java to C# Converter limits conversion output to 100 lines per file.

//To purchase the Premium Edition, visit our website:
//https://www.tangiblesoftwaresolutions.com/order/order-java-to-csharp.html
//====================================================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/*
 * ZooInspector
 * 
 * Copyright 2010 Colin Goodheart-Smithe

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */
namespace org.apache.zookeeper.inspector.manager
{

	using CreateMode = org.apache.zookeeper.CreateMode;
	using KeeperException = org.apache.zookeeper.KeeperException;
	using WatchedEvent = org.apache.zookeeper.WatchedEvent;
	using Watcher = org.apache.zookeeper.Watcher;
	using ZooKeeper = org.apache.zookeeper.ZooKeeper;
	using EventType = org.apache.zookeeper.Watcher.Event.EventType;
	using Ids = org.apache.zookeeper.ZooDefs.Ids;
	using Perms = org.apache.zookeeper.ZooDefs.Perms;
	using ACL = org.apache.zookeeper.data.ACL;
	using Stat = org.apache.zookeeper.data.Stat;
	using BasicDataEncryptionManager = org.apache.zookeeper.inspector.encryption.BasicDataEncryptionManager;
	using DataEncryptionManager = org.apache.zookeeper.inspector.encryption.DataEncryptionManager;
	using LoggerFactory = org.apache.zookeeper.inspector.logger.LoggerFactory;
	using ZooKeeperRetry = org.apache.zookeeper.retry.ZooKeeperRetry;

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public class ZooInspectorManagerImpl : ZooInspectorManager
	{
		private const string A_VERSION = "ACL Version";
		private const string C_TIME = "Creation Time";
		private const string C_VERSION = "Children Version";
		private const string CZXID = "Creation ID";
		private const string DATA_LENGTH = "Data Length";
		private const string EPHEMERAL_OWNER = "Ephemeral Owner";
		private const string M_TIME = "Last Modified Time";
		private const string MZXID = "Modified ID";
		private const string NUM_CHILDREN = "Number of Children";
		private const string PZXID = "Node ID";
		private const string VERSION = "Data Version";
		private const string ACL_PERMS = "Permissions";
		private const string ACL_SCHEME = "Scheme";
		private const string ACL_ID = "Id";
		private const string SESSION_STATE = "Session State";
		private const string SESSION_ID = "Session ID";
		/// 
		public const string CONNECT_STRING = "hosts";
		/// 
		public const string SESSION_TIMEOUT = "timeout";
		/// 
		public const string DATA_ENCRYPTION_MANAGER = "encryptionManager";

		private static readonly File defaultsFile = new File("./config/defaultNodeVeiwers.cfg");

		private DataEncryptionManager encryptionManager;
		private string connectString;
		private int sessionTimeout;
		private ZooKeeper zooKeeper;
		private readonly IDictionary<string, NodeWatcher> watchers = new Dictionary<string, NodeWatcher>();

		public virtual bool connect(Properties connectionProps)
		{
			try
			{
				if (this.zooKeeper == null)
				{
					string connectString = connectionProps.getProperty(CONNECT_STRING);
					string sessionTimeout = connectionProps.getProperty(SESSION_TIMEOUT);
					string encryptionManager = connectionProps.getProperty(DATA_ENCRYPTION_MANAGER);
					if (string.ReferenceEquals(connectString, null) || string.ReferenceEquals(sessionTimeout, null))
					{
						throw new System.ArgumentException("Both connect string and session timeout are required.");
					}
					if (string.ReferenceEquals(encryptionManager, null))
					{
						this.encryptionManager = new BasicDataEncryptionManager();
					}
					else
					{
						Type clazz = Type.GetType(encryptionManager);

						if (Arrays.asList(clazz.GetInterfaces()).contains(typeof(DataEncryptionManager)))
						{
							this.encryptionManager = (DataEncryptionManager)System.Activator.CreateInstance(Type.GetType(encryptionManager));
						}
						else
						{
							throw new System.ArgumentException("Data encryption manager must implement DataEncryptionManager interface");
						}
					}
					this.connectString = connectString;
					this.sessionTimeout = Convert.ToInt32(sessionTimeout);
					this.zooKeeper = new ZooKeeperRetry(connectString, Convert.ToInt32(sessionTimeout), null);
					((ZooKeeperRetry) this.zooKeeper).RetryLimit = 10;
					return ((ZooKeeperRetry) this.zooKeeper).testConnection();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
			return false;
		}

		public virtual bool disconnect()
		{
			try
			{
				if (this.zooKeeper != null)
				{
					this.zooKeeper.close();
					this.zooKeeper = null;
					return true;
				}
			}
			catch (Exception e)
			{
				LoggerFactory.Logger.error("Error occurred while disconnecting from ZooKeeper server", e);
			}
			return false;
		}

		public virtual IList<string> getChildren(string nodePath)
		{
			try
			{

				return zooKeeper.getChildren(nodePath, false);
			}
			catch (Exception e)
			{
				LoggerFactory.Logger.error("Error occurred retrieving children of node: " + nodePath, e);
			}
			return null;
		}

		public virtual string getData(string nodePath)
		{
			try
			{
				if (nodePath.Length == 0)
				{
					nodePath = "/";
				}
				Stat s = zooKeeper.exists(nodePath, false);
				if (s != null)
				{
					return this.encryptionManager.decryptData(zooKeeper.getData(nodePath, false, s));
				}
			}
			catch (Exception e)
			{
				LoggerFactory.Logger.error("Error occurred getting data for node: " + nodePath, e);
			}
			return null;
		}

		public virtual string getNodeChild(string nodePath, int childIndex)
		{
			try
			{
				Stat s = zooKeeper.exists(nodePath, false);
				if (s != null)
				{
					return this.zooKeeper.getChildren(nodePath, false).get(childIndex);
				}
			}

//====================================================================================================
//End of the allowed output for the Free Edition of Java to C# Converter.

//To purchase the Premium Edition, visit our website:
//https://www.tangiblesoftwaresolutions.com/order/order-java-to-csharp.html
//====================================================================================================
