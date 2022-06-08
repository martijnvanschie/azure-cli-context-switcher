namespace Azure.Cli.ContextSwitcher.Model
{
    [Serializable]
    public class ContextConfigurationException : Exception
    {
        public ContextConfigurationException() { }
        public ContextConfigurationException(string message) : base(message) { }
        public ContextConfigurationException(string message, Exception inner) : base(message, inner) { }
        protected ContextConfigurationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
