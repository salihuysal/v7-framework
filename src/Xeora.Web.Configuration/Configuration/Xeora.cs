﻿using Newtonsoft.Json;

namespace Xeora.Web.Configuration
{
    public class Xeora : Basics.Configuration.IXeora
    {
        public Xeora()
        {
            this.Service = new Service();
            this.DSS = new DSS();
            this.Session = new Session();
            this.Application = new Application();
            this.User = new UserSettings();
        }

        [JsonProperty(PropertyName = "service")]
        public Basics.Configuration.IService Service { get; private set; }

        [JsonProperty(PropertyName = "dss")]
        public Basics.Configuration.IDSS DSS { get; private set; }

        [JsonProperty(PropertyName = "session")]
        public Basics.Configuration.ISession Session { get; private set; }

        [JsonProperty(PropertyName = "application", Required = Required.Always)]
        public Basics.Configuration.IApplication Application { get; private set; }

        [JsonProperty(PropertyName = "user")]
        public Basics.Configuration.IUserSettings User { get; private set; }
    }
}
