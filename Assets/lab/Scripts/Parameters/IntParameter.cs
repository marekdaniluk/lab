namespace lab {
    /// <summary>
    /// Serializable int parameter.
    /// <para>This weird solution of inheriting from generic class is to avoid boiler code of serialization different types of parameters, because Unity does not serialize dictinaries.</para>
    /// </summary>
    [System.Serializable]
    public class IntParameter : ASerializableParameter<string, int> {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public IntParameter() : base() { }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="intParameter">Serializable parameter to copy</param>
        public IntParameter(IntParameter intParameter) : base(intParameter) { }
    }
}
