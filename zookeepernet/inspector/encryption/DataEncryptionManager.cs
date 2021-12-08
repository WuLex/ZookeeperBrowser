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
	/// A class which describes how data should be encrypted and decrypted
	/// 
	/// @author CGSmithe
	/// 
	/// </summary>
	public interface DataEncryptionManager
	{
		/// <param name="data">
		///            - the data to be encrypted </param>
		/// <returns> the encrypted data </returns>
		/// <exception cref="Exception"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public byte[] encryptData(String data) throws Exception;
		sbyte[] encryptData(string data);

		/// <param name="encrypted">
		///            - the data to be decrypted </param>
		/// <returns> the decrypted data </returns>
		/// <exception cref="Exception"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public String decryptData(byte[] encrypted) throws Exception;
		string decryptData(sbyte[] encrypted);
	}

}