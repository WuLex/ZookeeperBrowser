using System.Collections.Generic;
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
namespace org.apache.zookeeper.inspector.gui
{


	using ZooInspectorNodeViewer = org.apache.zookeeper.inspector.gui.nodeviewer.ZooInspectorNodeViewer;
	using ZooInspectorNodeManager = org.apache.zookeeper.inspector.manager.ZooInspectorNodeManager;

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public class ZooInspectorNodeViewersPanel : JPanel, TreeSelectionListener, ChangeListener
	{

		private readonly IList<ZooInspectorNodeViewer> nodeVeiwers = new List<ZooInspectorNodeViewer>();
		private readonly IList<bool> needsReload = new List<bool>();
		private readonly JTabbedPane tabbedPane;
		private readonly IList<string> selectedNodes = new List<string>();
		private readonly ZooInspectorNodeManager zooInspectorManager;

		/// <param name="zooInspectorManager"> </param>
		/// <param name="nodeVeiwers"> </param>
		public ZooInspectorNodeViewersPanel(ZooInspectorNodeManager zooInspectorManager, IList<ZooInspectorNodeViewer> nodeVeiwers)
		{
			this.zooInspectorManager = zooInspectorManager;
			this.setLayout(new BorderLayout());
			tabbedPane = new JTabbedPane(JTabbedPane.TOP, JTabbedPane.WRAP_TAB_LAYOUT);
			NodeViewers = nodeVeiwers;
			tabbedPane.addChangeListener(this);
			this.add(tabbedPane, BorderLayout.CENTER);
			reloadSelectedViewer();
		}

		/// <param name="nodeViewers"> </param>
		public virtual IList<ZooInspectorNodeViewer> NodeViewers
		{
			set
			{
				this.nodeVeiwers.Clear();
				((List<ZooInspectorNodeViewer>)this.nodeVeiwers).AddRange(value);
				needsReload.Clear();
				tabbedPane.removeAll();
				foreach (ZooInspectorNodeViewer nodeViewer in nodeVeiwers)
				{
					nodeViewer.ZooInspectorManager = zooInspectorManager;
					needsReload.Add(true);
					tabbedPane.add(nodeViewer.Title, nodeViewer);
				}
				this.revalidate();
				this.repaint();
			}
		}

		private void reloadSelectedViewer()
		{
			int index = this.tabbedPane.getSelectedIndex();
			if (index != -1 && this.needsReload[index])
			{
				ZooInspectorNodeViewer viewer = this.nodeVeiwers[index];
				viewer.nodeSelectionChanged(selectedNodes);
				this.needsReload[index] = false;
			}
		}

		public virtual void valueChanged(TreeSelectionEvent e)
		{
			TreePath[] paths = e.getPaths();
			selectedNodes.Clear();
			foreach (TreePath path in paths)
			{
				bool appended = false;
				StringBuilder sb = new StringBuilder();
				object[] pathArray = path.getPath();
				foreach (object o in pathArray)
				{
					if (o != null)
					{
						string nodeName = o.ToString();
						if (!string.ReferenceEquals(nodeName, null))
						{
							if (nodeName.Length > 0)
							{
								appended = true;
								sb.Append("/"); //$NON-NLS-1$
								sb.Append(o.ToString());
							}
						}
					}
				}
				if (appended)
				{
					selectedNodes.Add(sb.ToString());
				}
			}
			for (int i = 0; i < needsReload.Count; i++)
			{
				this.needsReload[i] = true;
			}
			reloadSelectedViewer();
		}

		public virtual void stateChanged(ChangeEvent e)
		{
			reloadSelectedViewer();
		}
	}

}