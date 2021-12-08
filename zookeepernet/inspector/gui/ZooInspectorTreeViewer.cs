//====================================================================================================
//The Free Edition of Java to C# Converter limits conversion output to 100 lines per file.

//To purchase the Premium Edition, visit our website:
//https://www.tangiblesoftwaresolutions.com/order/order-java-to-csharp.html
//====================================================================================================

using System;
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


	using NodeListener = org.apache.zookeeper.inspector.manager.NodeListener;
	using ZooInspectorManager = org.apache.zookeeper.inspector.manager.ZooInspectorManager;

	using Toaster = com.nitido.utils.toaster.Toaster;

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public class ZooInspectorTreeViewer : JPanel, NodeListener
	{
		private readonly ZooInspectorManager zooInspectorManager;
		private readonly JTree tree;
		private readonly Toaster toasterManager;

		/// <param name="zooInspectorManager"> </param>
		/// <param name="listener"> </param>
		public ZooInspectorTreeViewer(in ZooInspectorManager zooInspectorManager, TreeSelectionListener listener)
		{
			this.zooInspectorManager = zooInspectorManager;
			this.setLayout(new BorderLayout());
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final javax.swing.JPopupMenu popupMenu = new javax.swing.JPopupMenu();
			JPopupMenu popupMenu = new JPopupMenu();
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final javax.swing.JMenuItem addNotify = new javax.swing.JMenuItem("Add Change Notification");
			JMenuItem addNotify = new JMenuItem("Add Change Notification");
			this.toasterManager = new Toaster();
			this.toasterManager.setBorderColor(Color.BLACK);
			this.toasterManager.setMessageColor(Color.BLACK);
			this.toasterManager.setToasterColor(Color.WHITE);
			addNotify.addActionListener(new ActionListenerAnonymousInnerClass(this, zooInspectorManager));
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final javax.swing.JMenuItem removeNotify = new javax.swing.JMenuItem("Remove Change Notification");
			JMenuItem removeNotify = new JMenuItem("Remove Change Notification");
			removeNotify.addActionListener(new ActionListenerAnonymousInnerClass2(this, zooInspectorManager));
			tree = new JTree(new DefaultMutableTreeNode());
			tree.setCellRenderer(new ZooInspectorTreeCellRenderer(this));
			tree.setEditable(false);
			tree.getSelectionModel().addTreeSelectionListener(listener);
			tree.addMouseListener(new MouseAdapterAnonymousInnerClass(this, popupMenu, addNotify, removeNotify));
			this.add(tree, BorderLayout.CENTER);
		}

		private class ActionListenerAnonymousInnerClass : ActionListener
		{
			private readonly ZooInspectorTreeViewer outerInstance;

			private ZooInspectorManager zooInspectorManager;

			public ActionListenerAnonymousInnerClass(ZooInspectorTreeViewer outerInstance, ZooInspectorManager zooInspectorManager)
			{
				this.outerInstance = outerInstance;
				this.zooInspectorManager = zooInspectorManager;
			}

			public void actionPerformed(ActionEvent e)
			{
				IList<string> selectedNodes = outerInstance.SelectedNodes;
				zooInspectorManager.addWatchers(selectedNodes, outerInstance);
			}
		}

		private class ActionListenerAnonymousInnerClass2 : ActionListener
		{
			private readonly ZooInspectorTreeViewer outerInstance;

			private ZooInspectorManager zooInspectorManager;

			public ActionListenerAnonymousInnerClass2(ZooInspectorTreeViewer outerInstance, ZooInspectorManager zooInspectorManager)
			{
				this.outerInstance = outerInstance;
				this.zooInspectorManager = zooInspectorManager;
			}

			public void actionPerformed(ActionEvent e)
			{
				IList<string> selectedNodes = outerInstance.SelectedNodes;
				zooInspectorManager.removeWatchers(selectedNodes);
			}
		}

		private class MouseAdapterAnonymousInnerClass : MouseAdapter
		{
			private readonly ZooInspectorTreeViewer outerInstance;

			private JPopupMenu popupMenu;
			private JMenuItem addNotify;
			private JMenuItem removeNotify;

			public MouseAdapterAnonymousInnerClass(ZooInspectorTreeViewer outerInstance, JPopupMenu popupMenu, JMenuItem addNotify, JMenuItem removeNotify)
			{
				this.outerInstance = outerInstance;
				this.popupMenu = popupMenu;
				this.addNotify = addNotify;
				this.removeNotify = removeNotify;
			}

			public override void mouseClicked(MouseEvent e)
			{
				if (e.isPopupTrigger() || e.getButton() == MouseEvent.BUTTON3)
				{
					// TODO only show add if a selected node isn't being
					// watched, and only show remove if a selected node is being
					// watched
					popupMenu.removeAll();
					popupMenu.add(addNotify);
					popupMenu.add(removeNotify);
					popupMenu.show(outerInstance, e.getX(), e.getY());
				}
			}
		}

		/// 
		public virtual void refreshView()
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final java.util.Set<javax.swing.tree.TreePath> expandedNodes = new java.util.LinkedHashSet<javax.swing.tree.TreePath>();
			ISet<TreePath> expandedNodes = new LinkedHashSet<TreePath>();
			int rowCount = tree.getRowCount();
			for (int i = 0; i < rowCount; i++)
			{
				TreePath path = tree.getPathForRow(i);
				if (tree.isExpanded(path))
				{
					expandedNodes.Add(path);
				}
			}
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final javax.swing.tree.TreePath[] selectedNodes = tree.getSelectionPaths();
			TreePath[] selectedNodes = tree.getSelectionPaths();
			SwingWorker<bool, Void> worker = new SwingWorkerAnonymousInnerClass(this, expandedNodes, selectedNodes);
			worker.execute();
		}

		private class SwingWorkerAnonymousInnerClass : SwingWorker<bool, Void>
		{
			private readonly ZooInspectorTreeViewer outerInstance;

			private ISet<TreePath> expandedNodes;
			private TreePath[] selectedNodes;

			public SwingWorkerAnonymousInnerClass(ZooInspectorTreeViewer outerInstance, ISet<TreePath> expandedNodes, TreePath[] selectedNodes)
			{
				this.outerInstance = outerInstance;
				this.expandedNodes = expandedNodes;
				this.selectedNodes = selectedNodes;
			}


//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: @Override protected System.Nullable<bool> doInBackground() throws Exception
			protected internal override bool? doInBackground()
			{
				outerInstance.tree.setModel(new DefaultTreeModel(new ZooInspectorTreeNode(outerInstance, "/", null)));
				return true;
			}

			protected internal override void done()
			{
				foreach (TreePath path in expandedNodes)
				{
					outerInstance.tree.expandPath(path);
				}
				outerInstance.tree.getSelectionModel().setSelectionPaths(selectedNodes);
			}
		}

		/// 
		public virtual void clearView()
		{
			tree.setModel(new DefaultTreeModel(new DefaultMutableTreeNode()));
		}

		/// <summary>
		/// @author Colin
		/// 
		/// </summary>
		private class ZooInspectorTreeCellRenderer : DefaultTreeCellRenderer
		{
			private readonly ZooInspectorTreeViewer outerInstance;

			public ZooInspectorTreeCellRenderer(ZooInspectorTreeViewer outerInstance)
			{
				this.outerInstance = outerInstance;
				setLeafIcon(ZooInspectorIconResources.TreeLeafIcon);
				setOpenIcon(ZooInspectorIconResources.TreeOpenIcon);
				setClosedIcon(ZooInspectorIconResources.TreeClosedIcon);
			}
		}

		/// <summary>
		/// @author Colin
		/// 
		/// </summary>
		private class ZooInspectorTreeNode : TreeNode
		{
			private readonly ZooInspectorTreeViewer outerInstance;

			internal readonly string nodePath;

//====================================================================================================
//End of the allowed output for the Free Edition of Java to C# Converter.

//To purchase the Premium Edition, visit our website:
//https://www.tangiblesoftwaresolutions.com/order/order-java-to-csharp.html
//====================================================================================================
