﻿using System.Collections.Generic;

namespace Xeora.Web.Basics
{
    public class DomainInfo
    {
        public enum DeploymentTypes
        {
            Development,
            Release
        }

        public DomainInfo(DeploymentTypes deploymentType, string ID, LanguageInfo[] languages)
        {
            this.DeploymentType = deploymentType;
            this.ID = ID;
            this.Languages = languages;
            this.Children = new DomainInfoCollection();
        }

        /// <summary>
        /// Gets the type of the domain deployment
        /// </summary>
        /// <value>The type of the domain deployment</value>
        public DeploymentTypes DeploymentType { get; private set; }

        /// <summary>
        /// Gets the domain identifier
        /// </summary>
        /// <value>The domain identifier</value>
        public string ID { get; private set; }

        /// <summary>
        /// Gets the available languages for the domain
        /// </summary>
        /// <value>The available languages</value>
        public LanguageInfo[] Languages { get; private set; }

        /// <summary>
        /// Gets the children domains
        /// </summary>
        /// <value>The children domain collection</value>
        public DomainInfoCollection Children { get; private set; }

        public class LanguageInfo
        {
            public LanguageInfo(string ID, string name)
            {
                this.ID = ID;
                this.Name = name;
            }

            /// <summary>
            /// Gets the language identifier
            /// </summary>
            /// <value>The language identifier</value>
            public string ID { get; private set; }

            /// <summary>
            /// Gets the language human readable name
            /// </summary>
            /// <value>The language human readable name</value>
            public string Name { get; private set; }
        }

        public class DomainInfoCollection : List<DomainInfo>
        {
            private Dictionary<string, int> _NameIndexMap;

            public DomainInfoCollection() : base() =>
                this._NameIndexMap = new Dictionary<string, int>();

            public new void Add(DomainInfo value)
            {
                base.Add(value);

                this._NameIndexMap.Add(value.ID, base.Count - 1);
            }

            public void Remove(string ID)
            {
                if (this._NameIndexMap.ContainsKey(ID))
                {
                    base.RemoveAt(this._NameIndexMap[ID]);

                    this._NameIndexMap.Clear();

                    // Rebuild, NameIndexMap
                    int Index = 0;
                    foreach (DomainInfo item in this)
                    {
                        this._NameIndexMap.Add(item.ID, Index);

                        Index += 1;
                    }
                }
            }

            public new void Remove(DomainInfo value) =>
                this.Remove(value.ID);

            public new DomainInfo this[int index]
            {
                get
                {
                    if (index < this.Count)
                        return base[index];

                    return null;
                }
                set
                {
                    this.Remove(value.ID);
                    this.Add(value);
                }
            }

            public DomainInfo this[string ID]
            {
                get
                {
                    if (this._NameIndexMap.ContainsKey(ID))
                        return base[this._NameIndexMap[ID]];

                    return null;
                }
                set
                {
                    this.Remove(value.ID);
                    this.Add(value);
                }
            }
        }
    }
}
