//====================================================================================================
//The Free Edition of Java to C# Converter limits conversion output to 100 lines per file.

//To purchase the Premium Edition, visit our website:
//https://www.tangiblesoftwaresolutions.com/order/order-java-to-csharp.html
//====================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;

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
namespace org.apache.zookeeper.retry
{

	using CreateMode = org.apache.zookeeper.CreateMode;
	using KeeperException = org.apache.zookeeper.KeeperException;
	using Watcher = org.apache.zookeeper.Watcher;
	using ZooKeeper = org.apache.zookeeper.ZooKeeper;
	using ACL = org.apache.zookeeper.data.ACL;
	using Stat = org.apache.zookeeper.data.Stat;
	using LoggerFactory = org.apache.zookeeper.inspector.logger.LoggerFactory;

	/// <summary>
	/// @author Colin
	/// 
	/// </summary>
	public class ZooKeeperRetry : ZooKeeper
	{

		private bool closed = false;
		private readonly Watcher watcher;
		private int limit = -1;

		/// <param name="connectString"> </param>
		/// <param name="sessionTimeout"> </param>
		/// <param name="watcher"> </param>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public ZooKeeperRetry(String connectString, int sessionTimeout, org.apache.zookeeper.Watcher watcher) throws java.io.IOException
		public ZooKeeperRetry(string connectString, int sessionTimeout, Watcher watcher) : base(connectString, sessionTimeout, watcher)
		{
			this.watcher = watcher;
		}

		/// <param name="connectString"> </param>
		/// <param name="sessionTimeout"> </param>
		/// <param name="watcher"> </param>
		/// <param name="sessionId"> </param>
		/// <param name="sessionPasswd"> </param>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public ZooKeeperRetry(String connectString, int sessionTimeout, org.apache.zookeeper.Watcher watcher, long sessionId, byte[] sessionPasswd) throws java.io.IOException
		public ZooKeeperRetry(string connectString, int sessionTimeout, Watcher watcher, long sessionId, sbyte[] sessionPasswd) : base(connectString, sessionTimeout, watcher, sessionId, sessionPasswd)
		{
			this.watcher = watcher;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override public synchronized void close() throws InterruptedException
		public override void close()
		{
			lock (this)
			{
				this.closed = true;
				base.close();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override public String create(String path, byte[] data, java.util.List<org.apache.zookeeper.data.ACL> acl, org.apache.zookeeper.CreateMode createMode) throws KeeperException, InterruptedException
		public override string create(string path, sbyte[] data, IList<ACL> acl, CreateMode createMode)
		{
			int count = 0;
			do
			{
				try
				{
					return base.create(path, data, acl, createMode);
				}
				catch (KeeperException.ConnectionLossException)
				{
					LoggerFactory.Logger.warn("ZooKeeper connection lost.  Trying to reconnect.");
					if (exists(path, false) != null)
					{
						return path;
					}
				}
				catch (KeeperException.NodeExistsException)
				{
					return path;
				}
			} while (!closed && (limit == -1 || count++ < limit));
			return null;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override public void delete(String path, int version) throws InterruptedException, org.apache.zookeeper.KeeperException
		public override void delete(string path, int version)
		{
			int count = 0;
			do
			{
				try
				{
					base.delete(path, version);
				}
				catch (KeeperException.ConnectionLossException)
				{
					LoggerFactory.Logger.warn("ZooKeeper connection lost.  Trying to reconnect.");
					if (exists(path, false) == null)
					{
						return;
					}
				}
				catch (KeeperException.NoNodeException)
				{
					return;
				}
			} while (!closed && (limit == -1 || count++ < limit));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override public org.apache.zookeeper.data.Stat exists(String path, boolean watch) throws KeeperException, InterruptedException
		public override Stat exists(string path, bool watch)
		{
			int count = 0;
			do
			{
				try
				{
					return base.exists(path, watch ? watcher : null);
				}
				catch (KeeperException.ConnectionLossException)
				{
					LoggerFactory.Logger.warn("ZooKeeper connection lost.  Trying to reconnect.");
				}
			} while (!closed && (limit == -1 || count++ < limit));
			return null;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override public org.apache.zookeeper.data.Stat exists(String path, org.apache.zookeeper.Watcher watcher) throws KeeperException, InterruptedException
		public override Stat exists(string path, Watcher watcher)
		{
			int count = 0;
			do
			{
				try
				{
					return base.exists(path, watcher);
				}
				catch (KeeperException.ConnectionLossException)
				{
					LoggerFactory.Logger.warn("ZooKeeper connection lost.  Trying to reconnect.");
				}
			} while (!closed && (limit == -1 || count++ < limit));
			return null;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override public java.util.List<org.apache.zookeeper.data.ACL> getACL(String path, org.apache.zookeeper.data.Stat stat) throws KeeperException, InterruptedException
		public override IList<ACL> getACL(string path, Stat stat)
		{
			int count = 0;
			do
			{
				try
				{
					return base.getACL(path, stat);
				}
				catch (KeeperException.ConnectionLossException)
				{
					LoggerFactory.Logger.warn("ZooKeeper connection lost.  Trying to reconnect.");
				}
			} while (!closed && (limit == -1 || count++ < limit));
			return null;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override public java.util.List<String> getChildren(String path, boolean watch) throws KeeperException, InterruptedException
		public override IList<string> getChildren(string path, bool watch)
		{
			int count = 0;
			do
			{
				try
				{
					return base.getChildren(path, watch ? watcher : null);
				}
				catch (KeeperException.ConnectionLossException)
				{
					LoggerFactory.Logger.warn("ZooKeeper connection lost.  Trying to reconnect.");
				}
			} while (!closed && (limit == -1 || count++ < limit));
			return new List<string>();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override public java.util.List<String> getChildren(String path, org.apache.zookeeper.Watcher watcher) throws KeeperException, InterruptedException
		public override IList<string> getChildren(string path, Watcher watcher)
		{
			int count = 0;
			do
			{
				try
				{
					return base.getChildren(path, watcher);
				}
				catch (KeeperException.ConnectionLossException)
				{
					LoggerFactory.Logger.warn("ZooKeeper connection lost.  Trying to reconnect.");
				}
			} while (!closed && (limit == -1 || count++ < limit));
			return new List<string>();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override public byte[] getData(String path, boolean watch, org.apache.zookeeper.data.Stat stat) throws KeeperException, InterruptedException
		public override sbyte[] getData(string path, bool watch, Stat stat)
		{
			int count = 0;
			do
			{
				try
				{
					return base.getData(path, watch ? watcher : null, stat);
				}
				catch (KeeperException.ConnectionLossException)
				{
					LoggerFactory.Logger.warn("ZooKeeper connection lost.  Trying to reconnect.");
				}

//====================================================================================================
//End of the allowed output for the Free Edition of Java to C# Converter.

//To purchase the Premium Edition, visit our website:
//https://www.tangiblesoftwaresolutions.com/order/order-java-to-csharp.html
//====================================================================================================
