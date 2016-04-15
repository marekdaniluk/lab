namespace lab {
    /// <summary>
    /// Serializable string parameter.
    /// <para>This weird solution of inheriting from generic class is to avoid boiler code of serialization different types of parameters, because Unity does not serialize dictinaries.</para>
    /// </summary>
    [System.Serializable]
    public class StringParameter : ASerializableParameter<string, string> {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StringParameter() : base() { }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="stringParameter">Serializable parameter to copy.</param>
        public StringParameter(StringParameter stringParameter) : base(stringParameter) { }
    }
}
