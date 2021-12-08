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
	public interface ZooInspectorReadOnlyManager
	{

		/// <param name="nodePath"> </param>
		/// <returns> the data for the node </returns>
		string getData(string nodePath);

		/// <param name="nodePath"> </param>
		/// <returns> the metaData for the node </returns>
		IDictionary<string, string> getNodeMeta(string nodePath);

		/// <param name="nodePath"> </param>
		/// <returns> the ACLs set on the node </returns>
		IList<IDictionary<string, string>> getACLs(string nodePath);

		/// <returns> the metaData for the current session </returns>
		IDictionary<string, string> SessionMeta {get;}

		/// <param name="nodePath"> </param>
		/// <returns> true if the node has children </returns>
		bool hasChildren(string nodePath);

		/// <param name="nodePath"> </param>
		/// <returns> the index of the node within its siblings </returns>
		int getNodeIndex(string nodePath);

		/// <param name="nodePath"> </param>
		/// <returns> the number of children of the node </returns>
		int getNumChildren(string nodePath);

		/// <param name="nodePath"> </param>
		/// <param name="childIndex"> </param>
		/// <returns> the path to the node for the child of the nodePath at childIndex </returns>
		string getNodeChild(string nodePath, int childIndex);

		/// <param name="nodePath"> </param>
		/// <returns> true if the node allows children nodes </returns>
		bool isAllowsChildren(string nodePath);

		/// <param name="nodePath"> </param>
		/// <returns> a <seealso cref="System.Collections.IList"/> of the children of the node </returns>
		IList<string> getChildren(string nodePath);

	}
}