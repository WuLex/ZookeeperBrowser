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
namespace org.apache.zookeeper.inspector.encryption
{
	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public class BasicDataEncryptionManager : DataEncryptionManager
	{

		/*
		 * (non-Javadoc)
		 * 
		 * @see
		 * org.apache.zookeeper.inspector.encryption.DataEncryptionManager#decryptData
		 * (byte[])
		 */
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public String decryptData(byte[] encrypted) throws Exception
		public virtual string decryptData(sbyte[] encrypted)
		{
			return StringHelper.NewString(encrypted);
		}

		/*
		 * (non-Javadoc)
		 * 
		 * @see
		 * org.apache.zookeeper.inspector.encryption.DataEncryptionManager#encryptData
		 * (java.lang.String)
		 */
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public byte[] encryptData(String data) throws Exception
		public virtual sbyte[] encryptData(string data)
		{
			if (string.ReferenceEquals(data, null))
			{
				return new sbyte[0];
			}
			return data.GetBytes();
		}

	}

}