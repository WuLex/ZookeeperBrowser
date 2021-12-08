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
	public class NodeViewerACL : ZooInspectorNodeViewer
	{
		private ZooInspectorNodeManager zooInspectorManager;
		private readonly JPanel aclDataPanel;
		private string selectedNode;

		/// 
		public NodeViewerACL()
		{
			this.setLayout(new BorderLayout());
			this.aclDataPanel = new JPanel();
			this.aclDataPanel.setBackground(Color.WHITE);
			JScrollPane scroller = new JScrollPane(this.aclDataPanel);
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
				return "Node ACLs";
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
			this.aclDataPanel.removeAll();
			if (selectedNodes.Count > 0)
			{
				this.selectedNode = selectedNodes[0];
				SwingWorker<IList<IDictionary<string, string>>, Void> worker = new SwingWorkerAnonymousInnerClass(this);
				worker.execute();
			}
		}

		private class SwingWorkerAnonymousInnerClass : SwingWorker<IList<IDictionary<string, string>>, Void>
		{
			private readonly NodeViewerACL outerInstance;

			public SwingWorkerAnonymousInnerClass(NodeViewerACL outerInstance)
			{
				this.outerInstance = outerInstance;
			}


//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override protected java.util.List<java.util.Map<String, String>> doInBackground() throws Exception
			protected internal override IList<IDictionary<string, string>> doInBackground()
			{
				return outerInstance.zooInspectorManager.getACLs(outerInstance.selectedNode);
			}

			protected internal override void done()
			{
				IList<IDictionary<string, string>> acls = null;
				try
				{
					acls = get();
				}
				catch (InterruptedException e)
				{
					acls = new List<IDictionary<string, string>>();
					LoggerFactory.Logger.error("Error retrieving ACL Information for node: " + outerInstance.selectedNode, e);
				}
				catch (ExecutionException e)
				{
					acls = new List<IDictionary<string, string>>();
					LoggerFactory.Logger.error("Error retrieving ACL Information for node: " + outerInstance.selectedNode, e);
				}
				int numRows = acls.Count * 2 + 1;
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
				outerInstance.aclDataPanel.setLayout(new TableLayout(new double[] {10, TableLayout.PREFERRED, 10}, rows));
				int j = 0;
				foreach (IDictionary<string, string> data in acls)
				{
					int rowPos = 2 * j + 1;
					JPanel aclPanel = new JPanel();
					aclPanel.setBorder(BorderFactory.createLineBorder(Color.BLACK));
					aclPanel.setBackground(Color.WHITE);
					int numRowsACL = data.Count * 2 + 1;
					double[] rowsACL = new double[numRowsACL];
					for (int i = 0; i < numRowsACL; i++)
					{
						if (i % 2 == 0)
						{
							rowsACL[i] = 5;
						}
						else
						{
							rowsACL[i] = TableLayout.PREFERRED;
						}
					}
					aclPanel.setLayout(new TableLayout(new double[] {10, TableLayout.PREFERRED, 5, TableLayout.PREFERRED, 10}, rowsACL));
					int i = 0;
					foreach (KeyValuePair<string, string> entry in data.SetOfKeyValuePairs())
					{
						int rowPosACL = 2 * i + 1;
						JLabel label = new JLabel(entry.Key);
						JTextField text = new JTextField(entry.Value);
						text.setEditable(false);
						aclPanel.add(label, "1," + rowPosACL);
						aclPanel.add(text, "3," + rowPosACL);
						i++;
					}
					outerInstance.aclDataPanel.add(aclPanel, "1," + rowPos);
				}
				outerInstance.aclDataPanel.revalidate();
				outerInstance.aclDataPanel.repaint();
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