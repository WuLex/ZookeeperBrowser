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
	using TableLayout = info.clearthought.layout.TableLayout;



	using LoggerFactory = org.apache.zookeeper.inspector.logger.LoggerFactory;
	using ZooInspectorNodeManager = org.apache.zookeeper.inspector.manager.ZooInspectorNodeManager;

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public class NodeViewerMetaData : ZooInspectorNodeViewer
	{
		private ZooInspectorNodeManager zooInspectorManager;
		private readonly JPanel metaDataPanel;
		private string selectedNode;

		/// 
		public NodeViewerMetaData()
		{
			this.setLayout(new BorderLayout());
			this.metaDataPanel = new JPanel();
			this.metaDataPanel.setBackground(Color.WHITE);
			JScrollPane scroller = new JScrollPane(this.metaDataPanel);
			this.add(scroller, BorderLayout.CENTER);
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
				return "Node Metadata";
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
			this.metaDataPanel.removeAll();
			if (selectedNodes.Count > 0)
			{
				this.selectedNode = selectedNodes[0];
				SwingWorker<IDictionary<string, string>, Void> worker = new SwingWorkerAnonymousInnerClass(this);
				worker.execute();
			}
		}

		private class SwingWorkerAnonymousInnerClass : SwingWorker<IDictionary<string, string>, Void>
		{
			private readonly NodeViewerMetaData outerInstance;

			public SwingWorkerAnonymousInnerClass(NodeViewerMetaData outerInstance)
			{
				this.outerInstance = outerInstance;
			}


//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override protected java.util.Map<String, String> doInBackground() throws Exception
			protected internal override IDictionary<string, string> doInBackground()
			{
				return outerInstance.zooInspectorManager.getNodeMeta(outerInstance.selectedNode);
			}

			protected internal override void done()
			{
				IDictionary<string, string> data = null;
				try
				{
					data = get();
				}
				catch (InterruptedException e)
				{
					data = new Dictionary<string, string>();
					LoggerFactory.Logger.error("Error retrieving meta data for node: " + outerInstance.selectedNode, e);
				}
				catch (ExecutionException e)
				{
					data = new Dictionary<string, string>();
					LoggerFactory.Logger.error("Error retrieving meta data for node: " + outerInstance.selectedNode, e);
				}
				int numRows = data.Count * 2 + 1;
				double[] rows = new double[numRows];
				for (int i = 0; i < numRows; i++)
				{
					if (i % 2 == 0)
					{
						rows[i] = 5;
					}
					else
					{
						rows[i] = TableLayout.PREFERRED;
					}
				}
				outerInstance.metaDataPanel.setLayout(new TableLayout(new double[] {10, TableLayout.PREFERRED, 5, TableLayout.PREFERRED, 10}, rows));
				int i = 0;
				foreach (KeyValuePair<string, string> entry in data.SetOfKeyValuePairs())
				{
					int rowPos = 2 * i + 1;
					JLabel label = new JLabel(entry.Key);
					JTextField text = new JTextField(entry.Value);
					text.setEditable(false);
					outerInstance.metaDataPanel.add(label, "1," + rowPos);
					outerInstance.metaDataPanel.add(text, "3," + rowPos);
					i++;
				}
				outerInstance.metaDataPanel.revalidate();
				outerInstance.metaDataPanel.repaint();
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