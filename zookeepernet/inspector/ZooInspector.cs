using System;

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
namespace org.apache.zookeeper.inspector
{


	using ZooInspectorPanel = org.apache.zookeeper.inspector.gui.ZooInspectorPanel;
	using LoggerFactory = org.apache.zookeeper.inspector.logger.LoggerFactory;
	using ZooInspectorManagerImpl = org.apache.zookeeper.inspector.manager.ZooInspectorManagerImpl;

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public class ZooInspector
	{
		/// <param name="args"> </param>
		public static void Main(string[] args)
		{
			try
			{
				UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
				JFrame frame = new JFrame("ZooInspector");
				frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final org.apache.zookeeper.inspector.gui.ZooInspectorPanel zooInspectorPanel = new org.apache.zookeeper.inspector.gui.ZooInspectorPanel(new org.apache.zookeeper.inspector.manager.ZooInspectorManagerImpl());
				ZooInspectorPanel zooInspectorPanel = new ZooInspectorPanel(new ZooInspectorManagerImpl());
				frame.addWindowListener(new WindowAdapterAnonymousInnerClass(zooInspectorPanel));

				frame.setContentPane(zooInspectorPanel);
				frame.setSize(1024, 768);
				frame.setVisible(true);
			}
			catch (Exception e)
			{
				LoggerFactory.Logger.error("Error occurred loading ZooInspector", e);
				JOptionPane.showMessageDialog(null, "ZooInspector failed to start: " + e.Message, "Error", JOptionPane.ERROR_MESSAGE);
			}
		}

		private class WindowAdapterAnonymousInnerClass : WindowAdapter
		{
			private ZooInspectorPanel zooInspectorPanel;

			public WindowAdapterAnonymousInnerClass(ZooInspectorPanel zooInspectorPanel)
			{
				this.zooInspectorPanel = zooInspectorPanel;
			}

			public override void windowClosed(WindowEvent e)
			{
				base.windowClosed(e);
				zooInspectorPanel.disconnect(true);
			}
		}
	}

}