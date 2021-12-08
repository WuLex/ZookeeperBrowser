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
	public interface ZooInspectorNodeManager : ZooInspectorReadOnlyManager
	{
		/// <param name="nodePath"> </param>
		/// <param name="data"> </param>
		/// <returns> true if the data for the node was successfully updated </returns>
		bool setData(string nodePath, string data);
	}

}