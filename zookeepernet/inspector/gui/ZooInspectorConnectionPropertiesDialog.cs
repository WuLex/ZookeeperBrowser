//====================================================================================================
//The Free Edition of Java to C# Converter limits conversion output to 100 lines per file.

//To purchase the Premium Edition, visit our website:
//https://www.tangiblesoftwaresolutions.com/order/order-java-to-csharp.html
//====================================================================================================

using System.Collections.Generic;
using System.IO;

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
	using org.apache.zookeeper.inspector.manager;

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public class ZooInspectorConnectionPropertiesDialog : JDialog
	{

		/// <param name="connectionPropertiesTemplateAndLabels"> </param>
		/// <param name="zooInspectorPanel"> </param>
		public ZooInspectorConnectionPropertiesDialog(Pair<IDictionary<string, IList<string>>, IDictionary<string, string>> connectionPropertiesTemplateAndLabels, in ZooInspectorPanel zooInspectorPanel)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final java.util.Map<String, java.util.List<String>> connectionPropertiesTemplate = connectionPropertiesTemplateAndLabels.getKey();
			IDictionary<string, IList<string>> connectionPropertiesTemplate = connectionPropertiesTemplateAndLabels.Key;
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final java.util.Map<String, String> connectionPropertiesLabels = connectionPropertiesTemplateAndLabels.getValue();
			IDictionary<string, string> connectionPropertiesLabels = connectionPropertiesTemplateAndLabels.Value;
			this.setLayout(new BorderLayout());
			this.setTitle("Connection Settings");
			this.setModal(true);
			this.setAlwaysOnTop(true);
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final javax.swing.JPanel options = new javax.swing.JPanel();
			JPanel options = new JPanel();
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final javax.swing.JFileChooser fileChooser = new javax.swing.JFileChooser();
			JFileChooser fileChooser = new JFileChooser();
			int numRows = connectionPropertiesTemplate.Count * 2 + 1;
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
			options.setLayout(new TableLayout(new double[] {10, TableLayout.PREFERRED, 5, TableLayout.PREFERRED, 10}, rows));
			int i = 0;
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final java.util.Map<String, javax.swing.JComponent> components = new java.util.HashMap<String, javax.swing.JComponent>();
			IDictionary<string, JComponent> components = new Dictionary<string, JComponent>();
			foreach (KeyValuePair<string, IList<string>> entry in connectionPropertiesTemplate.SetOfKeyValuePairs())
			{
				int rowPos = 2 * i + 1;
				JLabel label = new JLabel(connectionPropertiesLabels[entry.Key]);
				options.add(label, "1," + rowPos);
				if (entry.Value.size() == 0)
				{
					JTextField text = new JTextField();
					options.add(text, "3," + rowPos);
					components[entry.Key] = text;
				}
				else if (entry.Value.size() == 1)
				{
					JTextField text = new JTextField(entry.Value.get(0));
					options.add(text, "3," + rowPos);
					components[entry.Key] = text;
				}
				else
				{
					IList<string> list = entry.Value;
					JComboBox combo = new JComboBox(((List<string>)list).ToArray());
					combo.setSelectedItem(list[0]);
					options.add(combo, "3," + rowPos);
					components[entry.Key] = combo;
				}
				i++;
			}
			JPanel buttonsPanel = new JPanel();
			buttonsPanel.setLayout(new TableLayout(new double[] {10, TableLayout.PREFERRED, 5, TableLayout.FILL, TableLayout.PREFERRED, 5, TableLayout.PREFERRED, 10}, new double[] {TableLayout.PREFERRED}));
			JButton loadPropsFileButton = new JButton("Load from file");
			loadPropsFileButton.addActionListener(new ActionListenerAnonymousInnerClass(this, options, fileChooser, components));
			buttonsPanel.add(loadPropsFileButton, "1,0");

			JButton okButton = new JButton("OK");
			okButton.addActionListener(new ActionListenerAnonymousInnerClass2(this, zooInspectorPanel, components));
			buttonsPanel.add(okButton, "4,0");
			JButton cancelButton = new JButton("Cancel");
			cancelButton.addActionListener(new ActionListenerAnonymousInnerClass3(this));
			buttonsPanel.add(cancelButton, "6,0");
			this.add(options, BorderLayout.CENTER);
			this.add(buttonsPanel, BorderLayout.SOUTH);
			this.pack();
		}

		private class ActionListenerAnonymousInnerClass : ActionListener
		{
			private readonly ZooInspectorConnectionPropertiesDialog outerInstance;

			private JPanel options;
			private JFileChooser fileChooser;
			private IDictionary<string, JComponent> components;

			public ActionListenerAnonymousInnerClass(ZooInspectorConnectionPropertiesDialog outerInstance, JPanel options, JFileChooser fileChooser, IDictionary<string, JComponent> components)
			{
				this.outerInstance = outerInstance;
				this.options = options;
				this.fileChooser = fileChooser;
				this.components = components;
			}


			public void actionPerformed(ActionEvent e)
			{
				int result = fileChooser.showOpenDialog(outerInstance);
				if (result == JFileChooser.APPROVE_OPTION)
				{
					File propsFilePath = fileChooser.getSelectedFile();
					Properties props = new Properties();
					try
					{
						StreamReader reader = new StreamReader(propsFilePath);
						try
						{
							props.load(reader);
							foreach (object key in props.keySet())
							{
								string propsKey = (string) key;
								if (components.ContainsKey(propsKey))
								{
									JComponent component = components[propsKey];
									string value = props.getProperty(propsKey);
									if (component is JTextField)
									{
										((JTextField) component).setText(value);
									}
									else if (component is JComboBox)
									{
										((JComboBox) component).setSelectedItem(value);
									}
								}
							}
						}
						finally
						{
							reader.Close();
						}
					}
					catch (IOException ex)
					{
						LoggerFactory.Logger.error("An Error occirred loading connection properties from file", ex);
						JOptionPane.showMessageDialog(outerInstance, "An Error occirred loading connection properties from file", "Error", JOptionPane.ERROR_MESSAGE);
					}
					options.revalidate();
					options.repaint();
				}

			}
		}

		private class ActionListenerAnonymousInnerClass2 : ActionListener
		{
			private readonly ZooInspectorConnectionPropertiesDialog outerInstance;

			private org.apache.zookeeper.inspector.gui.ZooInspectorPanel zooInspectorPanel;
			private IDictionary<string, JComponent> components;

			public ActionListenerAnonymousInnerClass2(ZooInspectorConnectionPropertiesDialog outerInstance, org.apache.zookeeper.inspector.gui.ZooInspectorPanel zooInspectorPanel, IDictionary<string, JComponent> components)
			{
				this.outerInstance = outerInstance;

//====================================================================================================
//End of the allowed output for the Free Edition of Java to C# Converter.

//To purchase the Premium Edition, visit our website:
//https://www.tangiblesoftwaresolutions.com/order/order-java-to-csharp.html
//====================================================================================================
