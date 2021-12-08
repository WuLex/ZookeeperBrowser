//====================================================================================================
//The Free Edition of Java to C# Converter limits conversion output to 100 lines per file.

//To purchase the Premium Edition, visit our website:
//https://www.tangiblesoftwaresolutions.com/order/order-java-to-csharp.html
//====================================================================================================

using System;
using System.Collections.Generic;
using System.Threading;

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
namespace org.apache.zookeeper.inspector.gui
{


	using ZooInspectorNodeViewer = org.apache.zookeeper.inspector.gui.nodeviewer.ZooInspectorNodeViewer;
	using LoggerFactory = org.apache.zookeeper.inspector.logger.LoggerFactory;
	using ZooInspectorManager = org.apache.zookeeper.inspector.manager.ZooInspectorManager;

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public class ZooInspectorPanel : JPanel, NodeViewersChangeListener
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			listeners.Add(this);
		}

		private readonly JButton refreshButton;
		private readonly JButton disconnectButton;
		private readonly JButton connectButton;
		private readonly ZooInspectorNodeViewersPanel nodeViewersPanel;
		private readonly ZooInspectorTreeViewer treeViewer;
		private readonly ZooInspectorManager zooInspectorManager;
		private readonly JButton addNodeButton;
		private readonly JButton deleteNodeButton;
		private readonly JButton nodeViewersButton;
		private readonly JButton aboutButton;
		private readonly IList<NodeViewersChangeListener> listeners = new List<NodeViewersChangeListener>();

		/// <param name="zooInspectorManager"> </param>
		public ZooInspectorPanel(in ZooInspectorManager zooInspectorManager)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			this.zooInspectorManager = zooInspectorManager;
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final java.util.ArrayList<org.apache.zookeeper.inspector.gui.nodeviewer.ZooInspectorNodeViewer> nodeViewers = new java.util.ArrayList<org.apache.zookeeper.inspector.gui.nodeviewer.ZooInspectorNodeViewer>();
			List<ZooInspectorNodeViewer> nodeViewers = new List<ZooInspectorNodeViewer>();
			try
			{
				IList<string> defaultNodeViewersClassNames = this.zooInspectorManager.DefaultNodeViewerConfiguration;
				foreach (string className in defaultNodeViewersClassNames)
				{
					nodeViewers.Add((ZooInspectorNodeViewer)System.Activator.CreateInstance(Type.GetType(className)));
				}
			}
			catch (Exception ex)
			{
				LoggerFactory.Logger.error("Error loading default node viewers.", ex);
				JOptionPane.showMessageDialog(ZooInspectorPanel.this, "Error loading default node viewers: " + ex.Message, "Error", JOptionPane.ERROR_MESSAGE);
			}
			nodeViewersPanel = new ZooInspectorNodeViewersPanel(zooInspectorManager, nodeViewers);
			treeViewer = new ZooInspectorTreeViewer(zooInspectorManager, nodeViewersPanel);
			this.setLayout(new BorderLayout());
			JToolBar toolbar = new JToolBar();
			toolbar.setFloatable(false);
			connectButton = new JButton(ZooInspectorIconResources.ConnectIcon);
			disconnectButton = new JButton(ZooInspectorIconResources.DisconnectIcon);
			refreshButton = new JButton(ZooInspectorIconResources.RefreshIcon);
			addNodeButton = new JButton(ZooInspectorIconResources.AddNodeIcon);
			deleteNodeButton = new JButton(ZooInspectorIconResources.DeleteNodeIcon);
			nodeViewersButton = new JButton(ZooInspectorIconResources.ChangeNodeViewersIcon);
			aboutButton = new JButton(ZooInspectorIconResources.InformationIcon);
			toolbar.add(connectButton);
			toolbar.add(disconnectButton);
			toolbar.add(refreshButton);
			toolbar.add(addNodeButton);
			toolbar.add(deleteNodeButton);
			toolbar.add(nodeViewersButton);
			toolbar.add(aboutButton);
			aboutButton.setEnabled(true);
			connectButton.setEnabled(true);
			disconnectButton.setEnabled(false);
			refreshButton.setEnabled(false);
			addNodeButton.setEnabled(false);
			deleteNodeButton.setEnabled(false);
			nodeViewersButton.setEnabled(true);
			nodeViewersButton.setToolTipText("Change Node Viewers");
			aboutButton.setToolTipText("About ZooInspector");
			connectButton.setToolTipText("Connect");
			disconnectButton.setToolTipText("Disconnect");
			refreshButton.setToolTipText("Refresh");
			addNodeButton.setToolTipText("Add Node");
			deleteNodeButton.setToolTipText("Delete Node");
			connectButton.addActionListener(new ActionListenerAnonymousInnerClass(this, zooInspectorManager));
			disconnectButton.addActionListener(new ActionListenerAnonymousInnerClass2(this));
			refreshButton.addActionListener(new ActionListenerAnonymousInnerClass3(this));
			addNodeButton.addActionListener(new ActionListenerAnonymousInnerClass4(this));
			deleteNodeButton.addActionListener(new ActionListenerAnonymousInnerClass5(this));
			nodeViewersButton.addActionListener(new ActionListenerAnonymousInnerClass6(this, zooInspectorManager, nodeViewers));
			aboutButton.addActionListener(new ActionListenerAnonymousInnerClass7(this));
			JScrollPane treeScroller = new JScrollPane(treeViewer);
			JSplitPane splitPane = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT, treeScroller, nodeViewersPanel);
			splitPane.setResizeWeight(0.25);
			this.add(splitPane, BorderLayout.CENTER);
			this.add(toolbar, BorderLayout.NORTH);
		}

		private class ActionListenerAnonymousInnerClass : ActionListener
		{
			private readonly ZooInspectorPanel outerInstance;

			private ZooInspectorManager zooInspectorManager;

			public ActionListenerAnonymousInnerClass(ZooInspectorPanel outerInstance, ZooInspectorManager zooInspectorManager)
			{
				this.outerInstance = outerInstance;
				this.zooInspectorManager = zooInspectorManager;
			}

			public void actionPerformed(ActionEvent e)
			{
				ZooInspectorConnectionPropertiesDialog zicpd = new ZooInspectorConnectionPropertiesDialog(zooInspectorManager.ConnectionPropertiesTemplate, outerInstance);
				zicpd.setVisible(true);
			}
		}

		private class ActionListenerAnonymousInnerClass2 : ActionListener
		{
			private readonly ZooInspectorPanel outerInstance;

			public ActionListenerAnonymousInnerClass2(ZooInspectorPanel outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public void actionPerformed(ActionEvent e)
			{
				outerInstance.disconnect();
			}
		}

		private class ActionListenerAnonymousInnerClass3 : ActionListener
		{
			private readonly ZooInspectorPanel outerInstance;

			public ActionListenerAnonymousInnerClass3(ZooInspectorPanel outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public void actionPerformed(ActionEvent e)
			{

//====================================================================================================
//End of the allowed output for the Free Edition of Java to C# Converter.

//To purchase the Premium Edition, visit our website:
//https://www.tangiblesoftwaresolutions.com/order/order-java-to-csharp.html
//====================================================================================================
