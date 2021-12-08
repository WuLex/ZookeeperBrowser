//====================================================================================================
//The Free Edition of Java to C# Converter limits conversion output to 100 lines per file.

//To purchase the Premium Edition, visit our website:
//https://www.tangiblesoftwaresolutions.com/order/order-java-to-csharp.html
//====================================================================================================

using System;
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
namespace org.apache.zookeeper.inspector.gui
{
	using TableLayout = info.clearthought.layout.TableLayout;



	using ZooInspectorNodeViewer = org.apache.zookeeper.inspector.gui.nodeviewer.ZooInspectorNodeViewer;
	using LoggerFactory = org.apache.zookeeper.inspector.logger.LoggerFactory;
	using ZooInspectorManager = org.apache.zookeeper.inspector.manager.ZooInspectorManager;

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public class ZooInspectorNodeViewersDialog : JDialog, ListSelectionListener
	{

		private readonly JButton upButton;
		private readonly JButton downButton;
		private readonly JButton removeButton;
		private readonly JButton addButton;
		private readonly JList viewersList;
		private readonly JButton saveFileButton;
		private readonly JButton loadFileButton;
		private readonly JButton setDefaultsButton;
		private readonly JFileChooser fileChooser = new JFileChooser(new File("."));

		/// <param name="frame"> </param>
		/// <param name="currentViewers"> </param>
		/// <param name="listeners"> </param>
		/// <param name="manager">
		///  </param>
		public ZooInspectorNodeViewersDialog(Frame frame, in IList<ZooInspectorNodeViewer> currentViewers, in ICollection<NodeViewersChangeListener> listeners, in ZooInspectorManager manager) : base(frame)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final java.util.List<org.apache.zookeeper.inspector.gui.nodeviewer.ZooInspectorNodeViewer> newViewers = new java.util.ArrayList<org.apache.zookeeper.inspector.gui.nodeviewer.ZooInspectorNodeViewer>(currentViewers);
			IList<ZooInspectorNodeViewer> newViewers = new List<ZooInspectorNodeViewer>(currentViewers);
			this.setLayout(new BorderLayout());
			this.setIconImage(ZooInspectorIconResources.ChangeNodeViewersIcon.getImage());
			this.setTitle("About ZooInspector");
			this.setModal(true);
			this.setAlwaysOnTop(true);
			this.setResizable(true);
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final javax.swing.JPanel panel = new javax.swing.JPanel();
			JPanel panel = new JPanel();
			panel.setLayout(new TableLayout(new double[] {10, TableLayout.PREFERRED, 5, TableLayout.PREFERRED, 5, TableLayout.FILL, TableLayout.PREFERRED, 5, TableLayout.PREFERRED, 10}, new double[] {10, TableLayout.PREFERRED, 5, TableLayout.PREFERRED, 5, TableLayout.PREFERRED, TableLayout.FILL, 5, TableLayout.PREFERRED, 5, TableLayout.PREFERRED, 10}));
			viewersList = new JList();
			DefaultListModel model = new DefaultListModel();
			foreach (ZooInspectorNodeViewer viewer in newViewers)
			{
				model.addElement(viewer);
			}
			viewersList.setModel(model);
			viewersList.setCellRenderer(new DefaultListCellRendererAnonymousInnerClass(this));
			viewersList.setDropMode(DropMode.INSERT);
			viewersList.enableInputMethods(true);
			viewersList.setDragEnabled(true);
			viewersList.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
			viewersList.getSelectionModel().addListSelectionListener(this);
			viewersList.setTransferHandler(new TransferHandlerAnonymousInnerClass(this));
			JScrollPane scroller = new JScrollPane(viewersList);
			panel.add(scroller, "1,1,6,6");
			upButton = new JButton(ZooInspectorIconResources.UpIcon);
			downButton = new JButton(ZooInspectorIconResources.DownIcon);
			removeButton = new JButton(ZooInspectorIconResources.DeleteNodeIcon);
			addButton = new JButton(ZooInspectorIconResources.AddNodeIcon);
			upButton.setEnabled(false);
			downButton.setEnabled(false);
			removeButton.setEnabled(false);
			addButton.setEnabled(true);
			upButton.setToolTipText("Move currently selected node viewer up");
			downButton.setToolTipText("Move currently selected node viewer down");
			removeButton.setToolTipText("Remove currently selected node viewer");
			addButton.setToolTipText("Add node viewer");
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final javax.swing.JTextField newViewerTextField = new javax.swing.JTextField();
			JTextField newViewerTextField = new JTextField();
			panel.add(upButton, "8,1");
			panel.add(downButton, "8,5");
			panel.add(removeButton, "8,3");
			panel.add(newViewerTextField, "1,8,6,8");
			panel.add(addButton, "8,8");
			upButton.addActionListener(new ActionListenerAnonymousInnerClass(this));
			downButton.addActionListener(new ActionListenerAnonymousInnerClass2(this));
			removeButton.addActionListener(new ActionListenerAnonymousInnerClass3(this));
			addButton.addActionListener(new ActionListenerAnonymousInnerClass4(this, newViewerTextField));
			saveFileButton = new JButton("Save");
			loadFileButton = new JButton("Load");
			setDefaultsButton = new JButton("Set As Defaults");
			saveFileButton.setToolTipText("Save current node viewer configuration to file");
			loadFileButton.setToolTipText("Load node viewer configuration frm file");
			setDefaultsButton.setToolTipText("Set current configuration asd defaults");
			panel.add(saveFileButton, "1,10");
			panel.add(loadFileButton, "3,10");
			panel.add(setDefaultsButton, "6,10");
			saveFileButton.addActionListener(new ActionListenerAnonymousInnerClass5(this, manager));
			loadFileButton.addActionListener(new ActionListenerAnonymousInnerClass6(this, manager, panel));
			setDefaultsButton.addActionListener(new ActionListenerAnonymousInnerClass7(this, manager));

			JPanel buttonsPanel = new JPanel();
			buttonsPanel.setLayout(new TableLayout(new double[] {10, TableLayout.FILL, TableLayout.PREFERRED, 5, TableLayout.PREFERRED, 10, TableLayout.FILL}, new double[] {TableLayout.PREFERRED}));
			JButton okButton = new JButton("OK");
			okButton.addActionListener(new ActionListenerAnonymousInnerClass8(this, currentViewers, listeners, newViewers));
			buttonsPanel.add(okButton, "2,0");
			JButton cancelButton = new JButton("Cancel");
			cancelButton.addActionListener(new ActionListenerAnonymousInnerClass9(this));
			buttonsPanel.add(cancelButton, "4,0");
			this.add(panel, BorderLayout.CENTER);
			this.add(buttonsPanel, BorderLayout.SOUTH);
			this.pack();
		}

		private class DefaultListCellRendererAnonymousInnerClass : DefaultListCellRenderer
		{
			private readonly ZooInspectorNodeViewersDialog outerInstance;

			public DefaultListCellRendererAnonymousInnerClass(ZooInspectorNodeViewersDialog outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public override Component getListCellRendererComponent(JList list, object value, int index, bool isSelected, bool cellHasFocus)
			{
				ZooInspectorNodeViewer viewer = (ZooInspectorNodeViewer) value;
				JLabel label = (JLabel) base.getListCellRendererComponent(list, value, index, isSelected, cellHasFocus);
				label.setText(viewer.Title);
				return label;
			}
		}

		private class TransferHandlerAnonymousInnerClass : TransferHandler
		{
			private readonly ZooInspectorNodeViewersDialog outerInstance;

			public TransferHandlerAnonymousInnerClass(ZooInspectorNodeViewersDialog outerInstance)
			{
				this.outerInstance = outerInstance;
			}


			public override bool canImport(TransferHandler.TransferSupport info)
			{
				// we only import NodeViewers

//====================================================================================================
//End of the allowed output for the Free Edition of Java to C# Converter.

//To purchase the Premium Edition, visit our website:
//https://www.tangiblesoftwaresolutions.com/order/order-java-to-csharp.html
//====================================================================================================
