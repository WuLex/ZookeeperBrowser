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



	using LoggerFactory = org.apache.zookeeper.inspector.logger.LoggerFactory;

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public class ZooInspectorAboutDialog : JDialog
	{
		/// <param name="frame">
		///  </param>
		public ZooInspectorAboutDialog(Frame frame) : base(frame)
		{
			this.setLayout(new BorderLayout());
			this.setIconImage(ZooInspectorIconResources.InformationIcon.getImage());
			this.setTitle("About ZooInspector");
			this.setModal(true);
			this.setAlwaysOnTop(true);
			this.setResizable(false);
			JPanel panel = new JPanel();
			panel.setLayout(new TableLayout(new double[] {5, 800, 5}, new double[] {5, 170, 5}));
			JEditorPane aboutPane = new JEditorPane();
			aboutPane.setEditable(false);
			aboutPane.setOpaque(false);
			java.net.URL aboutURL = typeof(ZooInspectorAboutDialog).getResource("about.html");
			try
			{
				aboutPane.setPage(aboutURL);
			}
			catch (IOException e)
			{
				LoggerFactory.Logger.error("Error loading about.html, file may be corrupt", e);
			}
			panel.add(aboutPane, "1,1");
			JPanel buttonsPanel = new JPanel();
			buttonsPanel.setLayout(new TableLayout(new double[] {TableLayout.FILL, TableLayout.PREFERRED, TableLayout.FILL}, new double[] {TableLayout.PREFERRED}));
			JButton okButton = new JButton("OK");
			okButton.addActionListener(new ActionListenerAnonymousInnerClass(this));
			buttonsPanel.add(okButton, "1,0");
			this.add(panel, BorderLayout.CENTER);
			this.add(buttonsPanel, BorderLayout.SOUTH);
			this.pack();
		}

		private class ActionListenerAnonymousInnerClass : ActionListener
		{
			private readonly ZooInspectorAboutDialog outerInstance;

			public ActionListenerAnonymousInnerClass(ZooInspectorAboutDialog outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public void actionPerformed(ActionEvent e)
			{
				outerInstance.dispose();
			}
		}
	}

}