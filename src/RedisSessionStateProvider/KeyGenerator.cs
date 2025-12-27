//
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
//

using System;

namespace Microsoft.Web.Redis
{
    internal class KeyGenerator
    {
        private string id;
        public string DataKey { get; private set; }
        public string LockKey { get; private set; }
        public string InternalKey { get; private set; }

        private void GenerateKeys(string id, string app, bool enableSessionKeyHashtag)
        {
            this.id = id;
            string prefixKey = enableSessionKeyHashtag ?  $"{{{app}_{id}}}" : $"{app}_{id}";
            DataKey = $"{prefixKey}_SessionStateItemCollection";
            LockKey = $"{prefixKey}_WriteLock";
            InternalKey = $"{prefixKey}_SessionTimeout";
        }

        public KeyGenerator(string sessionId, string applicationName, bool enableSessionKeyHashtag)
        {
            GenerateKeys(sessionId, applicationName,enableSessionKeyHashtag);
        }

        public void RegenerateKeyStringIfIdModified(string sessionId, string applicationName, bool enableSessionKeyHashtag)
        {
            if (!sessionId.Equals(this.id))
            {
                GenerateKeys(sessionId, applicationName, enableSessionKeyHashtag);
            }
        }
    }
}