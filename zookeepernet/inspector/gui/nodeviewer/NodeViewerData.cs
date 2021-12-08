using System.Collections.Generic;

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
namespace org.apache.zookeeper.inspector.gui.nodeviewer
{


	using ZooInspectorIconResources = org.apache.zookeeper.inspector.gui.ZooInspectorIconResources;
	using LoggerFactory = org.apache.zookeeper.inspector.logger.LoggerFactory;
	using ZooInspectorNodeManager = org.apache.zookeeper.inspector.manager.ZooInspectorNodeManager;

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public class NodeViewerData : ZooInspectorNodeViewer
	{
		private ZooInspectorNodeManager zooInspectorManager;
		private readonly JTextPane dataArea;
		private readonly JToolBar toolbar;
		private string selectedNode;

		/// 
		public NodeViewerData()
		{
			this.setLayout(new BorderLayout());
			this.dataArea = new JTextPane();
			this.toolbar = new JToolBar();
			this.toolbar.setFloatable(false);
			JScrollPane scroller = new JScrollPane(this.dataArea);
			scroller.setHorizontalScrollBarPolicy(JScrollPane.HORIZONTAL_SCROLLBAR_NEVER);
			this.add(scroller, BorderLayout.CENTER);
			this.add(this.toolbar, BorderLayout.NORTH);
			JButton saveButton = new JButton(ZooInspectorIconResources.SaveIcon);
			saveButton.addActionListener(new ActionListenerAnonymousInnerClass(this));
			this.toolbar.add(saveButton);

		}

		private class ActionListenerAnonymousInnerClass : ActionListener
		{
			private readonly NodeViewerData outerInstance;

			public ActionListenerAnonymousInnerClass(NodeViewerData outerInstance)
			{
				this.outerInstance = outerInstance;
			}


			public void actionPerformed(ActionEvent e)
			{
				if (!string.ReferenceEquals(outerInstance.selectedNode, null))
				{
					if (JOptionPane.showConfirmDialog(outerInstance, "Are you sure you want to save this node?" + " (this action cannot be reverted)", "Confirm Save", JOptionPane.YES_NO_OPTION, JOptionPane.WARNING_MESSAGE) == JOptionPane.YES_OPTION)
					{
						outerInstance.zooInspectorManager.setData(outerInstance.selectedNode, outerInstance.dataArea.getText());
					}
				}
			}
		}

		/*
		 * (non-Javadoc)
		 * 
		 * @see
		 * org.apache.zookeeper.inspector.gui.nodeviewer.ZooInspectorNodeViewer#
		 * getTitle()
		 */
		public override string Title
		{
			get
			{
				return "Node Data";
			}
		}

		/*
		 * (non-Javadoc)
		 * 
		 * @see
		 * org.apache.zookeeper.inspector.gui.nodeviewer.ZooInspectorNodeViewer#
		 * nodeSelectionChanged(java.util.Set)
		 */
		public override void nodeSelectionChanged(IList<string> selectedNodes)
		{
			if (selectedNodes.Count > 0)
			{
				this.selectedNode = selectedNodes[0];
				SwingWorker<string, Void> worker = new SwingWorkerAnonymousInnerClass(this);
				worker.execute();
			}
		}

		private class SwingWorkerAnonymousInnerClass : SwingWorker<string, Void>
		{
			private readonly NodeViewerData outerInstance;

			public SwingWorkerAnonymousInnerClass(NodeViewerData outerInstance)
			{
				this.outerInstance = outerInstance;
			}


//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override protected String doInBackground() throws Exception
			protected internal override string doInBackground()
			{
				return outerInstance.zooInspectorManager.getData(outerInstance.selectedNode);
			}

			protected internal override void done()
			{
				string data = "";
				try
				{
					data = get();
				}
				catch (InterruptedException e)
				{
					LoggerFactory.Logger.error("Error retrieving data for node: " + outerInstance.selectedNode, e);
				}
				catch (ExecutionException e)
				{
					LoggerFactory.Logger.error("Error retrieving data for node: " + outerInstance.selectedNode, e);
				}
				outerInstance.dataArea.setText(data);
			}
		}

		/*
		 * (non-Javadoc)
		 * 
		 * @see
		 * org.apache.zookeeper.inspector.gui.nodeviewer.ZooInspectorNodeViewer#
		 * setZooInspectorManager
		 * (org.apache.zookeeper.inspector.manager.ZooInspectorNodeManager)
		 */
		public override ZooInspectorNodeManager ZooInspectorManager
		{
			set
			{
				this.zooInspectorManager = value;
			}
		}

	}

}