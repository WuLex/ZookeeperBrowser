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
namespace org.apache.zookeeper.inspector.manager
{


	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public interface ZooInspectorManager : ZooInspectorNodeManager, ZooInspectorNodeTreeManager
	{

		/// <param name="connectionProps"> </param>
		/// <returns> true if successfully connected </returns>
		bool connect(Properties connectionProps);

		/// <returns> true if successfully disconnected </returns>
		bool disconnect();

		/// <returns> a <seealso cref="Pair"/> containing the following:
		///         <ul>
		///         <li>a <seealso cref="System.Collections.IDictionary"/> of property keys to list of possible values. If
		///         the list size is 1 the value is taken to be the default value for
		///         a <seealso cref="JTextField"/>. If the list size is greater than 1, the
		///         values are taken to be the possible options to show in a
		///         <seealso cref="JComboBox"/> with the first selected as default.</li>
		///         <li>a <seealso cref="System.Collections.IDictionary"/> of property keys to the label to show on the UI
		///         </li>
		///         <ul>
		///  </returns>
		Pair<IDictionary<string, IList<string>>, IDictionary<string, string>> ConnectionPropertiesTemplate {get;}

		/// <param name="selectedNodes"> </param>
		/// <param name="nodeListener"> </param>
		void addWatchers(IList<string> selectedNodes, NodeListener nodeListener);

		/// <param name="selectedNodes"> </param>
		void removeWatchers(IList<string> selectedNodes);

		/// <param name="selectedFile"> </param>
		/// <returns> nodeViewers </returns>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public java.util.List<String> loadNodeViewersFile(java.io.File selectedFile) throws java.io.IOException;
		IList<string> loadNodeViewersFile(File selectedFile);

		/// <param name="selectedFile"> </param>
		/// <param name="nodeViewersClassNames"> </param>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void saveNodeViewersFile(java.io.File selectedFile, java.util.List<String> nodeViewersClassNames) throws java.io.IOException;
		void saveNodeViewersFile(File selectedFile, IList<string> nodeViewersClassNames);

		/// <param name="nodeViewersClassNames"> </param>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void setDefaultNodeViewerConfiguration(java.util.List<String> nodeViewersClassNames) throws java.io.IOException;
		IList<string> DefaultNodeViewerConfiguration {set;get;}

		/// <returns> nodeViewers </returns>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: java.util.List<String> getDefaultNodeViewerConfiguration() throws java.io.IOException;

	}

}